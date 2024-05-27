using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


public class Move : MonoBehaviour
{
    // 完了までにかかる時間
    private float timeTaken = 0.4f;
    // 経過時間
    private float timeErapsed;
    // 目的地
    private Vector3 destination;
    // 出発地
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
        // 目的地に到着していたら処理しない
        if (origin == destination) { return; }
        // 経過時間を加算
        timeErapsed += Time.deltaTime;
        // 経過時間が完了時間の何割か算出
        float timeRate = timeErapsed / timeTaken;
        // 完了時間を超えるようであれば実行完了時間相当に丸める
        if (timeTaken > 1) { timeRate = 1; }
        // イージング用計算
        float easing = timeRate;
        // 座標を算出
        Vector3 currentPosition=Vector3.Lerp(origin, destination, easing);
        // 算出した座標をpositionに代入
        transform.position = currentPosition;
    }

    public void MoveTo(Vector3 newDestination)
    {
        // 経過時間を初期化
        timeErapsed = 0;
        // 移動中の可能性があるので、現在地とpositionに前回移動の目的地を代入
        origin= destination;
        // 新しい目的地を代入
        transform.position = origin;
        destination = newDestination;
    }
}

