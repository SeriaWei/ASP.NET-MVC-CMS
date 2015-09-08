@echo off
for /f %%a in ('dir Temp\Application\Modules /b') do (
	xcopy Temp\Application\Modules\%%a\bin\Easy.CMS.%%a.dll Temp\Application\bin /S /F /Y
	rd /S /Q Temp\Application\Modules\%%a\bin
	del Temp\Application\Modules\%%a\*.config
	del Temp\Application\Modules\%%a\*.xml
)
@pause