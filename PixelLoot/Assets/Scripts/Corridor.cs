using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    North, East, South, West
}

public class Corridor 
{
    public int startXPos;
    public int startYPos;
    public int corridorLength;
    public Direction direction;
    private bool overlap;

    public int EndPositionX
    {
        get
        {
            if(direction == Direction.North || direction == Direction.South)
            {
                return startXPos;
            }
            if(direction == Direction.East)
            {
                return startXPos + corridorLength - 1;
            }
            return startXPos - corridorLength + 1;
        }
    }

    public int EndPositionY
    {
        get
        {
            if(direction == Direction.East || direction == Direction.West)
            {
                return startYPos;
            }
            if(direction == Direction.North)
            {
                return startYPos + corridorLength - 1;
            }
            return startYPos - corridorLength + 1;
        }
    }

    public void SetupCorridor(Room room, IntRange length, IntRange roomWidth, IntRange roomHeight, int columns, int rows, bool firstCorridor, Room[] prevRooms)
    {
        direction = (Direction)Random.Range(0, 4);

        Direction oppositeDirection = (Direction)(((int)room.enteringCorridor + 2) % 4);

        if(!firstCorridor && direction == oppositeDirection)
        {
            int directionInt = (int)direction;
            directionInt++;
            directionInt = directionInt % 4;
            direction = (Direction)(directionInt);

        }

        corridorLength = length.Random;

        int maxLength = length.m_Max;

     
            switch (direction)
            {
                case Direction.North:
                    startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth - 1);
                    startYPos = room.yPos + room.roomHeight;
                    maxLength = rows - startYPos - roomHeight.m_Min;
                   
                    break;
                case Direction.East:
                    startXPos = room.xPos + room.roomWidth;
                    startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight - 1);
                    maxLength = columns - startXPos - roomWidth.m_Min;
                    
                    break;
                case Direction.South:
                    startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth);
                    startYPos = room.yPos;
                    maxLength = startYPos - roomHeight.m_Min;
                   
                    break;
                case Direction.West:
                    startXPos = room.xPos;
                    startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight);
                    maxLength = startXPos - roomWidth.m_Min;
                    
                    break;
            }

            corridorLength = Mathf.Clamp(corridorLength, 1, maxLength);         

    }
}
