using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;

public class HammerDurability : MonoBehaviour
{
    public static event Action<bool> OnChangeStateOfDiggingAnimation;
   

    [SerializeField] private Color finalColor;
    private Color initialColor;
    [SerializeField] internal float takenDamage, totalHammerDurability;
    private MeshRenderer meshRenderer;
   

   
    private void OnEnable()
    {
        HammerDurabilityUI.OnUpdateHammerCurrentDuration += UpdateHammerColor;
        DecreaseHammerDamageButton.OnDecreaseHammerDamage += OnDecreaseHammerDamageEvent;
        StoneAreaInteraction.OnTakeDamageFromHammer += DurabilityOfHammer;
    }
    private void OnDisable()
    {
        HammerDurabilityUI.OnUpdateHammerCurrentDuration -= UpdateHammerColor;
        StoneAreaInteraction.OnTakeDamageFromHammer -= DurabilityOfHammer;
        DecreaseHammerDamageButton.OnDecreaseHammerDamage -= OnDecreaseHammerDamageEvent;
    }
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        initialColor=meshRenderer.material.color;
        finalColor = Color.red;
    }

    

    private void DurabilityOfHammer()
    {
        takenDamage = Mathf.Clamp(takenDamage, 0, totalHammerDurability);
        UpdateHammerColor(takenDamage, totalHammerDurability);
      
        if (takenDamage >=totalHammerDurability)
        {
            OnChangeStateOfDiggingAnimation?.Invoke(false);

        }
        else
        {
         
            takenDamage += Time.deltaTime/3f;
            OnChangeStateOfDiggingAnimation?.Invoke(true);
        }
    }

    private void OnDecreaseHammerDamageEvent(float  amount)
    {
        takenDamage-=amount;
        takenDamage=Mathf.Clamp(takenDamage,0, totalHammerDurability);
        UpdateHammerColor(takenDamage, totalHammerDurability);
    }

    private void UpdateHammerColor(float damageAmount,float totalDurability)
    {
        meshRenderer.material.color = Color.Lerp(initialColor, finalColor, damageAmount / totalDurability);

    }
}
