﻿#region USING
using Path = System.IO.Path;
using static CakeScript.CakeAPI;
using static CakeScript.Startup;
#endregion


namespace CakeScript;

partial class Program
{
    [DependsOn(nameof(PrepareNuget))]
    public void FastReportLocalization()
    {
        const string projName = "FastReport.Localization";
        string packDir = PackDir;
        string packFRLocalizationDir = Path.Combine(packDir, projName);
        string localizationDir = Path.Combine(solutionDirectory, "Localization");

        if (!Directory.Exists(localizationDir))
            throw new Exception("'Localization' directory wasn't found on path: " + localizationDir);

        string tempDir = Path.Combine(packFRLocalizationDir, "tmp");
        if (DirectoryExists(tempDir))
        {
            DeleteDirectory(tempDir, new DeleteDirectorySettings
            {
                Force = true,
                Recursive = true
            });
        }
        CreateDirectory(tempDir);

        Information($"{Environment.NewLine}FastReport.Localization pack...", ConsoleColor.DarkMagenta);

        var packFiles = new[] {
            new NuSpecContent{Source = Path.Combine(packDir, FRLOGO192PNG), Target = ""},
            new NuSpecContent{Source = Path.Combine(packDir, MIT_LICENSE), Target = ""},
            new NuSpecContent{Source = Path.Combine(packFRLocalizationDir, "**", "*.*"), Target = ""},
            new NuSpecContent{Source = Path.Combine(localizationDir, "*.frl"), 
                Target = Path.Combine("build", "Localization")}
        };

        // generate nuspec
        var nuGetPackSettings = new NuGetPackSettings
        {
            Id = projName,
            Authors = new[] { "Fast Reports Inc." },
            Owners = new[] { "Fast Reports Inc." },
            Description = "FastReport.Localization includes localization files for FastReport .NET, FastReport.Core, FastReport.WPF, FastReport.Mono and FastReport.OpenSource",
            ProjectUrl = new Uri("https://www.fast-report.com/en/product/fast-report-net"),
            Icon = FRLOGO192PNG,
            License = new NuSpecLicense { Type = "file", Value = MIT_LICENSE },
            Tags = new[] { "fastreport", "localization" , "frl"},
            Version = version,
            BasePath = tempDir,
            OutputDirectory = GetOutdir(projName),
            Files = packFiles,
        };

        // pack
        NuGetPack(nuGetPackSettings);
    }

}
