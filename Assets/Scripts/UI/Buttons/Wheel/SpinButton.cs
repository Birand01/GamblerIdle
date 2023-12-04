using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinButton : MonoBehaviour
{
    public static event Action OnSpinWheel;
    private Button spinButton;

    private void Awake()
    {
        spinButton = GetComponent<Button>();
    }

    private void Start()
    {
        spinButton.onClick.AddListener(OnButtonClickEvent);
    }

    private void OnButtonClickEvent()
    {
        OnSpinWheel?.Invoke();
    }
}
