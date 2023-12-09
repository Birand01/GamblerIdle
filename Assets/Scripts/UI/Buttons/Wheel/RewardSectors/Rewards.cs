using ModestTree;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Rewards : MonoBehaviour
{
    [SerializeField] internal List<GameObject> sectors;
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
        for (int i = 0; i < transform.childCount; i++)
        {
            sectors.Add(this.transform.GetChild(i).gameObject);
        }
    }
   
    internal SectorConfiguration GetSector(float angle)
    {
        var anglePerSector=360/sectors.Count;
        return sectors[(int)((angle)/anglePerSector)].gameObject.GetComponent<SectorConfiguration>();
    }



    //internal WheelSectorsSO GetLandedSectorRewardValue(float angle)
    //{
    //    var anglePerSector = 360 / wheelSectorsSO.Count;
    //    WheelSectorsSO sector = wheelSectorsSO[(int)(angle / anglePerSector)];
    //    return sector;
    //}
}
