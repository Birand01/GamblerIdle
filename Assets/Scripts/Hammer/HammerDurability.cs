using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;

public class HammerDurability : MonoBehaviour
{
  

 
    [SerializeField] private Color finalColor,initialColor;
    [SerializeField] private float takenDamage;
    [SerializeField] internal float totalHammerDurability;
    private MeshRenderer meshRenderer;
   

    public static HammerDurability Instance { get;private set; }
    private void OnEnable()
    {
        StoneAreaInteraction.OnTakeDamageFromHammer += DurabilityOfHammer;
    }
    private void OnDisable()
    {
        StoneAreaInteraction.OnTakeDamageFromHammer -= DurabilityOfHammer;

    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        meshRenderer = GetComponent<MeshRenderer>();
        initialColor=meshRenderer.material.color;
        finalColor = Color.red;
    }

    

    private void DurabilityOfHammer(float damage)
    {
        takenDamage = damage;
        meshRenderer.material.color = Color.Lerp(initialColor, finalColor, takenDamage / totalHammerDurability);
        takenDamage=Mathf.Clamp(takenDamage, 0, totalHammerDurability);
    }


}
