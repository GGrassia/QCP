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

## How to use QCP

1. First and foremost, edit the default folder of the "startup.json" file in the json folder to the path of the folder you want to tidy up.

>Nothing will work otherwise!

2. Run QCP!

### For lazy people (I feel you guys)

- You could skip editing the startup json, but it would mean you have to press some buttons, and insert a path manually, you don't want that, do you?

- In the startup json edit to "true" both the silent and/or startup (only on W10 for the time being) fields.

- Done! In the path given to the "startup.json" you'll find some new folders with the files neatly divided between them based on the file type. Keep in mind there are default categories for the most used file types, every other less used file will be in a "Misc" folder.

### For configuration junkies

I'll get to it... Meanwhile if you read the settings.json file you might get an idea of how it works...

## How to install

### Compiling

Clone the repo and compile from source. Only thing needed is the .NET 5.0 SDK.

### W10

Unzip in a folder you will remember, done.

### MacOS

Move QCP.app into the Applications folder, done.

### Linux

Welp, I'm an ass. QCP! was developed on linux, yet I haven't done/tested a release for it.

.deb, .rpm and an AUR script are expected to hit release at some point, but I have no idea when I'll get to it.

## Future features

1. Automatic start at login on Mac OSX and Linux (Systemd only, if you are not using it you're perfectly capable of setting it up yourself, am I right?)

2. No idea, so requests and ideas are more than welcome!

## Requests

>Do you have ideas for new features or have you found bugs/nonsense in the code? Pull requests and ideas, as already said, are highly appreciated!

## Big thank you to

### The senpai

Testo @scarfacetheduke, who got me into coding and C#.

### The betatesters

"ILMASTAH" and Dario who endured my tiring messages and proud boastings of broken code.
