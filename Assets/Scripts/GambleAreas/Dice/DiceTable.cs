using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceTable : MonoBehaviour
{

    private void OnEnable()
    {
        DiceGameInteraction.OnActivateDiceTable += ScaleTable;
    }
    private void OnDisable()
    {
        DiceGameInteraction.OnActivateDiceTable -= ScaleTable;

    }


    private void ScaleTable(bool state)
    {
        if(state)
        {
            this.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InBounce);

        }
        else
        {
            this.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InCubic);
        }
    }
}
