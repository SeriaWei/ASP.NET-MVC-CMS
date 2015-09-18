@echo off

@IF NOT EXIST %windir%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe (GOTO :noBuildengine) ELSE GOTO :execute
:execute
rd /S /Q Release
%windir%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe Easy.CMS.Web.sln /p:Configuration=Release
%windir%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe Release.xml /p:Configuration=Release;SolutionName=Easy.CMS.Web;ProjectName=Easy.CMS.Web.csproj;ProjectPath=Easy.CMS.Web;ReleasePath=Release\Application /v:m

for /f %%a in ('dir Easy.CMS.Web\Modules /b') do (
	cd 	Easy.CMS.Web\Modules\%%a
	for /f %%b in ('dir /b *.csproj') do (
		cd ../../../
		%windir%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe Release.xml /p:Configuration=Release;SolutionName=Easy.CMS.Web;ProjectName=%%b;ProjectPath=Easy.CMS.Web\Modules\%%a;ReleasePath=Release\Application\Modules\%%a\	 /v:m
	)	
)
@echo Release Success
for /f %%a in ('dir Release\Application\Modules /b') do (
	xcopy Release\Application\Modules\%%a\bin\Easy.CMS.%%a.dll Release\Application\bin /S /F /Y
	@echo Delete Release\Application\Modules\%%a\bin
	rd /S /Q Release\Application\Modules\%%a\bin
	@echo Delete Release\Application\Modules\%%a\*.config
	del Release\Application\Modules\%%a\*.config
	@echo Delete Release\Application\Modules\%%a\*.xml
	del Release\Application\Modules\%%a\*.xml
)
xcopy DataBase Release\DataBase\ /S /F /Y
del Release\DataBase\Append_GO.cmd
@echo Release success...
@pause
goto finish
:noBuildengine
@echo no build engine.
@pause
:finish