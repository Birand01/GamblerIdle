using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotator : RotatorBase
{
    protected override void RotationStyle()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
