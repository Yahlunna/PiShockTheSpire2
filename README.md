# PiShockTheSpire2

> [!CAUTION]
> The contents of this repository are related with the use of an adult themed toy with the explicit purpose of inflicting pain. Viewer discretion is advised.

> [!Warning]
> This mod is in beta. Some errors are expected to happen while using it. Feel free to report any issues or give any feedback you want.

Source code for the PiShock The Spire 2 mod.


## Quick Setup Guide for PiShockTheSpire2 

> [!Note]
> If you play on steam, you can download PiShock The Spire 2 directly from the Workshop: [Click here](https://steamcommunity.com/sharedfiles/filedetails/?id=3747608882).

- **Step 0 (optional):** Set your game in the **public-beta** branch.

We recomend playing in the stable version of the mod, but if you wish to play the beta version of the game, the mod will have a beta-branch release version to support it.

- **Step 1:** Download the intended version of the mod listed in the [Releases section](https://github.com/Yahlunna/PiShockTheSpire2/releases).

Select between "Stable version" if you are playing normaly, or "Beta-version" if you wish to play in the beta version of the game. Keep in mind the beta branch is always changing, and its prone to break stuff! If beta-branch isnt working, please use the stable version until the beta version us properly updated (this could take a few days)
This mod uses BaseLib as a dependency. You can download BaseLib from [here](https://github.com/Alchyr/BaseLib-StS2). 

- **Step 2:** Install the mod.

First, browse into your StS2 installation folder. Once there, create a "mods" folder.

Extract the files from "PiShockTheSpire2.zip" into this folder. You will also need to keep BaseLib here. A successful instalation should look like this:

![](https://i.imgur.com/uQXrXvu.png)

**Step 2:** Execute the game and make sure the mod is running:
You can see your list of mods at `Settings > General > Modding`, by clicking in the Mod Settings button.

![](https://i.imgur.com/sMEKwjR.png)

This should look similar to this:

![](https://i.imgur.com/AOzuDsT.png)

> [!Warning]
> If you mod Slay The Spire 2, your save file will be safely stored and you will start your modded StS2 with a different one.
> To restore your main save file, simply disable all your mods in the menu above, and reset the game.

**Step 3:** Configure the mod

You can now open the Mod Settings menu and configure PiShock The Spire 2 to link it with a PiShock Shocker

The configuration menu will looks similar to this:

![](https://i.imgur.com/ju8EZS3.png)

You will need to fill 3 parameters in order to run this mod: your **Username**, an **API Key** for your account, and the **ID of the Shocker** you want to use.

You can find your **Username** and how to generate API Keys in your PiShock Account.

You can find your **Shocker ID** in the PiShock Vault. Please, keep in mind you will need to use your Shocker ID, **not** your Hub ID.

![](https://i.imgur.com/kufZzvc.png)

Feel free to adjust all the other given parameters to your liking to adapt the mod to your prefered expereince.


**Step 4 (Optional):** Debugging

You can use the Test current Shocker button in The PiShockTheSpire2 menu to check if the Shoker has linked properly with the game.
Pressing the test button will try make the Shocker vibrate at the max intensity and duration that you have configured.

If your shocker is not working properly pr you want to report an issue with the mod, please send me your ingame logs (you can find them at `(...)\AppData\Roaming\SlayTheSpire2\logs`, under the name of godot.log

**Advanced:** You can also manually debug any issue you have in the BaseLib console to check for the API response codes when a Shock operation is requested.

You can open an ingame console with the [`] key. On it, just type:

> showlog  

This command opens a live log dump where you can see any relevant debug information for the game, BaseLib, PiShockTheSpire2 and other mods. Feel free to use it to track any API issue you might be experiencing.

The command also opens your ingame log folder automatically with the following command:

> open logs

This way, you dont need to search for your SlayTheSpire2\logs folder.

Feel free to use the "Verbose Logs" option in the mod settings in order to find additional info on where the mod might be experiencign issues!
