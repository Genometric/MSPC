
function Compress($rid)
{
	$archive = ".\packages\$rid.zip"
	if (Test-Path $archive)
	{
		Remove-Item $archive
	}

	Compress-Archive -Path .\packages\$rid\*.* $archive
	Remove-Item -LiteralPath ".\packages\$rid\" -Force -Recurse
}

# ----------------------------
# Make Self-contained packages
# ----------------------------
dotnet publish .\CLI\CLI.csproj --output ./packages/mspc/ --no-self-contained
Compress("mspc")

# ----------------------------
# Make Self-contained packages
# ----------------------------
# Runtime Identifiers
# See the following page for a complete list of Identifiers:
# https://docs.microsoft.com/en-us/dotnet/core/rid-catalog
$rids = @("win-x64","osx-x64","linux-x64")
foreach ($rid in $rids) 
{
	dotnet publish .\CLI\CLI.csproj --output ./packages/$rid/ --self-contained true --runtime $rid
	Compress($rid)
}
