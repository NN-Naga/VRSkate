using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightCalibration : MonoBehaviour
{
    private static GameObject mainObject;
    private static Transform camTransform;
    private Vector3 campos;
    public Text HeightInfo;
    public PlayerData playerDataAsset;

    public void SetPosition(float diff)
    {
        playerDataAsset.height += diff;
        campos.y = playerDataAsset.height;
        camTransform.position = campos;
    }
 
    public void HeightUp()
    {
        SetPosition(0.1f);
        HeightInfo.text = "Height\n"+ camTransform.position.y + "m";

    }

    public void HeightDown()
    {
        SetPosition(-0.1f);
        HeightInfo.text = "Height\n" + camTransform.position.y + "m";
    }

    // Start is called before the first frame update
    void Start()
    {
        mainObject = gameObject.transform.parent.gameObject;
        camTransform = mainObject.transform;
        campos = camTransform.position;
        SetPosition(0f);
    }

}
