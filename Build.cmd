@echo off

@IF NOT EXIST %windir%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe (GOTO :noBuildengine) ELSE GOTO :execute
:execute
set MSBuildEmitSolution=1
set BuildConfiguration=Release
%windir%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe Easy.CMS.Web.sln 
@pause
:noBuildengine
