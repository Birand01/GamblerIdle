using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Experimental.GlobalIllumination;

public class DiceTable : MonoBehaviour
{
    [SerializeField] private Light spotLight; 
    public static event Action<CanvasType> OnSwitchDiceGambleUI;

    private void Awake()
    {
        spotLight = GetComponentInChildren<Light>();
    }
   
    private void OnEnable()
    {
        ExitDiceGameButton.OnSwitchMainCamera += ScaleDiceTable;
        DiceGameInteraction.OnSwitchDiceGameCamera += ScaleDiceTable;
    }
    private void OnDisable()
    {
        DiceGameInteraction.OnSwitchDiceGameCamera -= ScaleDiceTable;
        ExitDiceGameButton.OnSwitchMainCamera -= ScaleDiceTable;

    }


    private void ScaleDiceTable(CameraType cameraType)
    {
        if(cameraType==CameraType.diceGameCamera)
        {
           
            this.gameObject.transform.DOScale(0.03f, 2f).SetEase(Ease.InCubic).onComplete += OnDiceEvent; 
        }
        else
        {
            spotLight.intensity = 0f;
            this.gameObject.transform.DOScale(0f, 2f).SetEase(Ease.OutCubic);
        }
    }

    private void OnDiceEvent()
    {
        OnSwitchDiceGambleUI?.Invoke(CanvasType.DiceGambleUI);
        spotLight.intensity = 7.5f;
    }
}
