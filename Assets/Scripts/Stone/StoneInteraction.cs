using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneInteraction : InteractionBase
{
    public static event Action<bool> OnPlayerBreakObjectAnim;
    protected override void OnTriggerStayAction(Collider other)
    {
        OnPlayerBreakObjectAnim?.Invoke(true);
        other.transform.LookAt(new Vector3(this.transform.position.x,other.transform.position.y,this.transform.position.z));
    }



    protected override void OnTriggerExitAction(Collider other)
    {
        OnPlayerBreakObjectAnim?.Invoke(false);
    }
}
