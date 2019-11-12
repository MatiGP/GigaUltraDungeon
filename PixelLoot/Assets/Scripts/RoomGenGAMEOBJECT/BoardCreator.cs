using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    public enum TileType
    {
        Wall, Floor,
    }
    [Space(2)]
    [Header("Wymiary Planszy")]
    public int columns = 100;
    public int rows = 100;
    [Space(2)]
    [Header("Pomieszczenia i korytarze")]
    public IntRange numRooms = new IntRange(5, 10);
    public IntRange roomWidth = new IntRange(3, 10);
    public IntRange roomHeight = new IntRange(3, 10);
    public IntRange corridorLength = new IntRange(5, 7);
    [Space(2)]
    [Header("Tiles")]
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;
    [Space(2)]
    [Header("Characters, creatures, tresure to spawn")]
    public GameObject[] playableChars;
    public GameObject[] startWeapons;
    public GameObject[] enemies;

    private TileType[][] tiles;
    private Room[] rooms;
    private Corridor[] corridors;
    private GameObject boardHolder;

    private void Start()
    {
        boardHolder = new GameObject("BoardHolder");

        SetupTilesArray();

        CreateRoomsAndCorridors();

        SetTilesValuesForRooms();
        SetTilesValueForCorridors();

        InstantiateTiles();
        InstantiateOuterWalls();

    }

    void SetupTilesArray()
    {
        tiles = new TileType[columns][];
        for(int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new TileType[rows];
        }
    }

    void CreateRoomsAndCorridors()
    {
        rooms = new Room[numRooms.Random];

        corridors = new Corridor[rooms.Length - 1];

        rooms[0] = new Room();
        corridors[0] = new Corridor();

        rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);

        corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, columns, rows, true, rooms);

        Vector3 playerPos = new Vector3(rooms[0].xPos, rooms[0].yPos, 0f);
        Instantiate(playableChars[PlayerPrefs.GetInt("selectedChar")-1], playerPos, Quaternion.identity);
        Instantiate(startWeapons[PlayerPrefs.GetInt("selectedChar")-1], new Vector3(playerPos.x + 2, playerPos.y + 2), Quaternion.identity);

        for (int i = 1; i < rooms.Length; i++)
        {
            rooms[i] = new Room();

            rooms[i].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1], rooms);

            if(i < corridors.Length)
            {
                corridors[i] = new Corridor();

                corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, columns, rows, false, rooms);
            }
            int rand = Random.Range(0, enemies.Length);
            int randX = Random.Range(rooms[i].xPos, rooms[i].xPos + rooms[i].roomWidth);
            int randY = Random.Range(rooms[i].yPos, rooms[i].yPos + rooms[i].roomHeight);
            Vector3 enemyPos = new Vector3(randX, randY);
            Instantiate(enemies[rand], enemyPos, Quaternion.identity);
            
        }
    }

    void SetTilesValuesForRooms()
    {
        for(int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];

            for(int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;

                for(int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;

                    tiles[xCoord][yCoord] = TileType.Floor;
                }
            }
        }
    }

    void SetTilesValueForCorridors()
    {
        for (int i = 0; i < corridors.Length; i++)
        {
            Corridor currentCorridor = corridors[i];

            for (int j = 0; j < currentCorridor.corridorLength; j++)
            {
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                switch (currentCorridor.direction)
                {
                    case Direction.North:
                        yCoord += j;
                        break;
                    case Direction.East:
                        xCoord += j;
                        break;
                    case Direction.South:
                        yCoord -= j;
                        break;
                    case Direction.West:
                        xCoord -= j;
                        break;
                }

                tiles[xCoord][yCoord] = TileType.Floor;
            }
        }
    }

    void InstantiateTiles()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            for(int j = 0; j < tiles[i].Length; j++)
            {                
                if (tiles[i][j] == TileType.Wall)
                {
                    InstantiateFromArray(wallTiles, i, j);
                }
                else
                {
                    InstantiateFromArray(floorTiles, i, j);
                }
            }
        }
    }

    void InstantiateOuterWalls()
    {
        float leftEdgeX = -1f;
        float rightEdgeX = columns + 0f ;
        float bottomEdgeY = -1f;
        float topEdgeY = rows + 0f;

        InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
        InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
    }

    void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY)
    {
        float currentY = startingY;

        while(currentY <= endingY)
        {
            InstantiateFromArray(outerWallTiles, xCoord, currentY);
            currentY++;
        }
    }

    void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
    {
        float currentX = startingX;

        while (currentX <= endingX)
        {
            InstantiateFromArray(outerWallTiles, currentX, yCoord);
            currentX++;
        }
    }

    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        int randomIndex = Random.Range(0, prefabs.Length);

        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

        tileInstance.transform.parent = boardHolder.transform;
        
    }
}
