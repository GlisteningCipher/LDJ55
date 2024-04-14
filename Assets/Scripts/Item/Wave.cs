//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new wave", menuName = "ScriptableObjects/Wave")]
public class Wave : ScriptableObject
{
    public Item[] items;
    public int[] amounts;
}
