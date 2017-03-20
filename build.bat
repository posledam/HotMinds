@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

call tools\nuget.exe restore "src\HotMinds.sln"

REM Build
"%programfiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe" "src\HotMinds.sln" /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
REM "%programfiles(x86)%\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\MSBuild.exe" "src\HotMinds.sln" /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

REM Package
mkdir Build
call tools\nuget.exe pack "src\HotMinds\HotMinds.csproj" -symbols -o Build -p Configuration=%config% %version%
