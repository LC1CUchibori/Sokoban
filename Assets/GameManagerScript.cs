using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class GameManagerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject GoalPrefab;
    public GameObject ParticlePrefab;
    public GameObject clearText;

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

    

    void Start()
    {
        Screen.SetResolution(1280, 720, false);

        //マップ設定
        map = new int[,]
        {
            {0,0,0,0,0,0,0},
            {0,0,3,1,3,0,0},
            {0,0,0,2,0,0,0},
            {0,0,2,3,2,0,0},
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0}
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
                        new Vector3(x,map.GetLength(0)-y,0),
                        Quaternion.identity
                    );
                }
                if (map[y, x] == 2)
                {
                    field[y, x] = Instantiate(
                        boxPrefab, 
                        new Vector3(x, map.GetLength(0) - y, 0), Quaternion.identity);
                }
                if (map[y, x] == 3)
                {
                    field[y, x] = Instantiate(
                        GoalPrefab,
                       new Vector3(x, map.GetLength(0) - y, 0), Quaternion.identity);
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

    private Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] == null) { continue; }
                if (field[y, x].tag == "Player")
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    void SpawnParticle(Vector3 position)
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                field[y, x] = Instantiate
                    (ParticlePrefab,
                    position, Quaternion.identity);
            }
        }
    }

    bool IsCleard()
    {
        List<Vector2Int> goals = new List<Vector2Int>();

        for(int y = 0; y < map.GetLength(0); y++)
        {
            for(int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 3)
                {
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }

        for(int i = 0; i < goals.Count; i++)
        {
            GameObject f = field[goals[i].y,goals[i].x];
            if (f == null || f.tag != "Box")
            {
                return false;
            }
        }
        return true;
    }

    bool MoveNumber(Vector2Int moveFrom, Vector2Int moveTo)
    {
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }

        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(moveTo, moveTo + velocity);
            if (!success) { return false; }
        }

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

        //移動
        Vector3 moveToPosition = new Vector3(
            moveTo.x, map.GetLength(0) - moveTo.y, 0
            );
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x].GetComponent<Move>().MoveTo(moveToPosition);
        field[moveFrom.y, moveFrom.x] = null;

        // プレイヤーの移動後にパーティクルを生成
        if (field[moveTo.y, moveTo.x].tag == "Player")
        {
            SpawnParticle(moveToPosition);
        }

        return true;
    }

    //// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0,-1));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0,1));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(1,0));
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex +new Vector2Int(-1,0));
        }

        //もしクリアしたら
        if (IsCleard())
        {
            clearText.SetActive(true);
        }
    }
}
