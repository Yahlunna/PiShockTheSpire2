# PiShockTheSpire2

> [!CAUTION]
> The contents of this repository are related with the use of an adult themed toy with the explicit purpose of inflicting pain. Viewer discretion is advised.

Source code for the PiShock The Spire 2 mod.

> [!Note]
> The following text is just a wonky temporal installation guide for this mod until the StS2 Community Workshop is ready. 


## Quick Setup Guide for PiShockTheSpire2 beta

> [!Warning]
> This mod is in beta. Some errors are expected to happen while using it. Feel free to report any issues or give any feedback you want.

- **Step 0 (optional):** Set your game in the **public-beta** branch.

If you are having issues running the mod, switching to the beta branch of StS2 might help to solve some of them.

![](https://i.imgur.com/RhH2gpo.png)

- **Step 1:** Download the last version of the mod listed in the [Releases section](https://github.com/Yahlunna/PiShockTheSpire2/releases).

This mod uses BaseLib as a dependency. You can either download BaseLib from [here](https://github.com/Alchyr/BaseLib-StS2) before installing this mod, or download the PiShockTheSpire mod with its BaseLib dependency directly in the [Releases section](https://github.com/Yahlunna/PiShockTheSpire2/releases). 

If you are following this guide, this is probably your first time installing a StS2 mod manually, so I advise you to just download the "PiShockTheSpire2withBaseLibDependency.zip" asset with the solved dependency on it.
Otherwise, if you already own BaseLib, you can just download the mod implementation in the "PiShockTheSpire2withBaseLibDependency.zip" asset.

![](https://i.imgur.com/3uQ3Qhu.png)



- **Step 2:** Install the mod.

First, browse into your StS2 installation folder. If you are in steam, you can find it here:

![](https://i.imgur.com/HS6Mt47.png)

Once there, create a "mods" folder.

![](https://i.imgur.com/dJ0nFLA.png)

Extract the files from "PiShockTheSpire2.zip" into this folder. You will also need to keep BaseLib here. A successful instalation should look like this:

![](https://i.imgur.com/uQXrXvu.png)

**Step 2:** Execute the game and make sure the mod is running:
You can see your list of mods at `Settings > General > Modding`, by clicking in the Mod Settings button.

![](https://i.imgur.com/sMEKwjR.png)

This should look like this:

![](https://i.imgur.com/AOzuDsT.png)

> [!Warning]
> If you mod Slay The Spire 2, your save fille will be safely stored and you will start your modded StS2 with a different one.
> To restore your old safefile, simply disable all your mods in the menu above, and reset the game.

**Step 3:** Configure the mod

You can now open the Mod Settings menu and configure PiShock The Spire 2 to link it with a PiShock Shocker

The configuration menu looks like this:

![](https://i.imgur.com/ju8EZS3.png)

Feel free to adjust the parameters to your liking.
The main 3 parameters you need to run this mod are your **Username**, an **API Key** for your account, and the **ID of the Shocker** you want to use.

You can find your username and how to generate api keys in your PiShock Account.

![](https://i.imgur.com/WWCiTY5.png)

You can find your Shocker ID in the PiShock Vault. Please, keep in mind you will need to use your Shocker ID, **not** your Hub ID.

![](https://i.imgur.com/JpVaSw7.png)


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
