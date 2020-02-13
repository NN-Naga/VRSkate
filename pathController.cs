using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathController : MonoBehaviour {
    public int speed;
    public string PathName = "NewPath1";
    //[SerializeField] private float moveSpeed;              // 移動速度

    void Start()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash(
            "path", iTweenPath.GetPath(PathName),
            "speed", 0,
            "easeType", "linear",
            "orienttopath", true,
            "loopType", "loop"));
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.W))
            speed += 1;
        iTween.MoveAdd(this.gameObject, iTween.Hash(
            "speed", speed));
        
    }
}
