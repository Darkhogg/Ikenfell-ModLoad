#!/bin/bash

function CompileDll () {
    mcs -target:library \
        -r:lib/GameEngine.dll \
        -r:lib/LittleWitch.dll \
        -r:lib/MonoMod.exe \
        -out:dist/$1.Mod.mm.dll \
        src/$1/*.cs
}

CompileDll LittleWitch
#CompileDll GameEngine