using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public abstract class RotatorBase : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();
    [SerializeField] protected float rotationSpeed;
    private void OnEnable()
    {
        StartCoroutine(Subscribe());
    }
    private void OnDisable()
    {
        subscriptions.Clear();
    }

    protected virtual IEnumerator Subscribe()
    {
        yield return null;

        this.UpdateAsObservable().Subscribe(x =>
        {

            RotationStyle();
        })
            .AddTo(subscriptions);
    }

    protected virtual void RotationStyle() { }
}
