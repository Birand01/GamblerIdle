using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wheel : MonoBehaviour
{
    [SerializeField] private float rotationAmount,rotationDuration;
    internal bool isSpinButtonEnable=true;
    private void OnEnable()
    {
        SpinButton.OnSpinWheel += SpinWheel;
    }
    private void OnDisable()
    {
        SpinButton.OnSpinWheel -= SpinWheel;

    }
    private void SpinWheel(Button spinButton)
    {
        isSpinButtonEnable = false;
        float randomAngle = Random.Range(0, 360);
        float rotateAngles = (360 * rotationAmount) + randomAngle;
        this.transform.DOLocalRotate(new Vector3(0, 0, rotateAngles * -1), rotationDuration, RotateMode.FastBeyond360).OnComplete(() =>
            isSpinButtonEnable = true
            );
    }
}
