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
    public int currentFloorLevel;  
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
    public Tilemap[] roomDown;
    public Tilemap[] roomUpDown;
    public Tilemap[] roomDownRight;
    public Tilemap[] roomLeftDown;
    public Tilemap[] roomUp;
    public Tilemap[] roomUpRight;
    public Tilemap[] roomUpLeft;
    public Tilemap[] roomRight;
    public Tilemap[] roomRightLeft;
    public Tilemap[] roomLeft;
    public Tilemap[] roomLeftRightUp;
    public Tilemap[] roomUpLeftDown;
    public Tilemap[] roomDownLeftRight;
    public Tilemap[] roomUpRightDown;
    public Tilemap[] roomUpRightDownLeft;
    public Tilemap playerStarterRoomDown;
    public Tilemap playerStarterRoomUpDown;
    public Tilemap playerStarterRoomDownRight;
    public Tilemap playerStarterRoomLeftDown;
    public Tilemap playerStarterRoomUp;
    public Tilemap playerStarterRoomUpRight;
    public Tilemap playerStarterRoomUpLeft;
    public Tilemap playerStarterRoomRight;
    public Tilemap playerStarterRoomRightLeft;
    public Tilemap playerStarterRoomLeft;
    public Tilemap playerStarterRoomLeftRightUp;
    public Tilemap playerStarterRoomUpLeftDown;
    public Tilemap playerStarterRoomDownLeftRight;
    public Tilemap playerStarterRoomRightDown;
    public Tilemap playerStarterRoomUpRightDownLeft;


    private int floorNum;
    private Tilemap goRoom;
    private Tilemap firstRoom;
    private List<Vector2> visitedPos;
    private List<GameObject> marks;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    void Start()
    {       
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

        SelectRoom(out hitRight, out hitLeft, out hitUp, out hitDown, 0, true);

        for (int i = 1; i < visitedPos.Count; i++)
        {
            SelectRoom(out hitRight, out hitLeft, out hitUp, out hitDown, i, false);

            if (i == visitedPos.Count - 1)
            {                
                Instantiate(exitRoomPrefab, goRoom.GetComponent<SpawnEnemies>().doorSpawnPoint.position, Quaternion.identity);
                goRoom.GetComponent<SpawnEnemies>().spawnBoss = true;

            }
            goRoom.GetComponent<SpawnEnemies>().Spawn();

        }

        foreach (GameObject go in marks)
        {
            Destroy(go);
        }
    }

    private void SelectRoom(out RaycastHit2D hitRight, out RaycastHit2D hitLeft, out RaycastHit2D hitUp, out RaycastHit2D hitDown, int i, bool spawnPlayer)
    {
        Vector2 roomPos = visitedPos[i];
        hitRight = Physics2D.Raycast(new Vector2(visitedPos[i].x + 1, visitedPos[i].y), Vector2.right, 16, nodeLayer);
        hitLeft = Physics2D.Raycast(new Vector2(visitedPos[i].x - 1, visitedPos[i].y), Vector2.left, 16, nodeLayer);
        hitUp = Physics2D.Raycast(new Vector2(visitedPos[i].x, visitedPos[i].y + 1), Vector2.up, 16, nodeLayer);
        hitDown = Physics2D.Raycast(new Vector2(visitedPos[i].x, visitedPos[i].y - 1), Vector2.down, 16, nodeLayer);


        if (hitRight.collider != null && hitLeft.collider != null && hitUp.collider != null && hitDown.collider != null)
        {
            goRoom = Instantiate(roomUpRightDownLeft[Random.Range(0, roomUpRightDownLeft.Length)], roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider != null && hitLeft.collider != null && hitUp.collider != null && hitDown.collider == null)
        {

            goRoom = Instantiate(roomLeftRightUp[Random.Range(0, roomLeftRightUp.Length)], roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider == null && hitLeft.collider != null && hitUp.collider != null && hitDown.collider != null)
        {

            goRoom = Instantiate(roomUpLeftDown[Random.Range(0, roomUpLeftDown.Length)], roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider != null && hitLeft.collider != null && hitUp.collider == null && hitDown.collider != null)
        {

            goRoom = Instantiate(roomDownLeftRight[Random.Range(0, roomDownLeftRight.Length)], roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider != null && hitLeft.collider == null && hitUp.collider != null && hitDown.collider != null)
        {

            goRoom = Instantiate(roomUpRightDown[Random.Range(0, roomUpRightDown.Length)], roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider == null && hitLeft.collider != null && hitUp.collider == null && hitDown.collider == null)
        {

            goRoom = Instantiate(roomLeft[Random.Range(0, roomLeft.Length)], roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider != null && hitLeft.collider == null && hitUp.collider == null && hitDown.collider == null)
        {

            goRoom = Instantiate(roomRight[Random.Range(0, roomRight.Length)], roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider != null && hitLeft.collider != null && hitUp.collider == null && hitDown.collider == null)
        {

            goRoom = Instantiate(roomRightLeft[Random.Range(0, roomRightLeft.Length)], roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider == null && hitLeft.collider == null && hitUp.collider != null && hitDown.collider == null)
        {

            goRoom = Instantiate(roomUp[Random.Range(0, roomUp.Length)], roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider != null && hitLeft.collider == null && hitUp.collider != null && hitDown.collider == null)
        {

            goRoom = Instantiate(roomUpRight[Random.Range(0, roomUpRight.Length)], roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider == null && hitLeft.collider != null && hitUp.collider != null && hitDown.collider == null)
        {

            goRoom = Instantiate(roomUpLeft[Random.Range(0, roomUpLeft.Length)], roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider == null && hitLeft.collider == null && hitUp.collider == null && hitDown.collider != null)
        {

            goRoom = Instantiate(roomDown[Random.Range(0, roomDown.Length)], roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider == null && hitLeft.collider != null && hitUp.collider == null && hitDown.collider != null)
        {
            goRoom = Instantiate(roomLeftDown[Random.Range(0, roomLeftDown.Length)], roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider != null && hitLeft.collider == null && hitUp.collider == null && hitDown.collider != null)
        {

            goRoom = Instantiate(roomDownRight[Random.Range(0, roomDownRight.Length)], roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider == null && hitLeft.collider == null && hitUp.collider != null && hitDown.collider != null)
        {

            goRoom = Instantiate(roomUpDown[Random.Range(0, roomUpDown.Length)], roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (spawnPlayer)
        {
            InstantiatePlayer(goRoom.GetComponent<SpawnEnemies>().playerSpawnPoint.position);
        }
    }

    private void SelectStarterRoom(out RaycastHit2D hitRight, out RaycastHit2D hitLeft, out RaycastHit2D hitUp, out RaycastHit2D hitDown, int i)
    {
        Vector2 roomPos = visitedPos[0];
        hitRight = Physics2D.Raycast(new Vector2(visitedPos[0].x + 1, visitedPos[0].y), Vector2.right, 16, nodeLayer);
        hitLeft = Physics2D.Raycast(new Vector2(visitedPos[0].x - 1, visitedPos[0].y), Vector2.left, 16, nodeLayer);
        hitUp = Physics2D.Raycast(new Vector2(visitedPos[0].x, visitedPos[0].y + 1), Vector2.up, 16, nodeLayer);
        hitDown = Physics2D.Raycast(new Vector2(visitedPos[0].x, visitedPos[0].y - 1), Vector2.down, 16, nodeLayer);


        if (hitRight.collider != null && hitLeft.collider != null && hitUp.collider != null && hitDown.collider != null)
        {
            goRoom = Instantiate(playerStarterRoomUpRightDownLeft, roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider != null && hitLeft.collider != null && hitUp.collider != null && hitDown.collider == null)
        {

            goRoom = Instantiate(playerStarterRoomLeftRightUp, roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider == null && hitLeft.collider != null && hitUp.collider != null && hitDown.collider != null)
        {

            goRoom = Instantiate(playerStarterRoomUpLeftDown, roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider != null && hitLeft.collider != null && hitUp.collider == null && hitDown.collider != null)
        {

            goRoom = Instantiate(playerStarterRoomDownLeftRight, roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider != null && hitLeft.collider == null && hitUp.collider != null && hitDown.collider != null)
        {

            goRoom = Instantiate(roomUpRightDown[Random.Range(0, roomUpRightDown.Length)], roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider == null && hitLeft.collider != null && hitUp.collider == null && hitDown.collider == null)
        {

            goRoom = Instantiate(playerStarterRoomLeft, roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider != null && hitLeft.collider == null && hitUp.collider == null && hitDown.collider == null)
        {

            goRoom = Instantiate(playerStarterRoomRight, roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider != null && hitLeft.collider != null && hitUp.collider == null && hitDown.collider == null)
        {

            goRoom = Instantiate(playerStarterRoomRightLeft, roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider == null && hitLeft.collider == null && hitUp.collider != null && hitDown.collider == null)
        {

            goRoom = Instantiate(playerStarterRoomUp, roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider != null && hitLeft.collider == null && hitUp.collider != null && hitDown.collider == null)
        {

            goRoom = Instantiate(playerStarterRoomUpRight, roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider == null && hitLeft.collider != null && hitUp.collider != null && hitDown.collider == null)
        {

            goRoom = Instantiate(playerStarterRoomUpLeft, roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider == null && hitLeft.collider == null && hitUp.collider == null && hitDown.collider != null)
        {

            goRoom = Instantiate(playerStarterRoomDown, roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider == null && hitLeft.collider != null && hitUp.collider == null && hitDown.collider != null)
        {
            goRoom = Instantiate(playerStarterRoomLeftDown, roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider != null && hitLeft.collider == null && hitUp.collider == null && hitDown.collider != null)
        {

            goRoom = Instantiate(playerStarterRoomDownRight, roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
        if (hitRight.collider == null && hitLeft.collider == null && hitUp.collider != null && hitDown.collider != null)
        {

            goRoom = Instantiate(playerStarterRoomUpDown, roomPos, Quaternion.identity);
            goRoom.transform.SetParent(gridHolder.transform);
        }
    }

    void InstantiatePlayer(Vector3 position)
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            Instantiate(playerCharacters[PlayerPrefs.GetInt("selectedChar")-1], position, Quaternion.identity);            

        }
        if(PlayerStats.instance.playerSAI.currentLevel >= 2)
        {
            PlayerStats.instance.LoadState();
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
