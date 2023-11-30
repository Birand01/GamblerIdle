using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class DiamondInteraction : InteractionBase
{
    public static event Action<Transform> OnStackDiamond;
    protected override void OnTriggerEnterAction(Collider other)
    {     
            OnStackDiamond?.Invoke(this.transform);
    }

}
