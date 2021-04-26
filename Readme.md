# QCP!  - On startup folder organizing

## The name:_"Questa Cartella è un Porcile!"_

Roughly translating to "This folder is a bloody mess!", QCP! gets its name from a common phrase yelled by Italian mothers when their offspring's room is... Not tidy.
The original phrase obviously hints at a room, not a folder, and "porcile" means pigsty, I'll leave you to your own conclusions.

## Features

- **Sort files based on extension**

> Since both Windows and OSX use extensions to know how to open a file, I figured it would be simpler to use the same criteria to sort the files.

- **Json based configuration**

> Easy to read, check, edit and customize.

- **Automatic folder creation based on file type**

> Because sometimes, you need to be shown order to start thinking tidy.

### Why not a background running script?

Because I find real time sorting unproductive. If I've downloaded something, the first place I'll look for is the Downloads folder.
Having to think what kind of file I have downloaded and then search for the corresponding folder means to rewire the brain, why do it when you can just cleanup each time you open up your computer?

## How to use QCP, for everyone

1. Download the .zip for your OS of choice.

2. Unzip the downloaded file in a place you will remember, specially if you are on OSX or any Linux distro.

> Json files are a kind of text file which is easy to read both for you and the computer.

4. Run QCP! Next section descrives OS specific course of action.

> Warning, if you have set nothing up and try to use defaults it will crash.

### On Windows

Just running QCP.exe and saying "run anyway" to windows should be enough.

### On Linux

Navigate to the QCP folder and type `./QCP`

### On OSX

***Sigh*** Apple forces you to a really tiring game of launch, open anyway (from gatekeeper), open again and again...

> I don't like it, but Apple has this protection method... If the project gets a lot of interest I might consider buying an apple certified developer stuff to avoid all this. Every donation will go towards that specific goal until completed.

## For lazy people (I feel you guys)

- In the jsons folder inside the QCP folder there is a file called "startup.json", open it with a text editor.

- ***You could skip editing the startup json, but it would mean you have to press some buttons, and insert a path manually EVERY TIME, you don't want that, do you?***
Well then, in the "DefaultFolder" field there should be some text, delete it and put the path of the folder you want to keep tidy.

> Since I myself also had a little trouble with finding the path, on OSX you can copy the path from the Finder, just select the folder and then click on "copy xyz as Pathname" from the Finder's edit menu on the topbar. You can paste it later in the terminal with cmd+V. You're welcome!

- In the "startup.json" edit to "true" the silent and/or startup (only on W10 for the time being) fields.

- Done! After launching QCP, in the path given to the "startup.json" you'll find some new folders with the files neatly divided between them based on the file type. Keep in mind there are default categories for the most used file types, every other less used file will be in a "Misc" folder.

### For configuration junkies

All right. You want to know stuff eh?
Well, basically it's all in the settings.json file. (It will be defaultSettings.json in the folder).

First and foremost, backup the default file, in case you break something.

- The settings are divided in blocks which hold the information QCP needs to work:

1. "Name": which is the name of the generic folder QCP will create based on that file type group, also serves as a quick way of knowing what grouping are you looking at.

2. "Extensions": these are the extensions linked to the filetypes, divided by commas. If you want to move, add or remove an extension make sure you don't leave double commas or spaces in the file. If you want pdfs with pictures and not text documents, editing this file you can.
You can add custom filetype groups or remove some, this is where you can fully configure the software.

3. "Folder": here you can put the custom path for a filetype group. QCP won't make a new folder if you have given it a valid custom path (checks if the folder exists to know it), but will just move the files there.

- Now that you have configured how you want QCP to behave, follow the lazy people guide so it can run smoothly even for you.

- Don't forget to put the customized (if you haven't simply modified the DefaultSettings.json file) file path inside the "startup.json" one!

## How to install

### Compiling

Clone the repo and compile from source. Only thing needed is the .NET 5.0 SDK.

### Downloading

Choose the release zip corresponding to your OS, unzip somewhere you will remember.

## Future features

1. Automatic start at login on Mac OSX and Linux.

2. MacOSX Certified developer signature, just to bypass the Apple BS on running the app.

3. No idea, so requests and ideas are more than welcome!

## Requests

> Do you have ideas for new features or have you found bugs/nonsense in the code? Pull requests and ideas, as already said, are highly appreciated!

## Big thank you to

### The senpai

@scarfacetheduke, who got me into coding and C#.

### The betatesters

"ILMASTAH" and Dario, friends who endured my tiring messages and proud boastings of broken code.
