@echo off
for /f %%a in ('dir Temp\Application\Modules /b') do (
	xcopy Temp\Application\Modules\%%a\bin\Easy.CMS.%%a.dll Temp\Application\bin /S /F /Y
	@echo Delete Temp\Application\Modules\%%a\bin
	rd /S /Q Temp\Application\Modules\%%a\bin
	@echo Delete Temp\Application\Modules\%%a\*.config
	del Temp\Application\Modules\%%a\*.config
	@echo Delete Temp\Application\Modules\%%a\*.xml
	del Temp\Application\Modules\%%a\*.xml
)
rd /S /Q Temp\DataBase
xcopy ..\DataBase Temp\DataBase\ /S /F /Y
del Temp\DataBase\Append_GO.cmd
@pause