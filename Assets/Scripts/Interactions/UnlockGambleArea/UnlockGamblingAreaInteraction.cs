using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UnlockGamblingAreaInteraction : InteractionBase
{
   
    protected override void OnTriggerStayAction(Collider other)
    {  
        this.gameObject.GetComponentInParent<UnlockBuildingBase>().UnlockArea(true);
    }

    protected override void OnTriggerExitAction(Collider other)
    {
        this.gameObject.GetComponentInParent<UnlockBuildingBase>().UnlockArea(false);

    }
}
