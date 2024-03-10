using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class BookData
{
    public int bookID;//ID
    public string bookName;//名称
    public Level bookLevel;//等级
    public Sprite bookIcon;//图标
    public int finishValueMax;//完成度
    public BookType bookType;//图鉴类型
    public MakeType makeType;//制作类型
    public int learnAbilityID;//学习图鉴所需能力ID


    public ChAddValue chAddValueL2;//等级1加成
    public ChAddValue chAddValueL3;//等级1加成
    public ChAddValue chAddValueL4;//等级1加成
    public ChAddValue chAddValueL5;//等级1加成

}