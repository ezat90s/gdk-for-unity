#!/usr/bin/env bash

set -e -u -o pipefail

if [[ -n "${DEBUG-}" ]]; then
    set -x
fi

cd "$(dirname "$0")/../"

source ".shared-ci/scripts/pinned-tools.sh"

traceStart "Building deployment launcher :shrek:"
    dotnet build workers/unity/Packages/io.improbable.gdk.deploymentlauncher/.DeploymentLauncher/DeploymentLauncher.csproj | tee logs/deployment-launcher-build.log
traceEnd
