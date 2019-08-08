# Maze runner
## Targeted framework
.NET Framework 4.6.1 

## Type
Console Application

## Overview
This is a command-line game where a player has to move around in a maze, made out of 49 rooms. In some rooms, there are monsters that user must fight. There are items in some rooms as well. Game ends when either player dies or he found the final room where the princess is hiding. For every traversal in another room health is leveling down by 5. Starting health is 100. For every fight with a monster health is leveling down as well. 

### Items
Weapons - like Katana, Pistol, Shotgun, etc. They help you with the monsters, so you will lose much less health in a fight that way.

Food - Banana, Cake , etc. These items help you restore your health by eating them.

Special ( keys ) - With those items you can unlock doors.

### Backpack
You have a backpack where you can store the items you found. The capacity of the is limited , so plan well what you can store. Each item has weight and the more weight the item has , the less space you will have in the bag. You can drop items from the backpack to free some space.

## Commands
```
help - show list of commands
info - show information and rules
location - show current location
goto  - move to another room
items - show what items you have in your backpack
eat - eat something
unlock - use your key to unlock a room
pick - pick up item and put it in the backpack
drop - remove the item from the backpack and put it in the room
quit - exit game
```
