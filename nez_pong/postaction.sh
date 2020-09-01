# this script is here to un-fuck the Nez repo
# since the dotnet template installer seems to like ripping out big chunks of it

cd Nez
git add .editorconfig *.csproj
git commit -m "fix dotnet template's mess"
git reset HEAD --hard

cd ..
dotnet restore
dotnet build
