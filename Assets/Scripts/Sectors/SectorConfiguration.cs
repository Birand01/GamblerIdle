using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SectorConfiguration : MonoBehaviour
{
    [SerializeField] internal WheelSectorsSO sectorsSO;
    private TMP_Text rewardText;
    private void Awake()
    {
        rewardText = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        SetUpConfiguration();
    }
    private void SetUpConfiguration()
    {
        rewardText.text = sectorsSO.RewardValue.ToString();
    }
}
