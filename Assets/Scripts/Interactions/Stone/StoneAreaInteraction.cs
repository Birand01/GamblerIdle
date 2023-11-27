using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneAreaInteraction : InteractionBase
{
    public static event Action<bool> OnChangeStateOfDiggingAnimation;
    public static event Action<int, float> OnProduceDiamond;
    public static event Action<float> OnTakeDamageFromHammer;

    [SerializeField] private float diamondProduceCounter, diamondProduceTime,hammerDamageCounter;
    private int diamondPlaceIndex;
    private float yAxis;
    protected override void OnTriggerStayAction(Collider other)
    {   
      
        PlayerRotationEvent(other);
        HammerDamageEvent();
       

    }

    private void HammerDamageEvent()
    {

        OnTakeDamageFromHammer?.Invoke(hammerDamageCounter);
        if(hammerDamageCounter>=HammerDurability.Instance.totalHammerDurability)
        {
            OnChangeStateOfDiggingAnimation?.Invoke(false);

        }
        else
        {
            hammerDamageCounter += Time.deltaTime;
            DiamondProduceEvent();
            OnChangeStateOfDiggingAnimation?.Invoke(true);
        }

    }


    private void PlayerRotationEvent(Collider other)
    {
        other.transform.LookAt(new Vector3(this.transform.parent.position.x, other.transform.position.y, this.transform.parent.position.z));

    }
    private void DiamondProduceEvent()
    {
        diamondProduceCounter += Time.deltaTime;
        if (diamondProduceCounter >= diamondProduceTime)
        {

            OnProduceDiamond?.Invoke(diamondPlaceIndex, yAxis);
            diamondPlaceIndex++;
            if (diamondPlaceIndex >= 4)
            {
                diamondPlaceIndex = 0;
                //    yAxis += 0.6f;
            }

            diamondProduceCounter = 0;
        }
    }



    protected override void OnTriggerExitAction(Collider other)
    {
        yAxis= 0; 
        diamondProduceCounter = 0;
        diamondPlaceIndex = 0;   
        OnChangeStateOfDiggingAnimation?.Invoke(false);
    }
}
