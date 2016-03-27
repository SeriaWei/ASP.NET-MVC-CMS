@echo off
@echo -----------------------------------------------------------------------------
@echo This is an tool that append "GO" to InitialData scripts.
@echo -----------------------------------------------------------------------------
@pause
for /f %%a in ('dir InitialData /b') do (
echo File:%%a%
echo GO>>InitialData\%%a%
)
@echo Complete
@pause