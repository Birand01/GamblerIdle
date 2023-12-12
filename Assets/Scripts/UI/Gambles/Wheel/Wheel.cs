using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class Wheel : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();
    [Inject] BetAmount betAmount;
    [SerializeField] internal WheelSector[] Sectors;
    private WheelSector _finalSector;
    private float _finalAngle;                  // The final angle is needed to calculate the reward
    private float _startAngle;                  // The first time start angle equals 0 but the next time it equals the last final angle
    private float _currentLerpRotationTime;     // Needed for spinning animation
    internal bool _isStarted;


    public static event Action<int> OnGiveRewardMoney;

    private void OnEnable()
    {
        StartCoroutine(Subscribe());
        SpinButton.OnSpinWheel += OnSpinWheelButtonClickEvent;
    }
    private void OnDisable()
    {
        SpinButton.OnSpinWheel -= OnSpinWheelButtonClickEvent;
        subscriptions.Clear();
    }
    private void Awake()
    {
        SetSectorsValue();
    }
    protected virtual IEnumerator Subscribe()
    {
        yield return null;

        this.UpdateAsObservable().Subscribe(x =>
        {

            RotateWheel();
        })
            .AddTo(subscriptions);
    }

    private void RotateWheel()
    {
        if (!_isStarted)
            return;

        // Animation time
        float maxLerpRotationTime = 4f;

        // increment animation timer once per frame
        _currentLerpRotationTime += Time.deltaTime;

        // If the end of animation
        if (_currentLerpRotationTime > maxLerpRotationTime || this.transform.eulerAngles.z == _finalAngle)
        {
            _currentLerpRotationTime = maxLerpRotationTime;
            _isStarted = false;
            _startAngle = _finalAngle % 360;
            OnGiveRewardMoney?.Invoke(_finalSector.RewardValue * (betAmount.currentBetValue / 100));
            Debug.Log("Reward is" + _finalSector.RewardValue * (betAmount.currentBetValue / 100));

        }
        else
        {
            // Calculate current position using linear interpolation
            float t = _currentLerpRotationTime / maxLerpRotationTime;

            // This formulae allows to speed up at start and speed down at the end of rotation.
            // Try to change this values to customize the speed
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            float angle = Mathf.Lerp(_startAngle, _finalAngle, t);
            this.transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
  

    private void SetSectorsValue()
    {
        // Show sector reward value in text object if it's set
        foreach (var sector in Sectors)
        {
            if (sector.ValueTextObject != null)
            {
                sector.ValueTextObject.GetComponent<TMP_Text>().text = sector.RewardValue.ToString();
            }

        }
    }
   

    private void SetUpTheOccuranceOfASector()
    {

        _currentLerpRotationTime = 0f;

        // All sectors angles
        int[] sectorsAngles = new int[Sectors.Length];

        // Fill the necessary angles (for example if we want to have 12 sectors we need to fill the angles with 30 degrees step)
        // It's recommended to use the EVEN sectors count (2, 4, 6, 8, 10, 12, etc)
        for (int i = 1; i <= Sectors.Length; i++)
        {
            sectorsAngles[i - 1] = 360 / Sectors.Length * i;
        }

        //int cumulativeProbability = Sectors.Sum(sector => sector.Probability);

        double rndNumber = UnityEngine.Random.Range(1, Sectors.Sum(sector => sector.probability));

        // Calculate the propability of each sector with respect to other sectors
        int cumulativeProbability = 0;
        // Random final sector accordingly to probability
        int randomFinalAngle = sectorsAngles[0];
        _finalSector = Sectors[0];
        
       
        for (int i = 0; i < Sectors.Length; i++)
        {
            cumulativeProbability += Sectors[i].probability;

            if (rndNumber <= cumulativeProbability)
            {
                // Choose final sector
                randomFinalAngle = sectorsAngles[i];
                _finalSector = Sectors[i];
                Debug.Log("Sector is " + _finalSector);
              
                Debug.Log("Sector value is " + _finalSector.RewardValue);
              
                break;
            }
        }

        int fullTurnovers = 5;

        // Set up how many turnovers our wheel should make before stop
        _finalAngle = fullTurnovers * 360 + randomFinalAngle;

        // Stop the wheel
        _isStarted = true;
      


    }
    
    private void OnSpinWheelButtonClickEvent()
    {
        SetUpTheOccuranceOfASector();
      
    }
  


}
/**
 * One sector on the wheel
 */
[Serializable]
public class WheelSector
{
    [Tooltip("Text object where value will be placed (not required)")]
    public GameObject ValueTextObject;

    [Tooltip("Value of reward")]
    public int RewardValue = 100;

    [Tooltip("Chance that this sector will be randomly selected")]
    [RangeAttribute(0, 100)]
    public int probability = 100;

}
