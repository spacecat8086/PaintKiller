# PaintKiller
Simple vector graphics editor written in C# using WinForms
## Features
- Plugin support
- Serialization/deserialization in text format
- Custom UI
### How to build and use plugins:
1) Run 'dotnet build' in the 'Modules/Plugin-Name/' folder
2) Create the 'Plugins' folder in the PaintKiller build location and copy Plugin-Name.dll here

Now you can run the app without rebuilding and new shapes will appear in the palette
