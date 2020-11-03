# Phasmophobia MelonLoader Mod (Outdated when beta branch goes live)

Just an private project to learn some basic programming of Unreal Engine Mods with the help of MelonLoader.  
Place the generated DLL into the MelonLoader Mods folder.   


**Features**  
\- Simple Box ESP  
\- Bone/OuijaBoard/FuseBox ESP  
\- Fullbright Mode  
\- Basic Ghost Informations (Ghost Name/Type/State, responds to)  
\- Revealed Evidence  
\- Own Insanity  
\- Active and completed Missions  
\- Console Window for Logging (MelonLoader)  
\- Ghost actions: Hunt/Idle/Appear/Disappear  
\- Close+Lock/Open+Unlock Exit/All doors


**Hotkeys**  
Up Arrow = ESP  
Down Arrow = Fullbright  
Left Arrow = Information Box  
Right Arrow = Trolling UI  
Insert = Cheat UI  
Delete = Enable debug output
H = Force ghost to hunt
I = Force ghost to interact with Door/Book/Objects
O = Force ghost to appear
P = Force ghost to stop hunting / appearing
L = Lock all exit doors
U = Unlock all exit doors


**Screenshots**  
\- [Mod v.4.3](Images/v4.3.png)  
\- [Mod v.4.2](Images/v4.2.png)  
\- [Mod v.4](Images/v4.png)  
\- [Mod v.3](Images/v3.png)  
\- [Mod v.2](Images/v2.png)  
\- [Mod v.1](Images/v1.png)


# How to build
1. [Install MelonLoader](https://melonwiki.xyz/#/README)
2. Start the game without Mod, only with MelonLoader. ML will download stuff do handle the IL2CPP from Phasmophobia
3. Modify `PhasmoMelonMod.csproj` and edit all  
```<Reference Include="Assembly-CSharp"><HintPath>**.dll</HintPath></Reference>```  
to link to your Steams Phasmophobia directory
4. Compile (Release) and move the `obj\Release\PhasmoMelonMod.dll` to the Mod folder inside your Phasmophobia directory



# Credits
**Fullbright:** `ShieldSupporter` for sharing the code from `Plagues`  
**2D Box ESP:** `EBro912`  
**Others:** `HYPExMon5ter`


# License
**GNU General Public License 3**
