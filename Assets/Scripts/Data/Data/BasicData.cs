using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// NPC角色
/// </summary>
public class ChDataInst
{
    public int ChIndex;//ID
    public SexType CharacterSex;/// 性别
	public BasicData chBasicInst;
}
public enum SexType
{
    Man,WoMan
}


/// <summary>
/// NPC基础数据
/// </summary>
public class BasicData
{
    public int hunger;
    public int hungerSpend;/// 饥饿消耗							
	public int hungerSafe;// 饥饿安全
    public int hungerMax;

    public int energy;// 体力
    public int energySpend;
    public int energySafe;
    public int energyMax;

    public int weight = 0;//负重
    public int weightMax = 0;


 //   public AnimalType animalType;
    public int age;// 年龄
}