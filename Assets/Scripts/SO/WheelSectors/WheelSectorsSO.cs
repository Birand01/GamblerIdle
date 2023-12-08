using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WheelSector", menuName = "ScriptableObjects/WheelSector", order = 1)]
public class WheelSectorsSO : ScriptableObject
{
    public int RewardValue;
    [RangeAttribute(0, 100)] public int Probability;
}
