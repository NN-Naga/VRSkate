using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Text Result;
    public PlayerData playerDataAsset;

    public void ChangeGameScene()
    {
        SceneManager.LoadScene("VRSkateMain");
    }
    public void ChangeResultScene()
    {
        SceneManager.LoadScene("VRSkateResult");
    }
    public void ChangeTitleScene()
    {
        SceneManager.LoadScene("VRSkateTitle");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "VRSkateResult")
        {
            float totalTime = playerDataAsset.time;
            float Time60 = Mathf.Floor(totalTime / 60f);
            float Time1 = Mathf.Floor(totalTime % 60f);
            float Time001 = Mathf.Floor(totalTime % 1f * 100f);
            Result.text = "TIME\n" + Time60 +":"+ Time1 +"."+ Time001
                + "\nDISTANCE\n" + playerDataAsset.distance.ToString("f2") + "m";
        }
        if (SceneManager.GetActiveScene().name == "VRSkateTitle")
        {
            cs_PathControl.RapInfo = 0;
            cs_PathControl.TimeInfo = 0f;
            cs_PathControl.DistanceInfo = 0f;
        }
    }


    // Update is called once per frame
    void Update()
    {
        int Rap = cs_PathControl.RapInfo;
        if(SceneManager.GetActiveScene().name == "VRSkateMain" && Rap >= 3)
        {
            float time = cs_PathControl.TimeInfo;
            playerDataAsset.time = time;
            float distance = cs_PathControl.DistanceInfo;
            playerDataAsset.distance = distance;
            ChangeResultScene();
        }
    }

}
