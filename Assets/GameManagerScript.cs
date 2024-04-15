using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class GameManagerScript : MonoBehaviour
{
    //配列の宣言
    int[] map;

    void PrintArray()
    {
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            debugText += map[i].ToString() + ";";
        }
        Debug.Log(debugText);

    }

    // Start is called before the first frame update
    void Start()
    {
        //デバッグログの出力
       map = new int[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 };
       

        PrintArray();
    }

    int GetPlayerIndex()
    {
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }

    bool MoveNumber(int number,int moveFrom,int moveTo)
    {
        //移動先が範囲外なら移動不可
        if(moveTo<0||moveTo>=map.Length)
        {
            return false;
        }
        //移動先に２がいたら
        if (map[moveTo]==2)
        {
            //どの方向に移動するか算出
            int velocity=moveTo-moveFrom;
            //プレイヤーの移動先からさらに先に２を移動する
            //箱の移動処理。MoveNumberメソッド内でMoveNumberメソッドを
            //呼び、処理を再帰している。移動可不可boolで記録
            bool success = MoveNumber(2, moveTo, moveTo + velocity);
            //もし箱が移動失敗したら、プレイヤーの移動も失敗
            if(!success) { return false; }
        }
        //プレイヤー・箱関わらずの移動処理
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            int playerIndex = GetPlayerIndex();

            if(playerIndex<map.Length-1)
            {
                map[playerIndex + 1] = 1;
                map[playerIndex] = 0;
            }

            MoveNumber(1,playerIndex,playerIndex+1);
            PrintArray() ;
        }
    }
}
