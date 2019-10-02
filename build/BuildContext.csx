#load "nuget:Dotnet.Build, 0.7.1"
using static FileUtils;
using System.Xml.Linq;

var owner = "seesharper";
var projectName = "LightInject.Microsoft.Hosting";
var root = FileUtils.GetScriptFolder();
var solutionFolder = Path.Combine(root, "..", "src");
var projectFolder = Path.Combine(root, "..", "src", projectName);

var testProjectFolder = Path.Combine(root, "..", "src", $"{projectName}.Tests");

var artifactsFolder = CreateDirectory(root, "Artifacts");
var gitHubArtifactsFolder = CreateDirectory(artifactsFolder, "GitHub");
var nuGetArtifactsFolder = CreateDirectory(artifactsFolder, "NuGet");

var coverageArtifactsFolder = Path.GetFullPath(CreateDirectory(artifactsFolder, "TestCoverage"));

var pathToReleaseNotes = Path.Combine(gitHubArtifactsFolder, "ReleaseNotes.md");

var version = ReadVersion();

string ReadVersion()
{
    var projectFile = XDocument.Load(Directory.GetFiles(projectFolder, "*.csproj").Single());
    var versionPrefix = projectFile.Descendants("VersionPrefix").SingleOrDefault()?.Value;
    var versionSuffix = projectFile.Descendants("VersionSuffix").SingleOrDefault()?.Value;
    var version = projectFile.Descendants("Version").SingleOrDefault()?.Value;

    if (version != null)
    {
        return version;
    }


    if (versionSuffix != null)
    {
        return $"{versionPrefix}-{versionSuffix}";
    }
    else
    {
        return versionPrefix;
    }
}