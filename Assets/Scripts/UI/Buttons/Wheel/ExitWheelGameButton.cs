using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitWheelGameButton : MonoBehaviour
{
    private Button button;

    public static event Action<CanvasType> OnExitWheelGame;
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
        OnExitWheelGame?.Invoke(CanvasType.GameUI);
    }
}
