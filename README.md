## KasTAS 1.0.98 Readme
[![KasTAS Icon](https://github.com/KarstSkarn/KasTAS/blob/main/kasicon.png?raw=true "KasTAS Icon")](https://github.com/KarstSkarn/KasTAS/blob/main/kasicon.png "KasTAS Icon")
### Readme Index

- [KasTAS 1.0.98 Features](#kastas-1098-features)
- [Common FAQ](#common-faq)
- [File Security Check](#file-security-check)

### KasTAS 1.0.98 Features
Released 14/08/2022.

**KasTAS 1.0.98 Demo Video**

[![Demo Video](https://img.youtube.com/vi/LksHsI8QBbM/0.jpg)](https://www.youtube.com/watch?v=LksHsI8QBbM)

- Full support for a total of 21 Virtual Keys / Buttons.
- Stable use of *.kas* Script files fully tested.
- Includes *.pdf* file with examples and full in detail explaination of the KAS Script.
- Compatible from Windows 7 up to latest Windows 10.
- Both x86 and x64 compatible.
- Includes test functions to check if the target game/emulator is working properly with the virtual keys.
- Fully customizable keys and supports all keys stated in [Microsoft Windows Forms Keys](https://docs.microsoft.com/es-es/dotnet/api/system.windows.forms.keys "Microsoft Windows Forms Keys").
- Includes an example script (*happytest.kas*) made to be run with BGB Emulator test rom. You can download BGB Emulator [here](https://bgb.bircd.org/ "here").
- Built-in syntax checker for KAS Scripts (Read mode).
- Includes Notepad++ basic language package with matching color scheme for *.kas* files.
- Most of configuration is easily editable in two included *.ini* files.
- Written in C# using Visual Studio 2022.

### Common FAQ
- **Is the software specific to a emulator or game?**

No. What KasTAS really does is emulate that you are pushing keys in your keyboard in the choosen pattern. So **it will work in any active window** or in any emulator if you set up the same keys that you defined in your KasTAS program.
**Hint:** It will even work if you set up a pattern of keys like *"H", "E", "L", "O"* to write *"HELLO"* in your notepad.

- **Can the program auto-write a Kas Script file meanwhile I manually play a game?**

No. **The program has not any way to detect your keyboard strokes**. The use of the keyboard is single-direction meaning that it can emulate keystrokes but there is no way to read keystrokes.

- **Can I see the source?**

Not publicly but feel free to request me the source or discuss it using Discord. (Owain#3593)

- **Does it needs to be installed?**

No. The executable is fully standalone. Check the question below for more details.

- **Why the executable is so big?**

I know that the executable turned out bigger than I would like. This is caused by two factors.
First that in this kind of "not really important" programs I preffer standalone versus installable. Thats because in case the user realizes that the program is not what he was looking for, the user can easily remove the whole program by just deleting it without any kind of issue.
Also the other issue is that to do the virtual keystrokes must use Microsoft Windows Forms library despite is a console executable. And Microsoft Windows Forms library is one of the few libraries that cannot be trimmed. So sadly, there is a lot of unused stuff merged in the final executable.

### File Security Check

File **is 100% safe**. You can check the following hashes below. Despite this some minor AntiVirus software can flag it as some kind of threat possibly because the software is not signed and because the pattern of the use of Virtual Keys maybe is triggering some red flags for some AV Software.

Same issue can be easily found even with empty programs arround the internet as you can see [here (Stack Overflow exposing same issue)](https://stackoverflow.com/questions/60340213/what-could-be-causing-virustotal-to-flag-an-empty-program-as-a-trojan "here (Stack Overflow exposing same issue)").

For transparency purposes here you have the hashes for both the *.zip* and directly the executable below.

**KasTAS 1.0.98.zip SHA 256**
`b3bd0ba23c3205e57bc0a5327de38c05bb00140cc113c04029bc19ae11a7bae9`
VirusTotal [link here.](https://www.virustotal.com/gui/file/b3bd0ba23c3205e57bc0a5327de38c05bb00140cc113c04029bc19ae11a7bae9 "link here.")

**KasTAS 1.0.98.exe SHA 256**
`9cb32963df658354d47693b26be8e38cf84b0aab401eeaf2c0b558b692ed35a8`
VirusTotal [link here.](https://www.virustotal.com/gui/file/9cb32963df658354d47693b26be8e38cf84b0aab401eeaf2c0b558b692ed35a8 "link here.")

------------
*By KarstSkarn (Owain Horton) (J.L.S~) 2022*

[![CC Licence](https://i.creativecommons.org/l/by-nc-sa/4.0/88x31.png "CC Licence")](https://creativecommons.org/licenses/by-nc-sa/4.0/ "CC Licence")
