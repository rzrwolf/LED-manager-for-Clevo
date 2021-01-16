LED manager - for Clevo Laptops (all resellers - xmg, sager, eurocom, etc) with 3-zoned keyboards with LED backlight - P775XX, P870XX, may or may not work on similar laptops.

This is a small standalone tray utility for setting up and saving/restoring LED backlight settings. 
No any "Clevo Control Center" or "Obsidian" software is required for it to work independently with your KB  LED settings.

The program works with your laptop's EC via native clevomof.dll library for EC management for Clevo laptops by sending WMI calls.

Initially it was designed to complement "Obsidian" Keyboard LED utility, which is missing P870TM1 Cover backlight LED settings and due to annoying blue default color of this LED.
And of course - to forget the horrors of original CLEVO CCC package once and for all.
But after some additional coding, all available functions related to backlight from original CCC were implemented.

Prerequisites:
- Clevo CCC based laptop. (Clevo Control Center) V 1.0 (ver 5.xxx) or V 2.0 (1.x.xx). You won't need the CCC itself, just laptop that was supposed to work with it.  
- NET 4.0 packages installed in your system.
- Admin access in order to write necessary registry record and/or clevomof.dll file into your /syswow64 folder (optional, it can be done manually by yourself).

Limitations:
- CCC 3.0 Laptops are not supported (based on different EC and BIOS).
- CCC 2.0 Per-key-RGB laptops are not supported (use different approach and keyboard library to set up, however not impossible to implement, just don't have the machine/hardware to test).
- White LED keyboard laptops are not supported
- P75x Stripe not implemented (contact me if you want to test it)
- Touchpad LED not implemented (same as above - don't even know the model of laptop)
- Keyboard and cover LED settings survive the reboot, but never survive sleep mode and power on-off cycle, returning to default and “our favourite BLUE” – this is Clevo hardware design issue, not software. To keep settings – add program to autostart and keep it in tray for restoring settings after sleep mode. 
If you don’t use sleep mode and rarely power off your laptop – you can simply run it once, setup and close until next power on.

What utility actually does (main features):

- Enabling/disabling KB backlight (on P870XX includes disabling the Cover LED)
- Setting the RGB colors of LEFT, CENTER, RIGHT zones of keyboard backlight, including one-click button for setting all zones (or all zones+cover on P870xx) .
- Setting of P870XX Cover LED RGB color or preset colors.
- Disabling the P870XX cover LED separately from the KB LED (no more annoying light illuminating the whole room at night - just set it to BLACK color).
- Setting of LED backlight intensity – from off to maximum
- Settings of LED backlight timeout – from off to 30min
- Starting KB LED effects – cycling colors, breath, etc (not very useful, but who knows)
- Settings save and restore after power on-off and after sleep mode (if program is started and added to autostart).

Additionally:
- At Start-up program checks registry for clevomof.dll record in winacpi related branch, if there is none – automatically adds it. You will see related messages if something is wrong.
- At Start-up program checks /syswow64/ folder for clevomof.dll file, if there is none – it will automatically unpack it either to necessary folder (assuming admin access) or to the program’s root folder near it’s .exe, so you can add it manually where it belongs.
- Autostart function (uses registry autostart approach)
- OSD function – for KB, Cover LED, timeout, backlight intensity settings (doesn’t work with full screen apps, like games, only desktop)
- Silent mode with no OSD
- Settings reset and defaulting.
- HOTKEYS

HOTKEYS description:

CTRL + NUMPAD / - Cycle KB Timeout setting (0-30min)
CTRL + NUMPAD * - Enable/Disable KB LED (and cover LED on P870XX)
CTRL + NUMPAD - - Reduce LED intensity
CTRL + NUMPAD + - Increase LED intensity
CTRL + NUMPAD 0 – Disable Cover LED (only for P870XX)
CTRL + NUMPAD . – Cycle Cover LED preset colors (only for P870XX)


Software tested and works on:

P870XX - P870TM, P870KM, P870DMx
P775XX – P775TM, P775DMx

May work on some other 3-zoned LED keyboards.
