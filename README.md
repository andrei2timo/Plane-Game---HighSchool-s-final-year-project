# Plane-Game---HighSchool-s-final-year-project
A plane game using graphics in C#

![image](https://user-images.githubusercontent.com/57757171/196285335-68b53a06-7937-4fd9-82b7-581892aa6af9.png)


Airplanes are played on checkered paper where you draw two identical borders, usually 10x10 squares. Each box is identified by a number on the X axis (from 1 to 10) and a letter on the Y axis (from A to J). The first border is your own, where three identical planes are placed without the opponent seeing their position. Planes are not allowed to overlap or go out of bounds. Each player will try to guess where the opponent's planes are in the second frame.


When both players have drawn the three planes in their own border, the game can begin. You can draw lots to see who is first in line. Each move has two parts:

    - The first player announces a certain square of the opponent, for example C3.
    - The opponent looks at his border and responds with one of the following possibilities:
        a.  air - the box is outside any plane;
        b.  hit - the box is part of a plane, but the cabin was not found;
        c.  dead/downed - the box contains the cockpit of an airplane. All boxes of the downed plane become hit.
Both players note the result on their boxes, usually an X for hit and 0 for air.

Unlike Battleship (the game), where the goal is to find all the occupied cabins, the goal of the Airplane game is to find only the three cabins. The first player to find all three airplane cabins wins.
