using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New DataList", menuName = "DataList/New BookDataList")]

public class BookDataList_SO : ScriptableObject
{
	public List<ItemBookData> listItemBookData;
	public List<UseItemBookData> listUseItemBookData;
	public List<BuildingBookData> listBuldingBookData;
	public List<PlantBookData> listPlantBookData;

	public List<BookData> listBookData;

	public void BookInst()
	{
		listBookData.Clear();
		foreach (var item in listItemBookData)
		{
			listBookData.Add(item);
		}
		foreach (var item in listUseItemBookData)
		{
			listBookData.Add(item);
		}
		foreach (var item in listBuldingBookData)
		{
			listBookData.Add(item);
		}
		foreach (var item in listPlantBookData)
		{
			listBookData.Add(item);
		}
	}
}
