
function Compress($rid)
{
	$archive = ".\packages\$rid.zip"
	if (Test-Path $archive)
	{
		Remove-Item $archive
	}

	Compress-Archive -Path .\packages\$rid\* $archive -CompressionLevel Optimal
	Remove-Item -LiteralPath ".\packages\$rid\" -Force -Recurse
}

# ----------------------------
# Make Self-contained packages
# ----------------------------
dotnet publish .\CLI\CLI.csproj --output ./packages/mspc -p:UseAppHost=True
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
	dotnet publish .\CLI\CLI.csproj --output ./packages/$RID --runtime $RID --self-contained true -p:UseAppHost=True -p:PublishSingleFile=true
	Compress($rid)
}
