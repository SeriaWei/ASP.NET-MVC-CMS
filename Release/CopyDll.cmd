@echo off
for /f %%a in ('dir Modules /b') do (
	xcopy Modules\%%a\bin\Easy.CMS.%%a.dll bin\Easy.CMS.%%a.dll /S /F /Y
	rd /S /Q Modules\%%a\bin
	del Modules\%%a\*.config
	del Modules\%%a\*.xml
)
@pause