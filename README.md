# MirroredChecker

A small C# command for Autodesk Revit that finds and selects mirrored family instances in the active document.

I wrote it as a hiring assignment for my first job. I am keeping the source as a record of that work. The project is no longer maintained, and I do not plan to return to it.

## What the command does

The command collects non-type elements from the active document and checks `FamilyInstance.Mirrored` on family instances.

When it finds matches, it selects them in Revit and opens a dialog listing each instance's name, ID, category and family. If there are no matches, it shows a message instead.

Its scope is family instances in the active document. The implementation changes the selection; it does not modify the model geometry.

## Repository contents

| File | Purpose |
| --- | --- |
| [`src/MirroredChecker.cs`](src/MirroredChecker.cs) | The `MirroredChecker.DetectMirrored` external command |
| [`MirroredChecker.addin`](MirroredChecker.addin) | Revit command registration manifest |
| [`MirroredChecker.dll`](MirroredChecker.dll) | Included compiled assembly |

## Runtime and build context

The original project targeted Revit 2026. The included DLL records a .NET 8 target (`.NETCoreApp,Version=v8.0`), not .NET Framework 8.0.

This repository does not include a `.csproj`, solution file, dependency setup or automated tests. Rebuilding requires recreating a project and supplying the appropriate Revit API references. There is no working `dotnet build` command provided by this checkout.

Running the command requires a compatible Revit installation and a registered add-in manifest. The supplied manifest names `MirroredChecker.dll` without an absolute path. When setting up a local registration, point its `Assembly` entry to the DLL's actual location and follow [Autodesk's add-in registration documentation](https://help.autodesk.com/cloudhelp/2026/ENU/Revit-API/files/Revit_API_Developers_Guide/Introduction/Add_In_Integration/Revit_API_Revit_API_Developers_Guide_Introduction_Add_In_Integration_Add_in_Registration_html.html).

The included binary has not been rebuilt or verified against current Revit updates. This is a historical hiring assignment, with no further development planned.
