using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public abstract class InteractionBase : MonoBehaviour
{
    private CompositeDisposable _disposable = new CompositeDisposable();
    protected Collider _collider = null;

    protected virtual void OnTriggerStayAction(Collider other) { }
    protected virtual void OnTriggerEnterAction(Collider other)
    {

    }
    protected virtual void OnTriggerExitAction(Collider other) { }
    protected virtual void OnCollisionEnterAction(Collision collision) { }

    private void Reset()
    {
       
    }
    protected virtual void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;

    }
    protected virtual void Start()
    {
        OnTriggerEnterSubscription();
        OnTriggerExitSubscription();
        OnTriggerStaySubscription();
        OnCollisionEnter2DSubscription();
    }

    private void OnTriggerStaySubscription()
    {
        _collider.OnTriggerStayAsObservable().Subscribe(other =>
        {
            OnTriggerStayAction(other);
        }).AddTo(_disposable);
    }

    private void OnTriggerEnterSubscription()
    {
        _collider.OnTriggerEnterAsObservable().Subscribe(other =>
        {
            OnTriggerEnterAction(other);
        }).AddTo(_disposable);

    }

    private void OnCollisionEnter2DSubscription()
    {
        _collider.OnCollisionEnterAsObservable().Subscribe(other =>
        {
            OnCollisionEnterAction(other);

        }).AddTo(_disposable);
    }

    private void OnTriggerExitSubscription()
    {
        _collider.OnTriggerExitAsObservable().Subscribe(other =>
        {
            OnTriggerExitAction(other);
        }).AddTo(_disposable);
    }

    protected virtual void OnDisable()
    {
        _disposable.Clear();

    }
}
