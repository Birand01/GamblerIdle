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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            RollDice();
        }
    }

    private async void RollDice()
    {
        if(diceToThrow == null)
        { return; }
        foreach (var die in _spawnedDice)
        {
            Destroy(die);
        }
        for (int i = 0; i < amountOfDice; i++)
        {
            Dice dice = Instantiate(diceToThrow, transform.position, transform.rotation);
            _spawnedDice.Add(dice.gameObject);
            dice.RollDice(throwForce, rollForce,i);
            await Task.Yield();
        }
    }
}
