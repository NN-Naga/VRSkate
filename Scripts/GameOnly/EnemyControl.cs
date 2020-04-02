using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public enum MoveType
    {
        Position = 1, Rotation = 2
    }
    public MoveType Type;
    
    public EnemyMove moveParam = new EnemyMove(0, 0, 0, 1f, 1f, 1f);
    public EnemyRotate rotateParam = new EnemyRotate(EnemyRotate.Direction.Left, 1f);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        //Gizmo はワールド座標指定なので、相対座標指定の場合はマトリクス変換で移動する
        Gizmos.matrix = Matrix4x4.TRS(this.transform.position, this.transform.rotation,
                                      this.transform.localScale);
        switch (Type)
        {
            case MoveType.Position:
                Gizmos.DrawLine(Vector3.zero, moveParam.direction);
                break;

        }
    }

    void Start()
    {
        InvokeRepeating("TurnDirection", moveParam.startTime, moveParam.movingTime);
        if (rotateParam.LR == EnemyRotate.Direction.Left)
            rotateParam.speed = -rotateParam.speed;
    }

    void Update()
    {
        
        switch (Type)
        {
            case MoveType.Position:
                MovePosition();
                break;

            case MoveType.Rotation:
                MoveRotation();
                break;

            case (MoveType)(-1):
                MovePosition();
                MoveRotation();
                break;
        }

    }

    private bool call = true;
    void MovePosition()
    {
        this.transform.position += moveParam.direction * moveParam.speed * Time.deltaTime;
    }
    void MoveRotation()
    {
        this.transform.Rotate(new Vector3(0,rotateParam.speed,0));
    }
    void TurnDirection()
    {
        moveParam.direction = -moveParam.direction;
    }

    /** 移動パラメタ */
    [System.Serializable]
    public class EnemyMove
    {
        public Vector3 direction = Vector3.zero;
        public float speed = 1f;
        public float startTime = 1f;
        public float movingTime = 1f;

        public EnemyMove(float x, float y, float z, float speed, float startTime, float movingTime)
        {
            this.direction = new Vector3(x, y, z);
            this.speed = speed;
            this.startTime = startTime;
            this.movingTime = movingTime;
        }
    }

    /** 回転パラメタ */
    [System.Serializable]
    public class EnemyRotate
    {
        public enum Direction { Left = 1, Right = 2 }
        public Direction LR;
        public float speed = 1f;

        public EnemyRotate(Direction LR, float speed)
        {
            this.LR = LR;
            this.speed = speed;
        }
    }
}
