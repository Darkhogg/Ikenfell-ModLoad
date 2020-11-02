#!/bin/bash
set -e

function CompileDll () {
    printf '\x1B[;1;32m  -> \x1B[;1mCompiling "%s"...\x1B[m\n' "$1.Mod.mm.dll"
    mcs -target:library \
        -r:lib/GameEngine.dll \
        -r:lib/LittleWitch.dll \
        -r:lib/MonoMod.exe \
        -out:dist/"$1.Mod.mm.dll" \
        src/"$1"/*.cs
}

printf '\x1B[;1;33m==> \x1B[;1mBuilding...\x1B[m\n'
CompileDll LittleWitch
CompileDll GameEngine