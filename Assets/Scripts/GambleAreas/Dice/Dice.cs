using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using System.Threading.Tasks;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour
{
   
    private CompositeDisposable subscriptions = new CompositeDisposable();
    [SerializeField] private Transform[] diceFaces;
    private Rigidbody rb;
    private int _diceIndex = -1;
    private int topFace = 0;
    private bool _hasStoppedRolling, _delayFinished;

    
    public static event Action<int,int> OnDiceResult;
   
    public static event Action<Vector3,Ease> OnDiceRollScale;
    
    private int topface1,topface2=0;
    internal int sum, multiplication, difference;
   
    private void Awake()
    {
       
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
      
        StartCoroutine(Subscribe());
       
       
    }
    private void OnDisable()
    {
       
        subscriptions.Clear();
      
      

    }
    private IEnumerator Subscribe()
    {
        yield return null;
        this.UpdateAsObservable()
            .Subscribe(value =>
            {
                if (!_delayFinished)
                {
                    return;
                }
                RolledDicesValue();

            })
            .AddTo(subscriptions);

    }

    private void RolledDicesValue()
    {
        if(!_hasStoppedRolling && rb.velocity.sqrMagnitude==0)
        {
            _hasStoppedRolling = true;
            GetNumberOnTopFace();
        }
    }

    private int GetNumberOnTopFace()
    {
        if(diceFaces==null)
        {
            return -1;
        }
        var topFace = 0;
        var lastYPosition = diceFaces[0].position.y;
        for (int i = 0; i < diceFaces.Length; i++)
        {
            if (diceFaces[i].position.y>lastYPosition)
            {
                lastYPosition = diceFaces[i].position.y;
                topFace = i;
            }
        }
        OnDiceResult?.Invoke(_diceIndex, topFace + 1);
        Debug.Log($"Dice result{topFace +1}");
        return topFace + 1;
    }

   



    internal void RollDice(float throwForce, float rollForce, int i)
    {
        OnDiceRollScale?.Invoke(Vector3.zero,Ease.InBounce);
        _diceIndex = i;
        var randomVariance= UnityEngine.Random.Range(-1f, 1f);
        rb.AddForce(transform.forward * (throwForce + randomVariance), ForceMode.Impulse);
        var randX = UnityEngine.Random.Range(0, 1f);
        var randY = UnityEngine.Random.Range(0, 1f);
        var randZ = UnityEngine.Random.Range(0, 1f);
        rb.AddTorque(new Vector3(randX, randY, randZ)*(rollForce+randomVariance),ForceMode.Impulse);
        DelayedResult();

    }

    private async void DelayedResult()
    {
        await Task.Delay(2000);
        _delayFinished = true;
        OnDiceRollScale?.Invoke(Vector3.one, Ease.InOutBounce);      
    }


}
