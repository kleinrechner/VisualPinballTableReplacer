using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualPinballTableReplacer.Exceptions;

namespace VisualPinballTableReplacer.Services
{
    public class ReplaceVirtualPinballTableService : IReplaceVirtualPinballTableService
    {
        private const string dbFileName = "PUPDatabase.db";
        private const string pinUpMediaFolder = "POPMedia";

        private readonly IFormLoggingService _formLogging;

        public ReplaceVirtualPinballTableService(IFormLoggingService formLogging)
        {
            _formLogging = formLogging;
        }

        public async Task ReplaceTable(string pinUpPath, string oldTablePath, string newTablePath, CancellationToken cancellationToken = default)
        {
            _formLogging.LogInformation("Starting replace table...");
            var oldTableFile = Path.GetFileName(oldTablePath);
            var newTableFile = Path.GetFileName(newTablePath);

            await UpdateDatabase(pinUpPath, oldTableFile, newTableFile, cancellationToken);
            RenamePopMediaFolderFiles(Path.Combine(pinUpPath, pinUpMediaFolder), oldTableFile, newTableFile, cancellationToken);
            ReplaceTableFiles(oldTablePath, newTablePath, cancellationToken);

            _formLogging.LogSucceeded("Replace tables succeeded.");
        }

        private void ReplaceTableFiles(string oldTablePath, string newTablePath, CancellationToken cancellationToken = default)
        {
            RenameTableFolderFiles(oldTablePath, newTablePath, cancellationToken);
            CopyTableFile(oldTablePath, newTablePath, cancellationToken);
            DeleteOriginalTable(oldTablePath, cancellationToken);
        }

        private void DeleteOriginalTable(string oldTablePath, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (File.Exists(oldTablePath))
            {
                try
                {
#if(!DEMO)
                    File.Delete(oldTablePath);
#else
                    Thread.Sleep(2500);
#endif
                    _formLogging.LogSucceeded($"Removed file {Path.GetFileName(oldTablePath)}");
                }
                catch (Exception exc)
                {
                    _formLogging.LogException(new DeleteFileFailedException(oldTablePath, exc));
                }
            }
            else
            {
                _formLogging.LogWarning("Original table file does not exists.");
            }
        }

        private void CopyTableFile(string oldTablePath, string newTablePath, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var newFullTablePath = oldTablePath.Replace(Path.GetFileName(oldTablePath), Path.GetFileName(newTablePath));
            if (!File.Exists(newFullTablePath))
            {
                try
                {
#if (!DEMO)
                    File.Copy(newTablePath, newFullTablePath);
#else
                    Thread.Sleep(2500);
#endif
                    _formLogging.LogSucceeded($"Copy table {Path.GetFileName(oldTablePath)} to target folder succeeded.");
                }
                catch (Exception exc)
                {
                    _formLogging.LogException(new CopyFileFailedException(newTablePath, newFullTablePath, exc));
                }
            }
            else
            {
                _formLogging.LogWarning($"Target table file {newFullTablePath} already exists.");
            }
        }

        private void RenameTableFolderFiles(string oldTablePath, string newTablePath, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _formLogging.LogInformation($"Starting scan files in folder {Path.GetDirectoryName(oldTablePath)}...");
            foreach (var mediaFile in Directory.GetFiles(Path.GetDirectoryName(oldTablePath), $"{Path.GetFileNameWithoutExtension(oldTablePath)}.*", SearchOption.TopDirectoryOnly)
                                        .Where(x => Path.GetFileName(oldTablePath) != Path.GetFileName(x)))
            {
                cancellationToken.ThrowIfCancellationRequested();
                var newMediaFile = mediaFile.Replace(Path.GetFileNameWithoutExtension(oldTablePath), Path.GetFileNameWithoutExtension(newTablePath));

                if (!File.Exists(newMediaFile))
                {
                    try
                    {
#if (!DEMO)
                        File.Move(mediaFile, newMediaFile);
#else
                        Thread.Sleep(2500);
#endif
                        _formLogging.LogSucceeded($"Rename file {Path.GetFileName(mediaFile)} to {Path.GetFileName(newMediaFile)} succeeded.");
                    }
                    catch (Exception exc)
                    {
                        _formLogging.LogException(new RenameFileFailedException(mediaFile, newMediaFile, exc));
                    }
                }
                else
                {
                    _formLogging.LogWarning($"Target file {newMediaFile} already exists.");
                }
            }
        }

        private void RenamePopMediaFolderFiles(string pinUpMediaPath, string oldTableFile, string newTableFile, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _formLogging.LogInformation($"Starting scan files in folder {pinUpMediaPath}...");
            foreach (var mediaFile in Directory.GetFiles(pinUpMediaPath, $"{Path.GetFileNameWithoutExtension(oldTableFile)}*.*", SearchOption.AllDirectories))
            {
                cancellationToken.ThrowIfCancellationRequested();
                var newMediaFile = mediaFile.Replace(Path.GetFileNameWithoutExtension(oldTableFile), Path.GetFileNameWithoutExtension(newTableFile));

                if (!File.Exists(newMediaFile))
                {
                    try
                    {
#if (!DEMO)
                        File.Move(mediaFile, newMediaFile);
#else
                        Thread.Sleep(2500);
#endif
                        _formLogging.LogSucceeded($"Rename file {Path.GetFileName(mediaFile)} to {Path.GetFileName(newMediaFile)} succeeded.");
                    }
                    catch (Exception exc)
                    {
                        _formLogging.LogException(new RenameFileFailedException(mediaFile, newMediaFile, exc));
                    }
                }
                else
                {
                    _formLogging.LogWarning($"Target file {newMediaFile} already exists.");
                }
            }
        }

        private async Task UpdateDatabase(string pinUpPath, string oldTableFile, string newTableFile, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var databasePath = Path.Combine(pinUpPath, dbFileName);
            try
            {
                _formLogging.LogInformation($"Starting update database {databasePath}...");
                var connectionStringBuilder = new SqliteConnectionStringBuilder();
                connectionStringBuilder.DataSource = databasePath;

                var dbContextOptionsBuilder = new DbContextOptionsBuilder<PUPDatabaseContext>();
                dbContextOptionsBuilder.UseSqlite(connectionStringBuilder.ConnectionString);

                using (var dbContext = new PUPDatabaseContext(dbContextOptionsBuilder.Options))
                {
                    var currentGame = dbContext.Games.FirstOrDefault(x => x.GameFileName == oldTableFile);
                    if (currentGame != null)
                    {
                        var changed = false;
                        if (currentGame.GameFileName == oldTableFile)
                        {
                            currentGame.GameFileName = newTableFile;
                            changed = true;
                        }

                        if (currentGame.GameName == Path.GetFileNameWithoutExtension(oldTableFile))
                        {
                            currentGame.GameName = Path.GetFileNameWithoutExtension(newTableFile);
                            changed = true;
                        }

                        if (changed)
                        {
                            dbContext.Games.Update(currentGame);
                            
                            cancellationToken.ThrowIfCancellationRequested();
#if (!DEMO)
                            await dbContext.SaveChangesAsync();
#else
                            Thread.Sleep(2500);
#endif
                            _formLogging.LogSucceeded($"Update table in database succeeded.");
                        }
                    }
                    else
                    {
                        _formLogging.LogWarning($"Table in could not be found in database.");
                    }
                }
            }
            catch (Exception exc)
            {
                _formLogging.LogException(new DatabaseUpdateFailedException(databasePath, exc));
            }
        }
    }
}
