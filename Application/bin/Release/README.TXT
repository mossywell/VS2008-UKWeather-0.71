Mossywell's UK Weather README.TXT
=================================
Latest version: 0.71 (13 April 2008)


Contacts:
=========
Send any emails, suggestions, corrections etc. to:
ukweather@mossywell.com. (See Disclaimer 1!)


Disclaimers:
============
1. I don't provide technical support for this app as it's
   freeware. (Naturally, following in Microsoft's footsteps, if
   you bung me a substantial amount of cash, I'll support it!)
2. If it destroys, messes up, or generally causes problems with
   your PC, I'm not responsible for it, so don't try suing me.
   I really can't imagine that it would do that because it's a
   simple .Net 1.1 app, but Microsoft get away with such
   disclaimers, so I'm doing the same. :-)


General Notes:
==============
1. UK Weather requires at least Microsoft .Net Framework 2.0.


Release Notes:
==============
Version 0.71 (13 April 2008)
---------------------------
- FIX: Fixed a bug whereby the negative symbol wasn't being
  parsed.
- CHANGE: Keep-Alive changed from false to true. This is because
  the server also returns a keep-alive header.
- CHANGE: The code in the WebUtils DLL has been incorporated into
  the executable as it will never get used anywhere else!


Version 0.70 (05 April 2008)
---------------------------
- It's been a long time (nearly 3 years) since the last update
  but Weather.com finally made some big changes to their web
  site. As a result, I've had to do a bit of recoding of the
  web parser. Sorry it's taken so long - I had to refamiliarise
  myself with the code. (Good thing it's heavily commented!)


Version 0.62 (10 June 2005)
---------------------------
- Changed the default URL as it has been at the new value long
  enough and doesn't look like changing back to the old one.
- Add the URL that is current in use to the About.. screen.
- Changed the way that the data is parsed to correct for the
  fact that the source web site is putting a comma in the
  middle of the temperature!


Version 0.61 (21 February 2005)
-------------------------------
- Changed the installation routine to create a folder in the
  Programs folder of the Start Menu.
- Fixed the bug whereby uninstalling UK Weather whilst it was
  running failed to stop it first.
- Added the Farenheit temperature scale as an option. NOTE:
  the Temperature in Farenheit is calculated from the Celsius
  temperature rather than obtained directly from the web site.
  Therefore, they may be slightly different. This is because
  it would require a significant change in the code to obtain
  the Farenheit temperature directly from the web site, so I
  opted for this simple "fix" instead!
- Corrected a few typos in the Help file.
- Some behind-the-scenes coding changes to make future changes
  easier.

Version 0.60 (11 November 2004)
-------------------------------
- Added a configurable flashing icon frequency.
- Added a new option to flash the system tray icon continuously.
  That is, if you move the mouse over, the icons continues to
  flash. (It was originally put in for debugging purposes, but
  I decided to leave it in as someone might like it.)
- The part of the application that gets the data now runs as
  a separate thread, so the user interface is now more
  responsive when there is no network connection. (This change
  resulted in a major restructuring of UK Weather.)
- Added an "OK and Update Now" button to the Options form,
  giving the user the choice of simply accepting the change but
  allowing UK Weather to update in its own time, or forcing an
  update immediately.
- UK Weather now does a version and data URL check before
  loading, which can  be disabled, and prompts to download a
  new version if one exists, which can also be disabled.
- All dialogs should now appear centre screen.

Version 0.51 (23 September 2004)
--------------------------------
- Change the "bad postcode" icon to the letter "P" and the
  "no network" icon to the letter "N". The question mark only
  appears when UK Weather is first fired up. This prevents any
  confusion as to what the question mark actually means.
- System tray icon display now shows as the letter "X" if the
  web site returns data, but the data doesn't contain the
  temperature. This happens sometimes when the web site is
  having problems and is showing a temperature of "N/A".
- Changed how the code works out the executable's path. The
  normal way (Environment.CurrentDirectory) sometimes returned
  unexpected results, such as "Documents and Settings" in the
  "About..." window.
- Fixed the problem where UK Weather claims to have no network
  connection even when it does.
- No longer bombs out if there's no Event Log (as in older
  versions of Windows).
- Fixed the problem of the UK Weather throwing an exception if
  left running for many days.
- Corrected a couple of typos in the help files.

Version 0.50 (19 September 2004)
--------------------------------
- Finally got around to doing a readme. :-)
- Fixed problem where double clicking the icon launches Internet
  Explorer, even if IE isn't the default browser. Note that it
  is coded such that any extant open browsers won't be hijacked.
  That is, a new browser session is always opened.
- Changed what happens if the network connection is lost. In
  previous versions, if a network connection was lost, the icon
  would revert to a "?" (and the icon's popup text would
  complain about no data being obtainable). Now, provided that
  it has managed to obtain data successfully and that the postcode
  isn't changed by the user, it will now just retain the old
  values. It will, however, log the loss of connectivity in the
  Event Log.
- Loss of network is now only a warning in the Event Log, not an
  error.
- Changed the background colour of the "Colour Scheme" to the
  system default. Previously, it was XP's default blue.
- I think I've fixed the problem where the processor leaps to
  100% in a multi-user (Terminal Services / Citrix)
  environment. The problem was intermittent, however, so it's
  difficult to be sure that it's fixed.
- The desktop icon can now be installed into the user's
  profile or the "all users" profile.

Version 0.42 (24 May 2004)
--------------------------
- First release loaded into the web site. Why 0.42? Because that
  just happened to be the latest version when I asked my pals to
  test it out!


Known Bugs:
===========

Version 0.70:
- None known.


Possible Future Changes:
========================

1. Add a configurable timeout to the web request. At present,
   the timeout is hardcoded to 20 seconds.
2. Allow custom proxy settings. At present, UK Weather uses the
   settings as specified in IE (NOT the default browser as the
   old readme used to say).
3. Make the icon change immediately if the temperature scale is
   changed.
4. Internal reorganisation of the way the main form loads the
   first popup. (IMHO, it should be the other way round.)



