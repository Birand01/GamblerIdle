using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiamondManager : MonoBehaviour
{
    public static event Action<int> OnExchangeDiamondToMoney;

    [SerializeField] private List<Transform> diamondList = new List<Transform>();  
    [SerializeField] private Transform objectHolder;
    [SerializeField] private Ease stackEase;
    private bool state;
    private void OnEnable()
    {
        ExchangeArea.OnPlayerStartDropMoney += DropMoney;
        DiamondInteraction.OnStackDiamond += AddNewItem;
        ExchangeArea.OnPlayerStopDropMoney += OnObjectDropExitEvent;    
    }
    private void OnDisable()
    {
        ExchangeArea.OnPlayerStopDropMoney -= OnObjectDropExitEvent;
        ExchangeArea.OnPlayerStartDropMoney -= DropMoney;
        DiamondInteraction.OnStackDiamond -= AddNewItem;
    }
    private void Awake()
    {
        diamondList.Add(objectHolder);
    }
    private void AddNewItem(Transform _itemToAdd)
    {
        _itemToAdd.DOKill(true);

        _itemToAdd.DOJump(objectHolder.position +
        new Vector3(0, 0.025f * diamondList.Count, 0), 0.5f, 1, 0.1f).OnComplete(
         () =>
         {
             _itemToAdd.DOScale(0.01f, 0.5f);
             _itemToAdd.SetParent(objectHolder, true);
             _itemToAdd.localPosition = new Vector3(0, 0.025f * diamondList.Count, 0);
             _itemToAdd.localRotation = Quaternion.Euler(-90f, 0, 0);
             diamondList.Add(_itemToAdd.transform);
         }
         ).SetEase(stackEase);


    }

    private void DropMoney(Transform dropPosition)
    {
        StartCoroutine(DropCorotuine(dropPosition));

    }
    private IEnumerator DropCorotuine(Transform dropPosition)
    {
        state = true;
        int counter = diamondList.Count - 1;
        for (int i = counter; i >= 1; i--)
        {

            diamondList.ElementAt(i).parent = dropPosition.transform;
            diamondList[i].DOLocalJump(new Vector3(dropPosition.localPosition.x, dropPosition.localPosition.y,
            dropPosition.localPosition.z+50f), 2, 1, 0.1f).SetEase(Ease.InOutBounce);
            StartCoroutine(Disable(diamondList[i]));
            diamondList[i].gameObject.GetComponent<Collider>().isTrigger = false;
            diamondList.RemoveAt(i);
            OnExchangeDiamondToMoney?.Invoke(10);
            if (!state)
            {
                break;
            }

            yield return new WaitForSeconds(0.1f);
        }

    }
    private void OnObjectDropExitEvent()
    {
        state = false;

    }
    private IEnumerator Disable(Transform list)
    {
        yield return new WaitForSeconds(0.5f);
        list.gameObject.SetActive(false);
    }
}
