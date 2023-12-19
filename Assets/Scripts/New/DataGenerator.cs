using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGenerator : MonoBehaviour
{
    [Space]
    public List<NPCData> npcDatas;
    public Transform npcParent;
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
            NPC newNPC = newNPCObj.AddComponent<NPC>();
            newNPC.ID = i;
            newNPC.npcName = randomNPCData.NPCName;
            newNPC.age = randomNPCData.age;
            newNPC.deposit = randomNPCData.deposit;
        }
    }
}