using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "材料")]
public class MaterialData : ScriptableObject
{
    public List<ProductionTask> productionTasks;
}
