$csProjFile = "REPOSaver.csproj"
$gameVerFile = "gamever.txt"

# Find the current game version.
$gameVersionRaw = Get-Content $gameVerFile
$gameVersion = [System.Version]$gameVersionRaw

# Get last version from project xml.
[xml]$xml = Get-Content $csProjFile
$toolVersionRaw = $xml.Project.PropertyGroup.AssemblyVersion
$toolVersionParts = $toolVersionRaw -split '\.'
$major, $minor, $build = $toolVersionParts[0..2]

# Get the revision number without the -pre on the end (which would be the result if we extracted it in the line above).
$null = $toolVersionRaw -match '(\d+)-pre'
$revision = [int]$matches[1]

# Get the pre-version number.
$preVersion = if ($toolVersionRaw -match '-pre(\d+)') { [int]$matches[1] } else { 0 }

# Logic to set the new pre-version.
# Increment the pre version number if the game version has not changed.
# We set both the revision and pre number back to 1 if the game version has changed.
if ($gameVersion.Major -eq $major -and $gameVersion.Minor -eq $minor -and $gameVersion.Build -eq $build) {
	$preVersion++
}
else {
	$major = $gameVersion.Major
	$minor = $gameVersion.Minor
	$build = $gameVersion.Build
	$revision = 1
	$preVersion = 1
}

$newVersion ="$major.$minor.$build.$revision-pre$preVersion"
$xml.Project.PropertyGroup.AssemblyVersion = $newVersion

# Save the project xml.
$xml.OuterXml | Set-Content -Path $csProjFile

Write-Output "Updated Version from $($toolVersionRaw) to $($newVersion)"