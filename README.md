# Explorer-of-the-Dungeon---RPG
Using the Unity framework and C# I created and designed a real top down 2D RPG. This game allows players to navigate through different levels, interact with other characters, complete quests, and engage in battles, collecting points.

To create the two scenes, Main and Dungeon1, I chose a preset asset from the following link: https://0x72.itch.io/16x16-dungeon-tileset. This asset, being an image that contains multiple characters and various details for the room designs in the game, I imported it into my Unity project and, from the Inspector part, I set it as Multiple Sprite, which means that it contains a variety of sprites that I was able to cut out and introduce separately into the game.

After this, I began writing the scripts for the character's movement, for moving the camera, and, with the help of a Tile Palette, I created the two levels in the game.

1) EDITING THE DUNGEON1 LEVEL (room number 2)
Editing the Dungeon1 level - for each level individually, I created 3/4 types of tilemaps:
One for the floor, one for the floor design, one for the wall pattern, and another for collision, so that they overlap in the listed order (the Collision Active Tilemap is the last in line, which makes it impossible to cross over the objects/areas where it is activated).

2) THE WEAPON AND ATTACK ANIMATION
For the weapon (sword), which the character uses to fight enemies, I created an Animator Controller and 2 animations: one for striking (Weapon_swing) and one for the idle state (when the character has it beside them but is not in the process of striking).
When the player presses Space, the weapon trigger activates (in the script, this is a bool type, so it changes from false to true, and the Animator Controller makes the transition from the Weapon_idle state to Weapon_swing (during which the sword attack animation occurs)).

3) CHESTS AND CRATES
Both represent objects with which the character can interact during the game. Chests, by simply passing over them, give the character a sum of money, with which the player can buy other weapons from the menu. Crates give the player XP, which determines the increase of their level. To interact with them, the player must break them by hitting with the weapon.

4) THE MENU AND BUTTONS
I then created a menu, which opens by pressing the button in the bottom left corner of the game. For the button, I chose a sprite, and for the menu interface, I created a canvas, which contains a panel, and, with the help of some scripts, I made the connection between the game action and the menu. The menu contains 3 sections: one for the weapon upgrade, one for selecting the character in the game, and the third section displays information related to how much life the character has left (the area titled Health), how much money the character has gathered throughout the game (Money) and the character's level (Level). Additionally, the menu includes an XP Bar, which shows how much XP the character needs to advance to the next level. For the menus, I created an Animator Controller and two animations: Menu_showing and Menu_hiding (the first for displaying the menu and the second for hiding/exiting it from the screen).

5) User Manual
Instructions for controlling the character:
- W key/up arrow – move up
- S key/down arrow – move down
- A key/left arrow – move left
- D key/right arrow – move right
- Space key – to attack/hit enemies/objects with the weapon

Instructions for using the menu:
- To open the menu, right-click on the grey button at the bottom-left corner of the screen
- To close the menu, right-click with the mouse on the screen, outside of the menu

To start the game, right-click on the PLAY button.
To close the game, right-click on the QUIT button.
- To interact with chests or piles of money, pass over them with the character.
- To interact with crates (wooden boxes), hit them with the weapon.
- To move from one room to another, enter the portal.
