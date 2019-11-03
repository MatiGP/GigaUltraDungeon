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
}
