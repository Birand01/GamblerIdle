using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private float rotationAmount,rotationDuration;
    [SerializeField] private Transform wheel;
   
    private void SpinWheel()
    {
        float randomAngle = Random.Range(0, 360);
        float rotateAngles = (360 * rotationAmount) + randomAngle;
        wheel.DOLocalRotate(new Vector3(0, 0, rotateAngles * -1), rotationDuration, RotateMode.FastBeyond360);
    }
}
