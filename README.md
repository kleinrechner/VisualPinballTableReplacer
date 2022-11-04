# VisualPinballTableReplacer
With the **VisualPinballTableReplacer** you can replace a table file with another one. For example you update a table by replacing Tablev1.vpx with Tablev2.vpx or replace a table with a different mod for example JPsTable.vpx with TableModBigus.vpx.

## Following steps will be executed
You want to replace and old version of a table with a new version

 - Update database
	>Update the properties "Game FileName" and "FileName" with the filename of new version at the  entry in the database with the filename of the old version
- Rename files in folder of old version table file
	>Rename all files with the filename of the old version, including directb2s and pos. 
	For example: oldVersion.directb2s -> newVersion.directb2s
- Rename files in POPMedia folder
	>Rename all files which are starting with the name of the old version table with the file name of the new version. 
	For example: playfield\oldVersion.mp4 -> playfiled\newVersion.mp4, loading\oldVersion(Screen)01.mp4 -> loading\newVersion(Screen)01.mp4
- Copy file of new version in the target folder of the old version
- Delete table file of old version

> When a target file already does exists it will not be overritten.
