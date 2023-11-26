using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class DiamondInteraction : InteractionBase
{

    private void OnEnable()
    {
        DiamondInstantiator.OnDiamondKinemacity += Kinemacity;
    }

    protected override void OnDisable()
    {
        DiamondInstantiator.OnDiamondKinemacity -= Kinemacity;

    }

    protected override void OnTriggerEnterAction(Collider other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
            this.gameObject.SetActive(false);
        }
    }


    private IEnumerator Kinemacity(GameObject go)
    {
        yield return new WaitForSeconds(3f);
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.GetComponent<Collider>().isTrigger = true;
    }
}
