##
## Path
$project = "../"

Set-Location $project

# Run docfx
docfx.exe metadata
docfx.exe ./docfx.json --serve
