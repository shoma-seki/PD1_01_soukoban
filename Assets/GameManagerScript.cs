using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class gameManagerScript : MonoBehaviour
{
    //ゲームオブジェクト生成
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject goalPrefab;
    public GameObject ClearText;
    public GameObject particlePrefab;
    public GameObject wallPrefab;

    int[,] map;
    GameObject[,] field;

    Stack<GameObject[,]> preField = new Stack<GameObject[,]>();

    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] == null)
                {
                    continue;
                }
                if (field[y, x].tag == "Player")
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    bool MoveNumber(string tag, Vector2Int moveFrom, Vector2Int moveTo)
    {
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1))
        {
            return false;
        }
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0))
        {
            return false;
        }

        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Wall")
        {
            return false;
        }
        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber("Box", moveTo, moveTo + velocity);
            if (!success) { return false; }
        }

        //field[moveFrom.y, moveFrom.x].transform.position = new Vector3(-field.GetLength(1) / 2 + moveTo.x, field.GetLength(0) / 2 - moveTo.y, 0);
        Vector3 moveToPosition = new Vector3(
            -field.GetLength(1) / 2 + moveTo.x, field.GetLength(0) / 2 - moveTo.y, 0);
        field[moveFrom.y, moveFrom.x].GetComponent<Move>().MoveTo(moveToPosition);
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1280, 720, false);

        //配列の実体の作成と初期化
        map = new int[,] {
            {3,3,3,3,3,3,3,3,3,3,3,3,3 },
            {3,0,0,0,0,0,0,0,0,0,0,4,3 },
            {3,0,4,0,2,0,0,0,2,0,0,0,3 },
            {3,0,3,3,3,0,0,0,0,0,3,0,3 },
            {3,0,0,3,4,0,1,0,0,0,3,0,3 },
            {3,0,0,3,0,0,0,0,0,0,0,0,3 },
            {3,0,3,3,3,2,0,0,2,3,3,0,3 },
            {3,0,0,0,0,0,0,0,0,3,3,4,3 },
            {3,3,3,3,3,3,3,3,3,3,3,3,3 },
        };
        field = new GameObject[
            map.GetLength(0),
            map.GetLength(1)
        ];

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    field[y, x] = Instantiate(playerPrefab, new Vector3(-map.GetLength(1) / 2 + x, map.GetLength(0) / 2 - y, 0), Quaternion.identity);
                }
                if (map[y, x] == 2)
                {
                    field[y, x] = Instantiate(boxPrefab, new Vector3(-map.GetLength(1) / 2 + x, map.GetLength(0) / 2 - y, 0), Quaternion.identity);
                }
                if (map[y, x] == 3)
                {
                    field[y, x] = Instantiate(wallPrefab, new Vector3(-map.GetLength(1) / 2 + x, map.GetLength(0) / 2 - y, 0), Quaternion.identity);
                }
                if (map[y, x] == 4)
                {
                    field[y, x] = Instantiate(goalPrefab, new Vector3(-map.GetLength(1) / 2 + x, map.GetLength(0) / 2 - y, 0.01f), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(1, 0));
            if (isCleared())
            {
                ClearText.SetActive(true);
            }
            for (int i = 0; i < 5; i++)
            {
                Instantiate(particlePrefab, new Vector3(-map.GetLength(1) / 2 + playerIndex.x, map.GetLength(0) / 2 - playerIndex.y, 0), Quaternion.identity);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(-1, 0));
            if (isCleared())
            {
                ClearText.SetActive(true);
            }
            for (int i = 0; i < 5; i++)
            {
                Instantiate(particlePrefab, new Vector3(-map.GetLength(1) / 2 + playerIndex.x, map.GetLength(0) / 2 - playerIndex.y, 0), Quaternion.identity);
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(0, -1));
            if (isCleared())
            {
                ClearText.SetActive(true);
            }
            for (int i = 0; i < 5; i++)
            {
                Instantiate(particlePrefab, new Vector3(-map.GetLength(1) / 2 + playerIndex.x, map.GetLength(0) / 2 - playerIndex.y, 0), Quaternion.identity);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(0, 1));
            if (isCleared())
            {
                ClearText.SetActive(true);
            }
            for (int i = 0; i < 5; i++)
            {
                Instantiate(particlePrefab, new Vector3(-map.GetLength(1) / 2 + playerIndex.x, map.GetLength(0) / 2 - playerIndex.y, 0), Quaternion.identity);
            }

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject[,] cPreField = Clone(field);
            preField.Push(cPreField);
            for (int y = 0; y < cPreField.GetLength(0); y++)
            {
                for (int x = 0; x < cPreField.GetLength(1); x++)
                {
                    Destroy(cPreField[y, x]);
                }
            }
            if (preField.Count != 0) { Undo(preField.Pop()); }
        }
    }

    bool isCleared()
    {
        List<Vector2Int> goals = new List<Vector2Int>();

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 4)
                {
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }

        for (int i = 0; i < goals.Count; i++)
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if (f == null || f.tag != "Box")
            {
                return false;
            }
        }

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                Destroy(field[y, x]);
                if (map[y, x] == 1)
                {
                    field[y, x] = Instantiate(playerPrefab, new Vector3(-map.GetLength(1) / 2 + x, map.GetLength(0) / 2 - y, 0), Quaternion.identity);
                }
                if (map[y, x] == 2)
                {
                    field[y, x] = Instantiate(boxPrefab, new Vector3(-map.GetLength(1) / 2 + x, map.GetLength(0) / 2 - y, 0), Quaternion.identity);
                }
                if (map[y, x] == 3)
                {
                    field[y, x] = Instantiate(wallPrefab, new Vector3(-map.GetLength(1) / 2 + x, map.GetLength(0) / 2 - y, 0), Quaternion.identity);
                }
                if (map[y, x] == 4)
                {
                    field[y, x] = Instantiate(goalPrefab, new Vector3(-map.GetLength(1) / 2 + x, map.GetLength(0) / 2 - y, 0.01f), Quaternion.identity);
                }
            }
        }

        return true;
    }

    void Undo(GameObject[,] PreField)
    {
        int rows = PreField.GetLength(0);
        int cols = PreField.GetLength(1);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                if (field[y, x])
                {
                    Destroy(field[y, x]);

                    field[y, x] = Instantiate(PreField[y, x]);
                    field[y, x].transform.position = PreField[y, x].transform.position;
                    field[y, x].transform.rotation = PreField[y, x].transform.rotation;
                }
            }
        }
    }

    GameObject[,] Clone(GameObject[,] field)
    {
        int rows = field.GetLength(0);
        int cols = field.GetLength(1);

        GameObject[,] clone = new GameObject[rows, cols];
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                if (field[y, x])
                {
                    clone[y, x] = Instantiate(field[y, x]);
                    clone[y, x].transform.position = field[y, x].transform.position;
                    clone[y, x].transform.rotation = field[y, x].transform.rotation;
                }
            }
        }
        return clone;
    }
}
