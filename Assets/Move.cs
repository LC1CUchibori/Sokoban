using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


public class Move : MonoBehaviour
{
    // �����܂łɂ����鎞��
    private float timeTaken = 0.4f;
    // �o�ߎ���
    private float timeErapsed;
    // �ړI�n
    private Vector3 destination;
    // �o���n
    private Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        destination= transform.position;
        origin = destination;
    }

    // Update is called once per frame
    void Update()
    {
        // �ړI�n�ɓ������Ă����珈�����Ȃ�
        if (origin == destination) { return; }
        // �o�ߎ��Ԃ����Z
        timeErapsed += Time.deltaTime;
        // �o�ߎ��Ԃ��������Ԃ̉������Z�o
        float timeRate = timeErapsed / timeTaken;
        // �������Ԃ𒴂���悤�ł���Ύ��s�������ԑ����Ɋۂ߂�
        if (timeTaken > 1) { timeRate = 1; }
        // �C�[�W���O�p�v�Z
        float easing = timeRate;
        // ���W���Z�o
        Vector3 currentPosition=Vector3.Lerp(origin, destination, easing);
        // �Z�o�������W��position�ɑ��
        transform.position = currentPosition;
    }

    public void MoveTo(Vector3 newDestination)
    {
        // �o�ߎ��Ԃ�������
        timeErapsed = 0;
        // �ړ����̉\��������̂ŁA���ݒn��position�ɑO��ړ��̖ړI�n����
        origin= destination;
        // �V�����ړI�n����
        transform.position = origin;
        destination = newDestination;
    }
}

