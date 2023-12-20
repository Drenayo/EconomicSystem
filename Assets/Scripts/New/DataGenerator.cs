using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGenerator : MonoBehaviour
{
    [LabelText("NPC模板")]
    public List<NPCData> npcDatas;
    [LabelText("NPC父物体")]
    public Transform npcParent;
    [LabelText("生成数量")]
    public int randomNPCNumber = 20;
    



    void Start()
    {
        GenerateRandomNPC();
    }





    /// <summary>
    /// 生成NPC数据
    /// </summary>
    void GenerateRandomNPC()
    {
        for (int i = 0; i < randomNPCNumber; i++) 
        {
            // 从数组中随机选择一个NPCData
            NPCData randomNPCData = npcDatas[Random.Range(0, npcDatas.Count)];
            GameObject newNPCObj = new GameObject();
            newNPCObj.transform.parent = npcParent;
            newNPCObj.gameObject.name = randomNPCData.NPCName;
            NPC newNPC = newNPCObj.AddComponent<NPC>();
            newNPC.id = i;
            newNPC.npcName = randomNPCData.NPCName;
            newNPC.age = randomNPCData.age;
            newNPC.deposit = randomNPCData.deposit;

        }
    }
}