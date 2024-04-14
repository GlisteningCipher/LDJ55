//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new wave", menuName = "ScriptableObjects/Wave")]
public class Wave : ScriptableObject
{
    public GameObject item;
    public int amount;
    public float timeToShadow = 5f;
    public float timeToGrab = 10f;
}
