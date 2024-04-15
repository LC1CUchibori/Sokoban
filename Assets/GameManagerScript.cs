using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class GameManagerScript : MonoBehaviour
{
    //�z��̐錾
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
        //�f�o�b�O���O�̏o��
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
        //�ړ��悪�͈͊O�Ȃ�ړ��s��
        if(moveTo<0||moveTo>=map.Length)
        {
            return false;
        }
        //�ړ���ɂQ��������
        if (map[moveTo]==2)
        {
            //�ǂ̕����Ɉړ����邩�Z�o
            int velocity=moveTo-moveFrom;
            //�v���C���[�̈ړ��悩�炳��ɐ�ɂQ���ړ�����
            //���̈ړ������BMoveNumber���\�b�h����MoveNumber���\�b�h��
            //�ĂсA�������ċA���Ă���B�ړ��s��bool�ŋL�^
            bool success = MoveNumber(2, moveTo, moveTo + velocity);
            //���������ړ����s������A�v���C���[�̈ړ������s
            if(!success) { return false; }
        }
        //�v���C���[�E���ւ�炸�̈ړ�����
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
