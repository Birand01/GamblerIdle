using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Wheel : MonoBehaviour
{
    [Inject] Rewards rewards;
    [SerializeField] private float rotationAmount,rotationDuration;
    internal bool isSpinButtonEnable=true;
    float randomAngle;
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
        randomAngle =Random.Range(0, 360);
        Debug.Log("Angle is= " +randomAngle);
        float rotateAngles = (360 * rotationAmount) + randomAngle;

        var anglePerSector = 360 / rewards.sectors.Count;
        SectorConfiguration sectorConfiguration = rewards.sectors[(int)((randomAngle) / anglePerSector)].gameObject.GetComponent<SectorConfiguration>();

        Debug.Log("Sector is = " + sectorConfiguration);
        Debug.Log("Sector value is = " + sectorConfiguration.sectorsSO.RewardValue);
        this.transform.DOLocalRotate(new Vector3(0, 0, rotateAngles * -1), rotationDuration, RotateMode.FastBeyond360)
            .onComplete += CalculateSector;
    }

    private void CalculateSector()
    {
        //var anglePerSector = 360 / rewards.wheelSectorsSO.Count;
        //WheelSectorsSO sector = rewards.wheelSectorsSO[(int)(randomAngle / anglePerSector)];
        //Debug.Log(randomAngle);

        //Debug.Log("Sector is = " + sector);
        //Debug.Log("Sector value is = " + sector.RewardValue);
        isSpinButtonEnable = true;
    }
    

}
