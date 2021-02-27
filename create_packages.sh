#!/bin/sh

Compress()
{
  DIR=$1
  FILENAME=$2

  WD=$(pwd)
  cd $DIR
  zip -q -r ../$FILENAME *
  cd $WD
  rm -rf $DIR

  # The following is not a good alternative as
  # it flattens the hierarchies and errors out
  # if two files with same name under different
  # directories exist.
  #zip -j -q -r $FILENAME $DIR
}

# ----------------------------
# Make Self-contained packages
# ----------------------------
dotnet publish CLI/CLI.csproj --output ./packages/mspc --no-self-contained -p:UseAppHost=True
Compress ./packages/mspc mspc.zip

# ----------------------------
# Make Self-contained packages
# ----------------------------
# Runtime Identifiers
# See the following page for a complete list of Identifiers:
# https://docs.microsoft.com/en-us/dotnet/core/rid-catalog
for RID in "win-x64" "osx-x64" "linux-x64"
do
  dotnet publish CLI/CLI.csproj --output ./packages/$RID --runtime $RID --self-contained true -p:UseAppHost=True -p:PublishSingleFile=true
  Compress ./packages/$RID $RID.zip
done
