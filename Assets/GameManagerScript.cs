using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject playerPrefab;

    int[,] map;

    GameObject[,] field;

    // Start is called before the first frame update

    //void PrintArray()
    //{
    //    string debugText = "";
    //    for (int i = 0; i < map.Length; i++)
    //    {
    //        debugText += map[i].ToString() + ",";
    //    }
    //    Debug.Log(debugText);
    //}

    private Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for(int x=0; x<field.GetLength(1); x++)
            {
                if (field[y,x].tag == "Player")
                {
                    return new Vector2Int(x,y);
                }
            }
        }
        return new Vector2Int(-1,-1);
    }

    void Start()
    {
        //マップ設定
        map = new int[,]
        {
            {1,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
        };
        //フィールドサイズ決定
        field = new GameObject[
            map.GetLength(0),
            map.GetLength(1)
            ];


        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y,x]==1)
                {
                    field[y,x] = Instantiate(
                        playerPrefab,
                        new Vector3(x,map.GetLength(0)-1-y,0),
                        Quaternion.identity
                    );
                }
            }
        }

        string debugTXT = "";

        for (int y=0;y<map.GetLength(0);y++)
        {
            for(int x=0;x<map.GetLength(1);x++)
            {
                debugTXT += map[y, x].ToString() + ",";
            }
            debugTXT += "\n";
        }
        Debug.Log(debugTXT);
    }

    //bool MoveNumber(string number, Vector2Int moveFrom, Vector2Int moveTo)
    //{
    //    if (moveTo.y < 0 || moveTo.y >= field.GetLength(0))
    //    {
    //        return false;
    //    }
    //    if(moveTo.x<0||moveTo.x >= field.GetLength(1))
    //    {
    //        return false;
    //    }


        //移動先に箱があったら
        //if (field[moveTo] == 2)
        //{
        //    int velocity = moveTo - moveFrom;
        //    bool success = MoveNumber(2, moveTo, moveTo + velocity);
        //    if (success == false)
        //    {
        //        return false;
        //    }
        //}

        ////移動
        //field[moveTo] = number;
        //field[moveFrom] = 0;
        //return true;
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //    if (Input.GetKeyDown(KeyCode.RightArrow))
    //    {
    //        int playerIndex = GetPlayerIndex();

    //        MoveNumber(1, playerIndex, playerIndex + 1);
    //        PrintArray();

    //        string debugText = "";
    //        for (int i = 0; i < map.Length; i++)
    //        {
    //            debugText += map[i].ToString() + ",";
    //        }
    //        Debug.Log(debugText);
    //    }


    //    if (Input.GetKeyDown(KeyCode.LeftArrow))
    //    {
    //        int playerIndex = GetPlayerIndex();

    //        MoveNumber(1, playerIndex, playerIndex - 1);
    //        PrintArray();

    //        string debugText = "";
    //        for (int i = 0; i < map.Length; i++)
    //        {
    //            debugText += map[i].ToString() + ",";
    //        }
    //        Debug.Log(debugText);
    //    }

    //}
}
