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

        public async Task ReplaceTable(string pinUpPath, string oldTablePath, string newTablePath)
        {
            var oldTableFile = Path.GetFileName(oldTablePath);
            var newTableFile = Path.GetFileName(newTablePath);

            await UpdateDatabase(pinUpPath, oldTableFile, newTableFile);
            ReplaceMediaFiles(Path.Combine(pinUpPath, pinUpMediaFolder), oldTableFile, newTableFile);
            ReplaceTableFiles(Path.GetDirectoryName(oldTablePath), oldTablePath, newTablePath);
        }

        private void ReplaceTableFiles(string? tablePath, string oldTablePath, string newTablePath)
        {
            RenameBackscreenFile(oldTablePath, newTablePath);
            CopyTableFile(oldTablePath, newTablePath);
            DeleteOriginalTable(oldTablePath);
        }

        private static void DeleteOriginalTable(string oldTablePath)
        {
            if (File.Exists(oldTablePath))
            {
                try
                {
                    File.Delete(oldTablePath);
                }
                catch (Exception exc)
                {
                    throw new DeleteFileFailedException(oldTablePath, exc);
                }
            }
        }

        private static void CopyTableFile(string oldTablePath, string newTablePath)
        {
            var newFullTablePath = oldTablePath.Replace(Path.GetFileName(oldTablePath), Path.GetFileName(newTablePath));
            try
            {
                File.Copy(newTablePath, newFullTablePath);
            }
            catch (Exception exc)
            {
                throw new CopyFileFailedException(newTablePath, newFullTablePath, exc);
            }
        }

        private static void RenameBackscreenFile(string oldTablePath, string newTablePath)
        {
            var oldBackscreenPath = oldTablePath.Replace(Path.GetExtension(oldTablePath), ".directb2s");
            if (File.Exists(oldBackscreenPath))
            {
                var newBackscreenPath = oldTablePath.Replace(Path.GetFileNameWithoutExtension(oldTablePath), Path.GetFileNameWithoutExtension(newTablePath));
                try
                {
                    File.Move(oldBackscreenPath, newBackscreenPath);
                }
                catch (Exception exc)
                {
                    throw new RenameFileFailedException(oldBackscreenPath, newBackscreenPath, exc);
                }
            }
        }

        private void ReplaceMediaFiles(string pinUpMediaPath, string oldTableFile, string newTableFile)
        {            
            foreach (var mediaFile in Directory.GetFiles(pinUpMediaPath, $"{Path.GetFileNameWithoutExtension(oldTableFile)}.*", SearchOption.AllDirectories))
            {
                var newMediaFile = mediaFile.Replace(Path.GetFileNameWithoutExtension(oldTableFile), Path.GetFileNameWithoutExtension(newTableFile));

                try
                {
                    File.Move(mediaFile, newMediaFile);
                }
                catch (Exception exc)
                {
                    throw new RenameFileFailedException(mediaFile, newMediaFile, exc);
                }                
            }
        }

        private async Task UpdateDatabase(string pinUpPath, string oldTableFile, string newTableFile)
        {
            var databasePath = Path.Combine(pinUpPath, "PUPDatabase.db");
            try
            {
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
                            await dbContext.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                throw new DatabaseUpdateFailedException(databasePath, exc);
            }
        }
    }
}
