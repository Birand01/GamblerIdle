using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public abstract class HammerStoreButton : MonoBehaviour
{
    [SerializeField] protected TMP_Text priceText, amountText;
    protected Button button;
    protected virtual void Awake()
    {

        button = GetComponent<Button>();
    }
    protected virtual void Start()
    {
        button.onClick.AddListener(OnButtonClickEvent);
    }

    protected virtual void OnButtonClickEvent()
    {
    }

}
