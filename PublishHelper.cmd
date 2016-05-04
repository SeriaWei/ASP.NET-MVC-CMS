@echo off

for /f %%a in ('dir Release\Application\Modules /b') do (
	xcopy Release\Application\Modules\%%a\bin\*.dll Release\Application\bin /S /F /Y
	@echo Delete Release\Application\Modules\%%a\bin
	rd /S /Q Release\Application\Modules\%%a\bin
	@echo Delete Release\Application\Modules\%%a\*.config
	del Release\Application\Modules\%%a\*.config
	@echo Delete Release\Application\Modules\%%a\*.xml
	del Release\Application\Modules\%%a\*.xml
)
xcopy DataBase Release\DataBase\ /S /F /Y
del Release\DataBase\Append_GO.cmd