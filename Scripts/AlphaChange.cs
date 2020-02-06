using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaChange : MonoBehaviour
{
    MeshRenderer meshrender;
    public PlayerData playerDataAsset;
    public float alpha;
    public Text AlphaInfo;

    public void AlphaUp()
    {
        meshrender.material.color += new Color(0, 0, 0, 0.1f);
        playerDataAsset.alpha += 0.1f;
        AlphaInfo.text = "Alpha\n" + playerDataAsset.alpha;
    }

    public void AlphaDown()
    {
        meshrender.material.color -= new Color(0, 0, 0, 0.1f);
        playerDataAsset.alpha -= 0.1f;
        AlphaInfo.text = "Alpha\n" + playerDataAsset.alpha;
    }
    // Start is called before the first frame update
    void Start()
    {
        meshrender = GetComponent<MeshRenderer>();
        alpha = playerDataAsset.alpha;
        meshrender.material.color += new Color(0, 0, 0, alpha);
    }
}
