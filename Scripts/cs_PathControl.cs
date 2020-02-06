/*
 * @file cs_PathControl
 * @attention なし 
 * @note  なし 
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cs_PathControl : MonoBehaviour
{
    Vector3[] NodeData;
    iTweenPath cp_ITweenPath; //!< Instance pointer for 'ITween Path' Script
    float PositionPercent = 0; //!< 0.00~1.00. パス全体の移動距離に対し％で指定する値 
    public string PathName = "NewPath1";

    /*!
     * Visual Path Editorで作成済みのパスデータを取得
     *  @param[in] pathname 取得するパス名
     *  @return     取得結果  true:成功  false:失敗
     *  @note     Visual Path Editorで作成済みのパスデータ Node 1~n を NodeData[]へ格納する 
     *  @attention    呼び出しタイミングはStart()以降とすること。 
     *        iTweenPath.GetPath()内部のパス名リスト更新が OnEnable()のタイミングで行われている。 
     *        よってAwakeではまだ確定していない場合がありえる為、Start()以降のタイミングで呼び出すこと。 
     */


    Hashtable table = new Hashtable();
    void SetupPath()
    {
        table.Add("easetype", iTween.EaseType.easeInOutSine);
        table.Add("onstart", "cb_iTweenStart");  //Handler func when iTween start
        table.Add("onstartparams", "Start");   //parameter of Handler func when iTween start
        table.Add("oncomplete", "cb_iTweenComplete"); //Handler func when iTween end
        table.Add("oncompleteparams", "Complete"); //parameter of Handler func when iTween end

        table.Add("orienttopath", true); //Head to forwarding vector (*Important*)
        table.Add("lookTime", 1.0f); //Time value for heading nose (*Important*)
        table.Add("loopType", "loop");

        iTween.MoveTo(gameObject, table);
    }
    void cb_iTweenStart(string param)
    {
        Debug.Log("[iTween] cb_iTweenStart: " + param);
    }
    void cb_iTweenComplete(string param)
    {
        Debug.Log("[iTween] cb_iTweenComplete: " + param);
        iTween.MoveTo(gameObject, table);//Restart iTween.
    }



    bool SetPathPosition_from_ITweenPath(string PathName)
    {
        if (cp_ITweenPath == null)
        {
            cp_ITweenPath = GetComponent<iTweenPath>(); // このObject内の ITween path スクリプトコンポーネントのインスタンスを取得 
            if (cp_ITweenPath == null)
            {
                Debug.Log("[NO SCRIPT] No there script 'ITween Path' on this object");
                return (false);
            }
        }
        // GetPath()はStatic宣言されているので直呼び出し  
        NodeData = iTweenPath.GetPath(PathName); // Visual Path Editor のパス名を指定してパスデータを取得する。 
        if (NodeData == null)
        {
            Debug.Log("[NOT FOUND/null] path name '" + PathName + "'");
            return (false);
        }
        return (true);
    }
    float addSpeed = 0;
    private static int rap = 0;

    void Drive_Increase(bool drive)
    {
        if(drive == true && addSpeed <= 0.001f) //ボタン降下で加速
            addSpeed += 0.0005f;
        //Debug.Log ("Drive_Increase" + addSpeed);
        PositionPercent += addSpeed;
        addSpeed -= 0.000002f; //摩擦力
        if (PositionPercent > 1.0f)
        {
            PositionPercent = 0.0f;
            rap++;
        }
    }
    void Drive_Decrease()
    {
        //Debug.Log ("Drive_Decrease");
        PositionPercent -= addSpeed;
        if (PositionPercent < 0.0f) PositionPercent = 1.0f;
    }

    public GameObject trackedObject;
    public GameObject baseObject;
    private Vector3 offset;
    private Vector3 diff;



    void Watch_UI_Input()
    {
        bool drive_Fwd = false;
        bool drive_Bwd = false;

        //Begin: Control by Keyboard
        bool input_Key_W = Input.GetKeyDown(KeyCode.W);
        bool input_Key_S = Input.GetKey(KeyCode.S);
        if (!(input_Key_W & input_Key_S))
        { // Disable multi press
            drive_Fwd |= input_Key_W;
            drive_Bwd |= input_Key_S;
        }
        //End: Control by Keyboard
        /*
#if UNITY_ANDROID
  //#if UNITY_IPHONE
  //Begin: Control by Accel Sensor
  float accel_X = Input.acceleration.x; // 加速度センサ 横傾き 
  //Debug.Log ("accel_X:"+accel_X);
  if(accel_X <-0.2f){
   drive_Fwd |= true;
  }
  else if(accel_X >0.2f){
   drive_Bwd |= true;
  }
  //End: Control by Accel Sensor
#endif
        */
        
        diff = trackedObject.transform.position - baseObject.transform.position - offset;
        //Debug.Log("position = " + offset + "   vector = " + diff.magnitude);
        if (diff.magnitude > 0.02f) //ベクトルの長さが0.1fより大きい場合に加速する処理を入れる
        {
            drive_Fwd = true;  //プレイヤーを加速させる
        }
        offset = trackedObject.transform.position - baseObject.transform.position;
        


        if (drive_Fwd || addSpeed > 0.0001f) Drive_Increase(drive_Fwd); //ボタン降下または加速度が0になるまで呼び出し
        if (drive_Bwd) Drive_Decrease();
        //if (drive_Fwd | drive_Bwd) Debug.Log("Position is " + PositionPercent * 100.0f + "%"); //移動したなら表示 
    }

    void Start()
    {
        //DontDestroyOnLoad(this);
        SetPathPosition_from_ITweenPath(PathName);
        offset = trackedObject.transform.position - baseObject.transform.position; //相対位置を取るため、トラッカーの位置とプレイヤーの位置の差分を取る。
    }

    public Text Info;
    private static float time;
    public PlayerData playerDataAsset;
    private Vector3 campos;
    private static float distance;

    public static int RapInfo
    {
        set{ rap = value; }
        get{ return rap; }
    }
    public static float TimeInfo
    {
        set { time = value; }
        get { return time; }
    }
    public static float DistanceInfo
    {
        set { distance = value; }
        get { return distance; }
    }

    void Update()
    {
        Watch_UI_Input();
        time = Time.timeSinceLevelLoad;
        transform.position = iTween.PointOnPath(NodeData, PositionPercent); // Get target position 
        campos = transform.position;
        campos.y = playerDataAsset.height;
        transform.position = campos;
        distance += diff.magnitude;
        Info.text = "Vector = " + diff.magnitude.ToString("f3")
                    + "\nRap : " + rap
                    + "\nTime : " + time
                    + "\nDistance : " + distance.ToString("f2") + "m";
    }
}