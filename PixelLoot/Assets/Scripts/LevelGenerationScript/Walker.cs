using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;
public enum Direction
{
    Up,
    Right,
    Down,
    Left
}
public class Walker : MonoBehaviour
{
    [Header("Walker Settings")]
    public LayerMask nodeLayer;
    public int numOfWalks = 10;
    public Direction walkerDirection;
    public Direction previousDirection;
    public Grid gridHolder;
    public GameObject marker;
    [Space(2)]
    [Header("Enemy Settings")]
    public int minEnemyCountPerRoom = 4;
    public int maxEnemyCountPerRoom = 12;
    public GameObject[] enemies;
    [Space(2)]
    [Header("Playable Chars Settings")]
    public GameObject[] playerCharacters;    
    [Space(2)]
    public GameObject exitRoomPrefab;
    [Space(2)]
    
    [HideInInspector]
    public int roomWidth;
    [HideInInspector]
    public int roomHeight;
    [Header("Room Variations")]
    public Tilemap roomDown;
    public Tilemap roomUpDown;
    public Tilemap roomDownRight;
    public Tilemap roomLeftDown;
    public Tilemap roomUp;
    public Tilemap roomUpRight;
    public Tilemap roomUpLeft;
    public Tilemap roomRight;
    public Tilemap roomRightLeft;
    public Tilemap roomLeft;
    public Tilemap roomLeftRightUp;
    public Tilemap roomUpLeftDown;
    public Tilemap roomDownLeftRight;
    public Tilemap roomUpRightDown;
    public Tilemap roomUpRightDownLeft;

    [HideInInspector]
    public static bool hasPlayerBeenInstantiated;
    private Tilemap goRoom;
    private List<Vector2> visitedPos;
    private List<GameObject> marks;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    void Start()
    {
        DontDestroyOnLoad(this);
        marks = new List<GameObject>();
        visitedPos = new List<Vector2>();
        var graph = AstarPath.active.data.gridGraph;
        
        
        SetRoomPositions();

        minX = ReturnMinX();
        maxX = ReturnMaxX();
        minY = ReturnMinY();
        maxY = ReturnMaxY();


        graph.center = new Vector3((((int)minX + (int)maxX) / 2), (((int)minY + (int)maxY) / 2));
        graph.center += new Vector3(-3.5f, -1, 0);

        int width = 15 + (int)Mathf.Abs(minX) + (int)Mathf.Abs(maxX);
        int depth = 10 + (int)Mathf.Abs(minY) + (int)Mathf.Abs(maxY);

       

        InstantiateRooms();

        graph.SetDimensions(2*width, 2*depth, 0.5f);
        Invoke("Scan", 0.1f);

        InstantiateEnemies();
        InstantiatePlayer();

        
    }

    void Scan()
    {
        AstarPath.active.Scan();
    }

    void SetRoomPositions()
    {
        GameObject mark = Instantiate(marker, transform.position, Quaternion.identity);
        visitedPos.Add(transform.position);
        walkerDirection = Direction.Up;
        marks.Add(mark);
        for (int i = 1; i < numOfWalks; i++)
        {

            if (walkerDirection == Direction.Down)
            {
                transform.position = new Vector2(transform.position.x, (transform.position.y - roomHeight) + 2);

            }
            else if (walkerDirection == Direction.Up)
            {
                transform.position = new Vector2(transform.position.x, (transform.position.y + roomHeight) - 2);

            }
            else if (walkerDirection == Direction.Right)
            {
                transform.position = new Vector2(transform.position.x + roomWidth - 1, transform.position.y);

            }
            else if (walkerDirection == Direction.Left)
            {
                transform.position = new Vector2(transform.position.x - roomWidth + 1, transform.position.y);
            }

            if (!visitedPos.Contains(transform.position))
            {
                visitedPos.Add(transform.position);
                mark = Instantiate(marker, transform.position, Quaternion.identity);
                marks.Add(mark);
            }
            previousDirection = walkerDirection;
            walkerDirection = (Direction)Random.Range(0, 4);

        }

    }

