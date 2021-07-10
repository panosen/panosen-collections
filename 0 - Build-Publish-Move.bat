@echo off

dotnet restore

dotnet build --no-restore -c Release

move /Y Panosen.Collections\bin\Release\Panosen.Collections.*.nupkg D:\LocalSavoryNuget\

pause