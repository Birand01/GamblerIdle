using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneInteraction : InteractionBase
{
    public static event Action<bool> OnPlayerBreakObjectAnim;
    public static event Action<int, float> OnProduceDiamond;

    [SerializeField] private float timer, totalTimeSpentToProduceDiamond;
    [SerializeField] private int diamondPlaceIndex;
    private float yAxis;
    protected override void OnTriggerStayAction(Collider other)
    {
        timer += Time.deltaTime;
        if (timer >= totalTimeSpentToProduceDiamond)
        {

            OnProduceDiamond?.Invoke(diamondPlaceIndex, yAxis);
            diamondPlaceIndex++;
            if (diamondPlaceIndex >= 8)
            {
                diamondPlaceIndex = 0;
                yAxis += 0.6f;
            }
         
            timer = 0;
        }

        OnPlayerBreakObjectAnim?.Invoke(true);
        other.transform.LookAt(new Vector3(this.transform.parent.position.x, other.transform.position.y, this.transform.parent.position.z));


    }
    



    protected override void OnTriggerExitAction(Collider other)
    {
        yAxis= 0; 
        timer = 0;
        diamondPlaceIndex = 0;
      
        OnPlayerBreakObjectAnim?.Invoke(false);
    }
}
