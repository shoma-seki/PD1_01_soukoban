using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManagerScript : MonoBehaviour
{
    int[] map;

    // Start is called before the first frame update
    void Start()
    {
        //”z—ñ‚ÌÀ‘Ì‚Ìì¬‚Æ‰Šú‰»
        map = new int[] { 0, 0, 0, 1, 0, 0, 0, 0, 0 };

        //•¶š—ñ‚ÌéŒ¾‚Æ‰Šú‰»
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            debugText += map[i].ToString() + ",";
        }

        Debug.Log(debugText);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int playerIndex = -1;
            for (int i = 0; i < map.Length; i++)
            {
                if (map[i] == 1)
                {
                    playerIndex = i;
                }
            }

            if (playerIndex < map.Length - 1)
            {
                map[playerIndex] = 0;
                map[playerIndex + 1] = 1;
            }


            string debugText = "";
            for (int i = 0; i < map.Length; i++)
            {
                debugText += map[i].ToString() + ",";
            }

            Debug.Log(debugText);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int playerIndex = -1;
            for (int i = 0; i < map.Length; i++)
            {
                if (map[i] == 1)
                {
                    playerIndex = i;
                }
            }

            if(playerIndex > 0)
            {
                map[playerIndex] = 0;
                map[playerIndex - 1] = 1;
            }


            string debugText = "";
            for (int i = 0; i < map.Length; i++)
            {
                debugText += map[i].ToString() + ",";
            }

            Debug.Log(debugText);
        }

    }
}
