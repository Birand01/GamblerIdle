using ModestTree;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rewards : MonoBehaviour
{
    [SerializeField] private WheelSectorsSO[] wheelSectorsSO;
    [SerializeField] private List<GameObject> rewards = new List<GameObject>();
    private void OnEnable()
    {
        SetUpSectorValue();
        BetOptionBase.OnChangeBetAmount += UpdateSectorValues;
    }
    private void OnDisable()
    {
        BetOptionBase.OnChangeBetAmount -= UpdateSectorValues;

    }

    private void Awake()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            rewards.Add(this.transform.GetChild(i).gameObject); 
        }
    }

    private void SetUpSectorValue()
    {
        for(int i = 0;i < rewards.Count;i++)
        {
            rewards[i].GetComponent<TMP_Text>().text = wheelSectorsSO[i].RewardValue.ToString();
        }
    }

    private void UpdateSectorValues(int amount)
    {
        for (int i = 0; i < rewards.Count; i++)
        {
            wheelSectorsSO[i].RewardValue += amount;
            rewards[i].GetComponent<TMP_Text>().text = wheelSectorsSO[i].RewardValue.ToString();
        }
    }

    internal int GetLandedSector(float angle)
    {
        var anglePerCategory = 360 / rewards.Count;
        return (int)(angle/anglePerCategory);
    }
}
