#!/bin/bash

cd ../

# git pull --no-ff
git pull

cd code/

sudo service ticketing_api stop

export ASPNETCORE_ENVIRONMENT=StrahlenDev
sudo dotnet build
cd src/Frontend/
sudo dotnet publish -c Release

sudo service ticketing_api start
