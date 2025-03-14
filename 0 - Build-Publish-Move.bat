@echo off

dotnet restore

dotnet build --no-restore -c Release

dotnet nuget push Panosen.Collections\bin\Release\Panosen.Collections.*.nupkg -s https://nuget.panosen.cn/v3/index.json -k 1cd8e026-9715-3c58-aa2c-42cd087c0b88 --skip-duplicate

move /Y Panosen.Collections\bin\Release\Panosen.Collections.*.nupkg D:\LocalSavoryNuget\

pause