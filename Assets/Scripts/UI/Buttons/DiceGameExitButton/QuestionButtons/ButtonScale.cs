using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScale : MonoBehaviour
{
    private void OnEnable()
    {
        Dice.OnDiceRollScale += ScaleOfButton;
    }
    private void OnDisable()
    {
        Dice.OnDiceRollScale -= ScaleOfButton;

    }


    private void ScaleOfButton(Vector3 vector3, Ease ease)
    {
        this.gameObject.transform.DOScale(vector3, 0.2f).SetEase(ease);
    }
}
