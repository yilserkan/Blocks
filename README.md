# Blocks

## About the game
This game is a mobile puzzle game where the player needs to put pieces into a grid in the correct order. The levels within this game are procedurally generated according to the choosen difficulty. See ['Procedural Generation.pdf'](https://github.com/yilserkan/Blocks/blob/main/Procedural%20Generation.pdf) for a more detailed explanation of the procedural generation algortihm. 

The game concludes of 2 modes. 

**Play Mode** : After a level is selected the player can play the game. 

**Generate Level Mode** : Within this mode, new levels can be procedurally generated according to the chosen difficulty. These levels can be saved, so that they will be added to the level pool.

## Used Patterns

### Object Pooling
I used object pooling whilst creating the pieces and the anchor points to save performance.

### Humble Object
This was mainly used to be able to write unit tests on some critical parts of the code.

## Game Screenshots
### In-Game
![image](https://user-images.githubusercontent.com/80252098/180988452-07a13e03-77eb-4eca-a9af-2d28822a78da.png)
![image](https://user-images.githubusercontent.com/80252098/180988564-c93b6c74-8086-48d5-861f-0e21f96ac839.png)

### Level Generator
![image](https://user-images.githubusercontent.com/80252098/180988756-9824aea9-e6d7-4299-8b86-431177ccb478.png)


