using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 作为模板存在的，随机生成各种NPC
/// </summary>
[CreateAssetMenu(menuName = "NPC")]
public class NPCData : ScriptableObject
{
    public string NPCName { get {return  GenerateRandomName(); } }
    public GenderType genderType;
    public int age;
    public int deposit;

    static string GenerateRandomName()
    {
        string[] surnames = {
            "张", "王", "李", "赵", "陈", "刘", "黄", "周", "吴", "徐",
            "朱", "秦", "尤", "许", "何", "吕", "施", "张", "孔", "曹",
            "严", "华", "金", "魏", "陶", "姜", "戚", "谢", "邹", "喻",
            "柏", "水", "窦", "章", "云", "苏", "潘", "葛", "奚", "范",
            "彭", "郎", "韦", "昌", "马", "苗", "凤", "花", "方", "俞",
            "任", "袁", "柳", "鲍", "史", "唐", "费", "廉", "岑", "薛",
            "雷", "贺", "倪", "汤", "藤", "殷", "罗", "毕", "郝", "邬",
            "安", "常", "乐", "于", "时", "傅", "皮", "卞", "齐", "康",
            "伍", "余", "元", "卜", "顾", "孟", "平", "黄", "和", "穆",
        };

        string[] commonCharacters = {
            "伟", "娜", "秀", "强", "军", "磊", "艳", "杰", "涛", "婷",
            "明", "芳", "超", "红", "飞", "丽", "勇", "雅", "涵", "翔",
            "静", "宇", "婉", "浩", "蕾", "鹏", "婧", "宁", "豪", "娟",
            "伦", "晓", "刚", "婕", "杰", "欣", "宏", "青", "瑞", "波",
            "雪", "刚", "莉", "辉", "丹", "鑫", "霞", "宁", "博", "娴",
            "俊", "倩", "鹏", "婷", "杨", "健", "婉", "智", "娣", "峰",
            "玲", "宇", "娇", "伦", "晨", "欣", "旭", "莹", "涛", "薇",
            "良", "蓉", "杰", "洋", "艺", "瑶", "健", "雨", "华", "洁",
            "林", "欢", "鑫", "婕", "泽", "晴", "宇", "倩", "风", "宝",
            "骏", "璐", "尧", "芷", "彤", "煜", "璇", "峥", "晨", "凡",
            "菲", "熙", "哲", "宜", "烁", "倚", "纯", "琦", "莘", "航",
            "睿", "颖", "卓", "蔚", "霖", "琪", "乐", "欢", "瑾", "朔",
            "铭", "萌", "颢", "曜", "佳", "珂", "奕", "宁", "豫", "希",
            "荧", "晖", "潇", "翎", "琰", "晋", "焱", "洵", "瑜", "晗",
            "懿", "悦", "蔓", "婧", "婵", "婉", "潆", "漩", "忆", "勰",
            "倩", "淇", "瀚", "奇", "嫣", "翰", "沁", "琳", "依", "妍",
            "睐", "旻", "珩", "谕", "沛", "钰", "梦", "淼", "锦", "畅",
            "霏", "灵", "燕", "艺", "瑜", "琰", "予", "诗", "馨", "黎",
        };

        System.Random random = new System.Random();

        string randomSurname = surnames[random.Next(surnames.Length)];

        int nameLength = random.Next(2, 3); // 生成3-4个字的名字

        string randomName = randomSurname;

        for (int i = 0; i < nameLength; i++)
        {
            string randomCharacter = commonCharacters[random.Next(commonCharacters.Length)];
            randomName += randomCharacter;
        }

        return randomName;
    }
}
public enum GenderType
{
    Male,
    Female
}
