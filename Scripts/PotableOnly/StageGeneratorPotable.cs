using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGeneratorPotable : MonoBehaviour
{
    const float FirstStagePosition = -400f;
    int currentTipIndex;

    public GameObject[] stageTips;
    public int startTipIndex;
    public int preInstantiate;
    public List<GameObject> generatedStageList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        currentTipIndex = startTipIndex - 1;
        UpdateStage(preInstantiate);
    }

    // Update is called once per frame
    void Update()
    {
        int charaPositionIndex = PathControlPotable.RapInfo;
        //ステージ更新処理
        if(charaPositionIndex + preInstantiate > currentTipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }
    }

    void UpdateStage(int toTipIndex)
    {
        if (toTipIndex <= currentTipIndex) return;

        //指定のステージチップまで作成
        for(int i = currentTipIndex + 1; i <= toTipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);
            generatedStageList.Add(stageObject); //リスト追加
            TransferStage();
        }
        //ステージ削除
        while (generatedStageList.Count > preInstantiate + 1)
            DestroyOldestStage();

        currentTipIndex = toTipIndex;
    }

    GameObject GenerateStage(int tipIndex)
    {
        int nextStageTip = Random.Range(0, stageTips.Length);

        GameObject stageObject = (GameObject)Instantiate(
            stageTips[nextStageTip],
            new Vector3(FirstStagePosition, 0, 0),
            Quaternion.identity
            );

        return stageObject;
    }

    void TransferStage()
    {
        for(int i = 0; i <= generatedStageList.Count - 2; i++)
        {
            GameObject transferObject = generatedStageList[i];
            Transform objectTransform = transferObject.GetComponent<Transform>();
            Vector3 transferPosition = objectTransform.position;
            transferPosition.x += 200f;
            objectTransform.position = transferPosition;
        }
    }

    //ステージ削除
    void DestroyOldestStage()
    {
        GameObject oidStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oidStage);
    }
}
