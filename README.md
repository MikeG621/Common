# [Idmr.Common.dll](https://github.com/MikeG621/Common)

Author: [Michael Gaisser](mailto:mjgaisser@gmail.com)  
![GitHub Release](https://img.shields.io/github/v/release/MikeG621/Common)  
![GitHub License](https://img.shields.io/github/license/MikeG621/Common)

Library providing common types and aliases used throughout IDMR software.

## Latest Update
#### v1.3.1 - 12 Jul 2016
 - (GraphicsFunctions) Fixed ConvertTo1bpp's implementation of transparent

 ---
### Additional Information

#### Instructions
 - Add System.Drawing to your references.

Programmer's reference can be found in the [help file](help/Idmr.Common.chm).

### Version History
#### v1.3 - 14 Dec 2014
 - (ArrayFunctions) ASCII Encodings replaced with UTF8
 - (FixedSizeCollection) serializable; added IsModified and Tag
 - (ObjectCopier) new
 - (ResizableCollection) serializable; added Capacity, SetCount, IsModified; Add and Insert are now virtual

#### v1.2 - 24 Oct 2012
 - (ArrayFunctions) added WriteToArray(short[], Array, ref int) overload
 - (FixedSizeCollection) Fixed a bug checking against Capactiy instead of Count
 - (GraphicsFunctions) ConvertTo8bpp{Bitmap, Color[]) can now function when given a null array
 - (ResizableCollection) Unrestricted ItemLimit now allowed

---
#### Copyright Information
Copyright (C) Michael Gaisser, 2007-2024
This library file and related files are licensed under the Mozilla Public License
v2.0 or later.  See [License.txt](License.txt) for further details.