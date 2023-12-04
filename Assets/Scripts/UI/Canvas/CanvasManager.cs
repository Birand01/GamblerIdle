using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CanvasType
{
    HammerStoreUI,
    GameUI,
    WheelGambleUI
}
public class CanvasManager : MonoBehaviour
{
    List<CanvasController> canvasControllerList;
    internal CanvasController lastActiveCanvas;
    private void Awake()
    {
        canvasControllerList = GetComponentsInChildren<CanvasController>().ToList();
        canvasControllerList.ForEach(x => x.gameObject.SetActive(false));
        SwitchCanvas(CanvasType.GameUI);
    }
    private void OnEnable()
    {
        WheelGameInteraction.OnSwitchWheelGambleUI += SwitchCanvas;
        HammerStoreInteraction.OnSwitchHammerStoreUI += SwitchCanvas;
    }
    private void OnDisable()
    {
        HammerStoreInteraction.OnSwitchHammerStoreUI -= SwitchCanvas;
        WheelGameInteraction.OnSwitchWheelGambleUI -= SwitchCanvas;

    }
    private void SwitchCanvas(CanvasType canvasType)
    {

        if (lastActiveCanvas != null)
        {
            lastActiveCanvas.gameObject.SetActive(false);
        }
        CanvasController desiredCanvas = canvasControllerList.Find(x => x.canvasType == canvasType);
        if (desiredCanvas != null)
        {
            desiredCanvas.gameObject.SetActive(true);
            lastActiveCanvas = desiredCanvas;
        }
        else
        {
            Debug.LogWarning("The desired canvas was not found");
        }
    }

}