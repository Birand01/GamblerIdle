using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DiceThrower : MonoBehaviour
{
    [SerializeField] private Dice diceToThrow;
    private int amountOfDice=2;
    [SerializeField] private float throwForce, rollForce;

    private List<GameObject> _spawnedDice=new List<GameObject>();
  
    private void OnEnable()
    {
        RollDiceButton.OnRollDice += RollDice;
    }
    private void OnDisable()
    {
        RollDiceButton.OnRollDice -= RollDice;

    }

    private IEnumerator RollDice()
    {
        if (diceToThrow == null)
        { yield return null; }
      
        foreach (var die in _spawnedDice)
        {
            die.gameObject.SetActive(false);
            // TO DO : USE oBJECT POOLING
        }
        for (int i = 0; i < amountOfDice; i++)
        {
            Dice dice = Instantiate(diceToThrow,new Vector3(transform.position.x+i,transform.position.y,transform.position.z), transform.rotation);
            dice.transform.SetParent(this.transform);
            _spawnedDice.Add(dice.gameObject);
            dice.RollDice(throwForce, rollForce, i);
            yield return new WaitForSeconds(0.1f);

        }
    }

   
}
