echo off

nuget pack -Prop Configuration=Release

FOR /F %%I IN ('DIR *.nupkg /B /O:D') DO SET NEWEST_FILE=%%I

echo Newest package: %NEWEST_FILE%


@pause