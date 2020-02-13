using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    private Vector3 offset;

    void Start()
    {
        offset = GetComponent<Transform>().position; //最初の時点でのプレイヤーのポジションを取得
    }

    void Update()
    {
        Vector3 diff = transform.position - offset;
        if (diff.magnitude > 0.01f) //ベクトルの長さが0.01fより大きい場合にプレイヤーの向きを変える処理を入れる(0では入れないので）
        {
            transform.rotation = Quaternion.LookRotation(diff);  //ベクトルの情報をQuaternion.LookRotationに引き渡し回転量を取得しプレイヤーを回転させる
        }

        offset = transform.position; //プレイヤーの位置を更新
    }
}