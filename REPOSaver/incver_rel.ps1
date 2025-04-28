$csProjFile = Resolve-Path "REPOSaver.csproj"
$gameVerFile = Resolve-Path "gamever.txt"

# Find the current game version.
$gameVersionRaw = Get-Content $gameVerFile
$gameVersion = [System.Version]$gameVersionRaw

# Get last version from project xml.
[xml]$xml = Get-Content $csProjFile
$toolVersion = [System.Version]::new($xml.Project.PropertyGroup.AssemblyVersion)
$newVersion = $toolVersion

# Increment the revision unless the game version is different, in which case we set the revision back to 1.
if ($gameVersion.Major -eq $toolVersion.Major -and $gameVersion.Minor -eq $toolVersion.Minor -and $gameVersion.Build -eq $toolVersion.Build) {
	$newVersion = [System.Version]::new($toolVersion.Major, $toolVersion.Minor, $toolVersion.Build, $toolVersion.Revision + 1)
}
else {
	$newVersion = [System.Version]::new($gameVersion.Major, $gameVersion.Minor, $gameVersion.Build, 1)
}

# Save the project xml.
$xml.Project.PropertyGroup.AssemblyVersion = $newVersion.ToString()
$xml.Save($csProjFile)

Write-Output "Updated Version from $($toolVersion) to $($newVersion)"