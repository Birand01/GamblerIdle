using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StoneAreaInteraction : InteractionBase
{
    public static event Action<bool> OnStoneAreaInteractionExit;
    public static event Action<int, float> OnProduceDiamond;
    public static event Action OnTakeDamageFromHammer;

    [Inject] HammerDurability hammerDurability;  

    [SerializeField] private float diamondProduceCounter, diamondProduceTime;
    private int diamondPlaceIndex;
    private float yAxis;
    protected override void OnTriggerStayAction(Collider other)
    {   
      
        PlayerRotationEvent(other);
        HammerDamageEvent();
        DiamondProduceEvent();

    }
    protected override void OnTriggerExitAction(Collider other)
    {
        yAxis = 0;
        diamondProduceCounter = 0;
        diamondPlaceIndex = 0;
        OnStoneAreaInteractionExit?.Invoke(false);
    }

    private void HammerDamageEvent()
    {

        OnTakeDamageFromHammer?.Invoke();
       
      

    }


    private void PlayerRotationEvent(Collider other)
    {
        other.transform.LookAt(new Vector3(this.transform.parent.position.x, other.transform.position.y, this.transform.parent.position.z));

    }
    private void DiamondProduceEvent()
    {
        if(hammerDurability.takenDamage<hammerDurability.totalHammerDurability)
        {
            diamondProduceCounter += Time.deltaTime;
            if (diamondProduceCounter >= diamondProduceTime)
            {

                OnProduceDiamond?.Invoke(diamondPlaceIndex, yAxis);
                diamondPlaceIndex++;
                if (diamondPlaceIndex >= 7)
                {
                    diamondPlaceIndex = 0;
                    yAxis += 0.6f;
                }

                diamondProduceCounter = 0;
            }
        }
       
    }



    
}
