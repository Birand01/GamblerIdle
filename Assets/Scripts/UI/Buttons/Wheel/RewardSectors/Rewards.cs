using ModestTree;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Rewards : MonoBehaviour
{
  
    private void OnEnable()
    {
       
        //BetOptionBase.OnChangeBetAmount += UpdateSectorValues;
    }
    private void OnDisable()
    {
       // BetOptionBase.OnChangeBetAmount -= UpdateSectorValues;

    }
    private void Awake()
    {
       
    }
   
   



    //internal WheelSectorsSO GetLandedSectorRewardValue(float angle)
    //{
    //    var anglePerSector = 360 / wheelSectorsSO.Count;
    //    WheelSectorsSO sector = wheelSectorsSO[(int)(angle / anglePerSector)];
    //    return sector;
    //}
}
