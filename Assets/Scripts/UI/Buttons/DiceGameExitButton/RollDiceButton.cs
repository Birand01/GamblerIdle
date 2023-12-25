using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;


public class RollDiceButton : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();

    private Button button;
   
   
    public static event Action OnRollDice;

    private void OnEnable()
    {
        StartCoroutine(Subscribe());
        DiceAwardManager.OnDiceGameBetValue += IsButtonInteractable;
        Dice.OnDiceRollScale += ScaleOfButton;
       
    }
    private void OnDisable()
    {
        Dice.OnDiceRollScale -= ScaleOfButton;
        DiceAwardManager.OnDiceGameBetValue -= IsButtonInteractable;
    }
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    private void Start()
    {
        button.onClick.AddListener(OnButtonClickEvent);
    }
    private IEnumerator Subscribe()
    {
        yield return null;
        this.UpdateAsObservable()
            .Subscribe(value =>
            {
                

            })
            .AddTo(subscriptions);

    }

    private void ScaleOfButton(Vector3 vector3,Ease ease)
    {
        this.gameObject.transform.DOScale(vector3, 0.2f).SetEase(ease);
    }
   

    private void OnButtonClickEvent()
    {
       OnRollDice?.Invoke();
    }

    private void IsButtonInteractable(bool state)
    {
        if (state)
        {
            button.interactable = state;
        }
        else
        {
            button.interactable = false;
        }
    }
}
