using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RepairAreaInteraction : InteractionBase
{
  
    protected override void OnTriggerEnterAction(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }

  
}
