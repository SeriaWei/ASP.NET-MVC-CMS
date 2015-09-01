@echo off
@echo -----------------------------------------------------------------------------
@echo This is an tool that append "GO" to InitialData scripts.
@echo -----------------------------------------------------------------------------
@echo Do you want to continue?[y/n]
set /p isStart=
if "%isStart%" NEQ "y" goto done
for /f %%a in ('dir InitialData /b') do (
echo File:%%a%
echo GO>>InitialData\%%a%
)
:done
@pause