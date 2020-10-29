# Ikenfell "Mod" Loader

Very in progress "mod" loader for Ikenfell.

Here "mod" is in quotes because technically this is intended to be a loader for
kind of story packs, or modifications done explicitly to the game _content_, but
not necessarily functionality that already exists.

## Setup

You will need to do a few things before being able to compile stuff:

  - Copy `GameEngine.dll` and `LittleWitch.dll` from your Ikenfell install dir to the `lib` directory.
  - Download [MonoMod][] and exctract everything into your Ikenfell install dir.
  - Copy `MonoMod.exe` to the `lib` directory.
  - Install [Mono][] so you get access to the `mcs` command.

  [MonoMod]: https://github.com/MonoMod/MonoMod
  [Mono]: https://www.mono-project.com/


## Building

If you're on Linux, just run `./build.sh` to compile this.  You'll get the target DLLs in `dist`.  It probably works
on Mac too, but Windows might have problems.  Check the command that's being run inside, you probably can run it from
a regular `cmd` assuming you could install [Mono][] and use the `mcs` command from before.


## Installing

Copy `dist/LittleWitch.Mod.mm.dll` to your Ikenfell install dir, then run `./MonoMod.exe LittleWitch.dll`.
This will generate a `MONOMODDED_LittleWitch.dll` file which you should rename to just `LittleWitch.dll`, and that's it.

I very much recommend backing up the original DLL before renaming the monomod one so you can use it again. If you don't,
do a "Verify local cache" on Steam to get it back unmodded.