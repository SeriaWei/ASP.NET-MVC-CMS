*.csproj  Ìí¼Ó

<UsingTask TaskName="CopyTask" AssemblyFile="bin\MSBuild.CopyTask.dll" />
  <Target Name="AfterBuild">
    <CopyTask />
  </Target>