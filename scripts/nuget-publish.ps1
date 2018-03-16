##
##	Globals
##

## jobs
$jobSingle = $TRUE;

## solution
$project = "SiCo.Utilities.Generics"
$projects = @(
    "SiCo.Utilities.Compression",
    "SiCo.Utilities.Crypto",
    "SiCo.Utilities.CSV",
    "SiCo.Utilities.Generics",
    "SiCo.Utilities.Helper",
    "SiCo.Utilities.I18n",
    "SiCo.Utilities.Json",
    "SiCo.Utilities.Pgsql",
    "SiCo.Utilities.Web.Charts",
    "SiCo.Utilities.Web.Vue"
);

## nuget
$env:Path += ";" + $env:APPDATA + "\NuGet\"

##
##	Functions
##
function PublishNuget($path) {
    Set-Location $path

    # Build project
    dotnet build --configuration Release --no-incremental

    # Create nuuget package
    dotnet pack --configuration Release

    $subpath =  "$($path)\bin\Release"
    $nupkgs = Get-ChildItem $subpath -Filter "*.nupkg"
    if ( $nupkgs -eq $null) 
    {
        continue;
    }

    # Copy nuget
    #Write-Output $subpath
    Set-Location $subpath

    # Binary
    if ($nupkgs.Count -eq 1) {
        $name = $nupkgs;
    }

    if ($nupkgs.Count -gt 1) {
        $name = $nupkgs.Get($nupkgs.Count - 1);
    }

    Write-Output "### Upload Binary"
    Write-Output "Versions: $nupkgs"
    Write-Output "Selected: $name"
    Write-Output "###"
    nuget push $name.name -Source https://www.nuget.org/api/v2/package
}

function PublishAll($array) {
    foreach ($pkg in $projects)
    {
        Write-Output $pkg
        $t = Join-Path $pathSrc $pkg -Resolve;
        PublishNuget($t)
    }
}

##
##	Run
##

$pathScripts = Get-Location;
$pathSolution = Join-Path $pathScripts "..\" -Resolve;
$pathSrc = Join-Path $pathScripts "..\src" -Resolve;

# DowloadNuget
Set-Location $pathSolution 
dotnet restore

if ($jobSingle) {
    $pathProject = Join-Path $pathSrc $project -Resolve;
    Write-Output $pathProject;
    PublishNuget(Join-Path $pathSrc $project -Resolve)
}else {
    PublishAll($projects)
}

Set-Location $pathScripts