    void InstantiateRooms()
    {
        RaycastHit2D hitRight;
        RaycastHit2D hitLeft;
        RaycastHit2D hitUp;
        RaycastHit2D hitDown;


        for (int i = 0; i < visitedPos.Count; i++)
        {
            Vector2 roomPos = visitedPos[i];
            hitRight = Physics2D.Raycast(new Vector2(visitedPos[i].x + 1, visitedPos[i].y), Vector2.right, 16, nodeLayer);
            hitLeft = Physics2D.Raycast(new Vector2(visitedPos[i].x - 1, visitedPos[i].y), Vector2.left, 16, nodeLayer);
            hitUp = Physics2D.Raycast(new Vector2(visitedPos[i].x, visitedPos[i].y + 1), Vector2.up, 16, nodeLayer);
            hitDown = Physics2D.Raycast(new Vector2(visitedPos[i].x, visitedPos[i].y - 1), Vector2.down, 16, nodeLayer);


            if (hitRight.collider != null && hitLeft.collider != null && hitUp.collider != null && hitDown.collider != null)
            {
                goRoom = Instantiate(roomUpRightDownLeft, roomPos, Quaternion.identity);
                goRoom.transform.SetParent(gridHolder.transform);
            }
            if (hitRight.collider != null && hitLeft.collider != null && hitUp.collider != null && hitDown.collider == null)
            {

                goRoom = Instantiate(roomLeftRightUp, roomPos, Quaternion.identity);
                goRoom.transform.SetParent(gridHolder.transform);
            }
            if (hitRight.collider == null && hitLeft.collider != null && hitUp.collider != null && hitDown.collider != null)
            {

                goRoom = Instantiate(roomUpLeftDown, roomPos, Quaternion.identity);
                goRoom.transform.SetParent(gridHolder.transform);
            }
            if (hitRight.collider != null && hitLeft.collider != null && hitUp.collider == null && hitDown.collider != null)
            {

                goRoom = Instantiate(roomDownLeftRight, roomPos, Quaternion.identity);
                goRoom.transform.SetParent(gridHolder.transform);
            }
            if (hitRight.collider != null && hitLeft.collider == null && hitUp.collider != null && hitDown.collider != null)
            {

                goRoom = Instantiate(roomUpRightDown, roomPos, Quaternion.identity);
                goRoom.transform.SetParent(gridHolder.transform);
            }
            if (hitRight.collider == null && hitLeft.collider != null && hitUp.collider == null && hitDown.collider == null)
            {

                goRoom = Instantiate(roomLeft, roomPos, Quaternion.identity);
                goRoom.transform.SetParent(gridHolder.transform);
            }
            if (hitRight.collider != null && hitLeft.collider == null && hitUp.collider == null && hitDown.collider == null)
            {

                goRoom = Instantiate(roomRight, roomPos, Quaternion.identity);
                goRoom.transform.SetParent(gridHolder.transform);
            }
            if (hitRight.collider != null && hitLeft.collider != null && hitUp.collider == null && hitDown.collider == null)
            {

                goRoom = Instantiate(roomRightLeft, roomPos, Quaternion.identity);
                goRoom.transform.SetParent(gridHolder.transform);
            }
            if (hitRight.collider == null && hitLeft.collider == null && hitUp.collider != null && hitDown.collider == null)
            {

                goRoom = Instantiate(roomUp, roomPos, Quaternion.identity);
                goRoom.transform.SetParent(gridHolder.transform);
            }
            if (hitRight.collider != null && hitLeft.collider == null && hitUp.collider != null && hitDown.collider == null)
            {

                goRoom = Instantiate(roomUpRight, roomPos, Quaternion.identity);
                goRoom.transform.SetParent(gridHolder.transform);
            }
            if (hitRight.collider == null && hitLeft.collider != null && hitUp.collider != null && hitDown.collider == null)
            {

                goRoom = Instantiate(roomUpLeft, roomPos, Quaternion.identity);
                goRoom.transform.SetParent(gridHolder.transform);
            }
            if (hitRight.collider == null && hitLeft.collider == null && hitUp.collider == null && hitDown.collider != null)
            {

                goRoom = Instantiate(roomDown, roomPos, Quaternion.identity);
                goRoom.transform.SetParent(gridHolder.transform);
            }
            if (hitRight.collider == null && hitLeft.collider != null && hitUp.collider == null && hitDown.collider != null)
            {
                goRoom = Instantiate(roomLeftDown, roomPos, Quaternion.identity);
                goRoom.transform.SetParent(gridHolder.transform);
            }
            if (hitRight.collider != null && hitLeft.collider == null && hitUp.collider == null && hitDown.collider != null)
            {

                goRoom = Instantiate(roomDownRight, roomPos, Quaternion.identity);
                goRoom.transform.SetParent(gridHolder.transform);
            }
            if (hitRight.collider == null && hitLeft.collider == null && hitUp.collider != null && hitDown.collider != null)
            {

                goRoom = Instantiate(roomUpDown, roomPos, Quaternion.identity);
                goRoom.transform.SetParent(gridHolder.transform);
            }

            if(i == visitedPos.Count - 1)
            {
                int randomX = (int)Random.Range(visitedPos[i].x - 9, visitedPos[i].x + 3);
                int randomY = (int)Random.Range(visitedPos[i].y - 4, visitedPos[i].y + 2);
                Instantiate(exitRoomPrefab, new Vector3(randomX, randomY), Quaternion.identity);
            }
            

        }

        foreach (GameObject go in marks)
        {
            Destroy(go);
        }
    }

    void InstantiateEnemies()
    {
        for(int i = 1; i < visitedPos.Count; i++)
        {
            int enemySpawnRange = Random.Range(minEnemyCountPerRoom, maxEnemyCountPerRoom);
            for(int x = 0; x < enemySpawnRange; x++)
            {
                int randomX = (int)Random.Range(visitedPos[i].x - 9, visitedPos[i].x + 3);
                int randomY = (int)Random.Range(visitedPos[i].y - 4, visitedPos[i].y + 2);
                Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(randomX, randomY), Quaternion.identity);
            }
            
        }
    }

    void InstantiatePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Instantiate(playerCharacters[PlayerPrefs.GetInt("selectedChar") - 1], visitedPos[0], Quaternion.identity);           
        }              
    }

    float ReturnMaxX()
    {
        float maxX = visitedPos[0].x;

        for (int i = 0; i < visitedPos.Count; i++)
        {
            if(maxX < visitedPos[i].x)
            {
                maxX = visitedPos[i].x;
            }
        }

        return maxX;
    }
    float ReturnMinX()
    {
        float mixX = visitedPos[0].x;

        for (int i = 0; i < visitedPos.Count; i++)
        {
            if (mixX > visitedPos[i].x)
            {
                mixX = visitedPos[i].x;
            }
        }

        return mixX;
    }
    float ReturnMaxY()
    {
        float maxY = visitedPos[0].y;

        for (int i = 0; i < visitedPos.Count; i++)
        {
            if (maxY < visitedPos[i].y)
            {
                maxY = visitedPos[i].y;
            }
        }

        return maxY;
    }
    float ReturnMinY()
    {
        float minY = visitedPos[0].y;

        for (int i = 0; i < visitedPos.Count; i++)
        {
            if (minY > visitedPos[i].y)
            {
                minY = visitedPos[i].y;
            }
        }

        return minY;
    }
}
