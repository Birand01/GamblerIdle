using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
public class WheelRotator : RotatorBase
{

    protected override void RotationStyle()
    {
        this.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }
  
}
