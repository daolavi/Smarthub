SET server=%1
SET db=%2
SET user=%3
SET password=%4
SET path=%~dp0

"%path%\migrate.exe" --conn "Data Source=%server%;Initial Catalog=%db%;User ID=%user%;Password=%password%" --provider sqlserver2008 --assembly "%path%\SmartHub.DbMigration.dll" --task migrate:up
