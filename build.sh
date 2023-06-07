#!/bin/bash

set -e -o

function version_compare() {
    ver1=(${1//./ })
    ver2=(${2//./ })

    len1=${#ver1[@]}
    len2=${#ver2[@]}

    vlen=$(($len1 < $len2 ? $len1 : $len2))
    
    for ((i=0;i<vlen;i++))
    do
        if [ ${ver1[$i]} -gt ${ver2[$i]} ]; then
            return 1
        fi
    done
    return 0
}

MINIMAL_DOTNET_VERSION=6.0.200

EXIT_CODE=0

CURRENT_DOTNET_VERSION=$(dotnet --version 2> /dev/null) || EXIT_CODE=$?

if (($EXIT_CODE == 0)) && version_compare "$MINIMAL_DOTNET_VERSION" "$CURRENT_DOTNET_VERSION"; then
    echo "dotnet command already installed"
else
    # Install .NET Core (https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-install-script)
    curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --version 3.1.100

    PATH="~/.dotnet:$PATH"
fi

FSIARGS=""
OS=${OS:-"unknown"}
if [[ "$OS" != "Windows_NT" ]]
then
  FSIARGS="--fsiargs -d:MONO"
fi

function run() {
  if [[ "$OS" != "Windows_NT" ]]
  then
    mono "$@"
  else
    "$@"
  fi
}

run .paket/paket.bootstrapper.exe

if [[ "$OS" != "Windows_NT" ]] &&
       [ ! -e ~/.config/.mono/certs ]
then
  mozroots --import --sync --quiet
fi

dotnet tool restore
dotnet fake run $FSIARGS build.fsx $@