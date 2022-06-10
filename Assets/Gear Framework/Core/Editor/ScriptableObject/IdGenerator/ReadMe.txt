GUIDGeneratorAttribute
- if there is a need to generate assets across different computers, GUIDGeneratorAttribute is the right choice

UniqueIdByCustomTypeAttribute
UniqueIdByTypeAttribute
- are good for projects where all assets are generated on the same computer - because the last generated id is stored locally in the project where the asset was created
- you can see the generators in the Generators directory at the root of the IdGenerator directory
- generator names are generated as Type.ToString() => [Type.Namespace].[Type.Name]
- in reality, I don't have any good arguments for using int instead of string. int is a bit faster and if you have millions of operations in a short period of time (per frame) you may feel the difference, that's it

Examples are available in the Examples directory at the root of the IdGenerator directory
You can create an example assets from Assets/Create/Gear Framework/Examples/IdGenerator/