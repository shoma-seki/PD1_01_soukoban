using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManagerScript : MonoBehaviour
{
    //ゲームオブジェクト生成
    public GameObject playerPrefab;



    int[,] map;

    //void PrintArray()
    //{
    //    //文字列の宣言と初期化
    //    string debugText = "";
    //    for (int i = 0; i < map.Length; i++)
    //    {
    //        debugText += map[i].ToString() + ",";
    //    }

    //    Debug.Log(debugText);
    //}

    //int GetPlayerIndex()
    //{
    //    for (int i = 0; i < map.Length; i++)
    //    {
    //        if (map[i] == 1)
    //        {
    //            return i;
    //        }
    //    }
    //    return -1;
    //}

    //bool MoveNumber(int number, int moveFrom, int moveTo)
    //{
    //    if (moveTo < 0 || moveTo >= map.Length)
    //    {
    //        return false;
    //    }

    //    if (map[moveTo] == 2)
    //    {
    //        int velocity = moveTo - moveFrom;
    //        bool success = MoveNumber(2, moveTo, moveTo + velocity);
    //        if (!success) { return false; }
    //    }

    //    map[moveTo] = number;
    //    map[moveFrom] = 0;
    //    return true;
    //}

    // Start is called before the first frame update
    void Start()
    {
        //配列の実体の作成と初期化
        map = new int[,] {
            {0,0,0,0,0 },
            {0,0,0,0,0},
            {0,0,1,0,0},
        };

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    GameObject instance=Instantiate(playerPrefab,new Vector3(x, y, 0),Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    int playerIndex = GetPlayerIndex();
        //    MoveNumber(1, playerIndex, playerIndex + 1);
        //    PrintArray();
        //}

        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    int playerIndex = GetPlayerIndex();
        //    MoveNumber(1, playerIndex, playerIndex - 1);
        //    PrintArray();
        //}
    }
}
