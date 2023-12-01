using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "HammerStoreButton", menuName = "ScriptableObjects/HammerStoreButton", order = 0)]
public class StoreButtonSO : ScriptableObject
{
    public string title;
    public int price;
    public int skillAmount;
    public int multiplier;
}
