using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour
{
   
    private CompositeDisposable subscriptions = new CompositeDisposable();
    [SerializeField] private Transform[] diceFaces;
    private Rigidbody rb;
    private int _diceIndex = -1;
    private int topFace = 0;
    private bool _hasStoppedRolling, _delayFinished;

    public delegate IEnumerator OnDiceResultEventHandler(int a, int b);
    public static event OnDiceResultEventHandler OnDiceResult;
    public delegate IEnumerator OnDiceCalculationEventHandler(int a, int b,int c);
    public static event OnDiceCalculationEventHandler OnDiceCalculation;
    public static event Action<bool> OnDisableRollDiceButton;
    
    private int topface1,topface2=0;
    internal int sum, multiplication, difference;
   
    private void Awake()
    {
       
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        OnDiceResult += DiceCalculations;
        StartCoroutine(Subscribe());
       
       
    }
    private void OnDisable()
    {
        OnDiceResult -= DiceCalculations;
        subscriptions.Clear();
      
      

    }
    private IEnumerator Subscribe()
    {
        yield return null;
        this.UpdateAsObservable()
            .Subscribe(value =>
            {
                GetTopFacesOfDices();
               

            })
            .AddTo(subscriptions);

    }
   
    private void GetTopFacesOfDices()
    {
        if (!_delayFinished) { return; }
        if (!_hasStoppedRolling && rb.velocity.sqrMagnitude == 0f)
        {
            _hasStoppedRolling = true;
            GetNumberOnTopFace();
           
        }
    }

    [ContextMenu("Get Top Face")]
    private int GetNumberOnTopFace()
    {
        if(diceFaces==null) return -1;
        var lastYPosition = diceFaces[0].position.y;
        for (int i = 0; i < diceFaces.Length; i++)
        {
            if (diceFaces[i].position.y>lastYPosition)
            {
                lastYPosition = diceFaces[i].position.y;
                topFace=i;
            }
        }

        
       StartCoroutine( OnDiceResult?.Invoke(_diceIndex,topFace + 1));
       

        //Debug.Log($"DICE RESULT IS : {topFace + 1} and " + "DICE INDEX IS :" + _diceIndex);
        return topFace + 1;
    }
    



    private IEnumerator DiceCalculations(int diceIndex,int diceValue)
    {  
        if(diceIndex==0)
        {
            topface1 = diceValue;
        }
        else if(diceIndex==1)
        {
            topface2 = diceValue;

          
        }
        yield return new WaitForSeconds(1.5f);
        sum = topface1 + topface2;
        multiplication = topface1 * topface2;
        difference = Mathf.Abs(topface1 - topface2);
        StartCoroutine(OnDiceCalculation?.Invoke(sum,multiplication,difference));
 

        Debug.Log("SUM :" + sum + "--" + "MULTIPLICATION :" + multiplication + "--" + "DIFFERENCE : " + difference);
        yield return null;
    }

    

 
   
   
    internal void RollDice(float throwForce, float rollForce, int i)
    {
        OnDisableRollDiceButton?.Invoke(false);
        _diceIndex = i;
        var randomVariance= UnityEngine.Random.Range(-1f, 1f);
        rb.AddForce(transform.forward * (throwForce + randomVariance), ForceMode.Impulse);
        var randX = UnityEngine.Random.Range(0, 1f);
        var randY = UnityEngine.Random.Range(0, 1f);
        var randZ = UnityEngine.Random.Range(0, 1f);
        rb.AddTorque(new Vector3(randX, randY, randZ)*(rollForce+randomVariance),ForceMode.Impulse);
        StartCoroutine(DelayResult());

    }

    private IEnumerator DelayResult()
    {
        yield return new WaitForSeconds(1f);
        _delayFinished = true;
        yield return new WaitForSeconds(2f);
       
        OnDisableRollDiceButton?.Invoke(true);
    }

   
}
