//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new spell", menuName = "ScriptableObjects/Spell")]
public class Spell : ScriptableObject
{
    public Wave[] waves = default;
}
