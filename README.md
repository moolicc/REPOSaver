# REPOSaver

![image](https://github.com/user-attachments/assets/c08b5b6d-a8db-4c40-98b9-5ab3184047b1)

![image](https://github.com/user-attachments/assets/dc8f0243-960c-44bb-924d-7a92c25591bf)

## Save archiver

The primary function of REPOSaver is to perform automated backups and restorations of game save files.
Every time REPO performs a save, REPOSaver will backup the file. If REPO subsequently deletes a save (ie, if you lose), REPOSaver will automatically restore the game.
**You will still have to return to the main menu and reload the save after such a restoration occurs,** but at least all of your hard-earned progress will persist :).

## Save editor

The program also supports save editing. There are various menu options available, but you can also double-click a save item in the list to open the save editor.
A prompt will appear warning you that editing save files is dangerous, and while issues haven't been encountered yet, that doesn't mean you won't corrupt a save by editing it.

### Save editor usage

Briefly, here are some walkthroughs for some common edits:

#### 1. Changing a save's name
To change the name of a save, select the save you want to change from the list and click the "rename" menu button.

Note that while there's no confirmation prompt begging you to confirm, this is still a save modification and *could* cause issues (though it's not super likely I reckon).

![image](https://github.com/user-attachments/assets/c845e24a-459b-493f-b9e1-a918f66146f6)


#### 2. Changing a player's health or upgrades

To adjust a player's health, simply navigate to Players --> [Name of player] \
Then, find the "Health" field in the grid on the right and set the value to your desired health.
I wouldn't exceed a value greater than 100 plus 20 for every health upgrade, as I have no clue what will happen if you set a player's health higher than their maximum.

To set a player's upgrades, similarly navigate to the desired player, then set the level for any of their Upgrades.

![image](https://github.com/user-attachments/assets/a36ae737-86da-4e48-bdef-de61ad0c9f37)


#### 3. Setting level/world/"global" settings

To set so-called "global" settings (The current level, total money, etc), navigate to the "Global" item.
Then in the grid on the right, you may change the currency, highest completed level, or the charge level of the charging station.

![image](https://github.com/user-attachments/assets/1084dbcf-1936-4cef-9f5c-4773c35519ed)

#### 4. Set purchased items

To set the items that are currently owned (such as guns, baseball bats, etc), navigate to Global --> Items.

Under the "Purchased" category, you may set any of the items' values to 1 to indicate you'd like one of those items. Note that I'm not yet sure what happens if you use a number larger than 1.

![image](https://github.com/user-attachments/assets/37af0d84-2fed-4ec7-b06e-94a5fb8c761d)

