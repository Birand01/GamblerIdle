using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitDiceGameButton : MonoBehaviour
{
    private Button button;
    public static event Action<CanvasType> OnExitDiceGameCanvas;
    public static event Action<CameraType> OnSwitchMainCamera;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    private void Start()
    {
        button.onClick.AddListener(OnButtonClickEvent);
    }

    private void OnButtonClickEvent()
    {
        OnExitDiceGameCanvas?.Invoke(CanvasType.GameUI);
        OnSwitchMainCamera?.Invoke(CameraType.mainCamera);

    }
}
