#!/bin/bash
BASE_DIR=`dirname $0`

dotnet build $BASE_DIR/../backend/src/WebApi
nohup dotnet run --project $BASE_DIR/../backend/src/WebApi > /dev/null &
sleep 5
openapi-generator-cli generate --generator-key api-v1.0
ps aux | grep AnalysisWebApi | grep -v grep | awk '{ print "kill -9", $2 }' | sh
