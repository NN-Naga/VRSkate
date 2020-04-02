using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    public Text Result;
    public Text CountDownText;
    public PlayerData playerDataAsset;
    private bool end = false;
    public AudioClip SelectSound;
    public AudioClip ClearSound;
    AudioSource audioSource;

    public void ChangeGameScene()
    {
        audioSource.PlayOneShot(SelectSound);
        transform.Find("Title").gameObject.SetActive(false);
        transform.Find("HighScore").gameObject.SetActive(false);
        transform.Find("StartButton").gameObject.SetActive(false);
        transform.Find("Count").gameObject.SetActive(true);

        StartCoroutine("CountdownDisplay");
        
    }
    
    public void ChangeTitleScene()
    {
        audioSource.PlayOneShot(SelectSound);
        SceneManager.LoadScene("VRSkateGameTitle");
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name == "VRSkateGameTitle")
        {
            PathControl.RapInfo = 0;
            PathControl.TimeInfo = 0f;
            PathControl.DistanceInfo = 0f;
            float highScore = playerDataAsset.time;
            string Result2 = "HIGH SCORE " + TimeOutput(highScore, "m") + ":" + TimeOutput(highScore, "s") + "." + TimeOutput(highScore, "ms");
            Result.text = "<size=32><color=#ffffff>" + Result2 + "</color></size>";
        }
        if (SceneManager.GetActiveScene().name == "VRSkateGameMain")
        {
            StartCoroutine("CountdownDisplay");
            
        }
    }

    IEnumerator CountdownDisplay()
    {
        if (SceneManager.GetActiveScene().name == "VRSkateGameTitle")
        {
            for (int i = 3; i > 0; i--)
            {
                CountDownText.text = i.ToString();
                yield return new WaitForSeconds(1.0f);
            }
            SceneManager.LoadScene("VRSkateGameMain");
        }
        if (SceneManager.GetActiveScene().name == "VRSkateGameMain")
        {
            CountDownText.text = "START!";
            yield return new WaitForSeconds(1.0f);
            transform.Find("Count").gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int Rap = PathControl.RapInfo;
        if (SceneManager.GetActiveScene().name == "VRSkateGameMain" && Rap >= 3 && !end)
        {
            transform.Find("Status").gameObject.SetActive(false);
            transform.Find("Result").gameObject.SetActive(true);
            GameResult();
            end = true;
        }
    }

    void GameResult()
    {
        float totalTime = PathControl.TimeInfo;
        string HighScoreResult = "";

        if (totalTime < playerDataAsset.time)
        {
            playerDataAsset.time = totalTime;
            HighScoreResult = "<size=48><color=#ffff7c>NEW RECORD!</color></size>\n";
        }

        float highScore = playerDataAsset.time;

        string Result1 = "TIME " + TimeOutput(totalTime, "m") + ":" + TimeOutput(totalTime, "s") + "." + TimeOutput(totalTime, "ms");
        string Result2 = "HIGH SCORE " + TimeOutput(highScore, "m") + ":" + TimeOutput(highScore, "s") + "." + TimeOutput(highScore, "ms");
        Result.text = HighScoreResult + "<size=64><color=#ffffff>"+Result1+"</color></size>"+"\n<size=32><color=#cccccc>" + Result2 + "</color></size>";

        audioSource.PlayOneShot(ClearSound);
    }

    float TimeOutput(float time, string unit)
    {
        if(unit == "m")
            return Mathf.Floor(time / 60f);
        if(unit == "s")
            return Mathf.Floor(time % 60f);
        if(unit == "ms")
            return Mathf.Floor(time % 1f * 100f);

        return 0;
    }

}