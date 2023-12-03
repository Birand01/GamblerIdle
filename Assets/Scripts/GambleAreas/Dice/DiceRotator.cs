using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRotator : RotatorBase
{
    private List<Transform> dices = new List<Transform>();
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            dices.Add(transform.GetChild(i));
        }
    }
    protected override void RotationStyle()
    {
        foreach (Transform t in dices)
        {
            t.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
   
  
}
