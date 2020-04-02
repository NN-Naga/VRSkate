using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoint : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        //prefabのポジション設定
        GameObject go = (GameObject)Instantiate(
            prefab,
            new Vector3(0, 1.0f, 0),
            Quaternion.identity
            );

        //オブジェクト一括削除設定
        go.transform.SetParent(transform, false);
    }

    // ギズモ表示
    void OnDrawGizmos()
    {
        //オフセット設定
        Vector3 offset = new Vector3(0, 0.5f, 0);

        //球を表示
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position + offset, 0.5f);

        //prefab名アイコン表示
        if (prefab != null)
            Gizmos.DrawIcon(transform.position + offset, prefab.name, true);
    }
}
