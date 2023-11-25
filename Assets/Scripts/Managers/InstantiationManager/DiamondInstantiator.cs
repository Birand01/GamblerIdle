using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class DiamondInstantiator : MonoBehaviour
{
    [SerializeField] private Transform diamondParent;
    [SerializeField] private GameObject diamondPrefab;
    [SerializeField] private List<Transform> diamondPlaces = new List<Transform>();

  
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            diamondPlaces.Add(transform.GetChild(i));
        }
    }
    private void OnEnable()
    {
        StoneInteraction.OnProduceDiamond += ProduceDiamond;
    }

    private void OnDisable()
    {
        StoneInteraction.OnProduceDiamond -= ProduceDiamond;
    }



    private void ProduceDiamond(int index, float yAxis)
    {
        GameObject diamond = Instantiate(diamondPrefab);
        diamond.transform.position = diamondParent.position;
        diamond.transform.localRotation = Quaternion.Euler(-90f, 0f, 0);
        diamond.transform.SetParent(diamondParent);
        diamond.transform.DOJump(new Vector3(diamondPlaces[index].position.x, diamondPlaces[index].position.y + yAxis+0.4f,
            diamondPlaces[index].position.z), 2f, 1, 0.5f).SetEase(Ease.OutQuad).OnComplete(()=>
            diamond.gameObject.GetComponent<Collider>().isTrigger = false
           
            );


    }
  
}