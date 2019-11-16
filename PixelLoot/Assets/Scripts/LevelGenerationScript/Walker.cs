using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public enum Direction
{
    Up,
    Right,
    Down,
    Left
}
public class Walker : MonoBehaviour
{

    public LayerMask nodeLayer;
    public int numOfWalks = 10;
    public Direction walkerDirection;
    public Direction previousDirection;
    public Grid gridHolder;
    public GameObject marker;
    public GameObject[] enemies;
    public GameObject[] playerCharacters;
    public GameObject[] starterWeapons;

    public int roomWidth;
    public int roomHeight;

    public Tilemap[] roomDown;
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

    Tilemap goRoom;
    List<Vector2> visitedPos;
    List<GameObject> marks;
    // Start is called before the first frame update
    void Start()
    {
        marks = new List<GameObject>();
        visitedPos = new List<Vector2>();
        SetRoomPositions();
        InstantiateRooms();
        InstantiateEnemies();
        InstantiatePlayer();
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

                goRoom = Instantiate(roomDown[Random.Range(0, roomDown.Length)], roomPos, Quaternion.identity);
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
            int randomX = (int)Random.Range(visitedPos[i].x - 9, visitedPos[i].x + 3);
            int randomY = (int)Random.Range(visitedPos[i].y - 4, visitedPos[i].y + 2);
            Instantiate(enemies[Random.Range(0, 0)], new Vector3(randomX, randomY), Quaternion.identity);
        }
    }

    void InstantiatePlayer()
    {
        Instantiate(playerCharacters[PlayerPrefs.GetInt("selectedChar") - 1], visitedPos[0], Quaternion.identity);
        Instantiate(starterWeapons[PlayerPrefs.GetInt("selectedChar") - 1], visitedPos[0] + new Vector2(1,1), Quaternion.identity);

    }
}
