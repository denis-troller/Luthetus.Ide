﻿namespace Luthetus.Ide.ClassLib.WebsiteProjectTemplates.ConsoleAppCase;

public partial class ConsoleAppFacts
{
    public static string GetCsprojContents(string projectName) => @$"<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
";
}
