using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour{
    public static DiceManager instance;
    
    [SerializeField]
    float ThrowPower = 5f;
    public List<Dice> dices;
    public event Action<int> OnAllDiceAreStopped;

    private void Awake(){
        instance = this;
    }

    private void Start()
    {
        foreach (var dice in dices)
        {
            dice.diceCallBack += DiceResultCallback;
        }
        //OnAllDiceAreStopped += Test_AllDiceStopped;
    }
    private void OnDestroy()
    {
        foreach (var dice in dices)
        {
            dice.diceCallBack -= DiceResultCallback;
        }
    }
    //void Test_AllDiceStopped(int res)
    //{
    //    Debug.Log("res : " + res);
    //}
    int diceCount = 0;
    int dicePointCount = 0;
    private void DiceResultCallback(int obj)
    {

        diceCount++;
        dicePointCount += obj;

        if (diceCount == dices.Count)
        {
            OnAllDiceAreStopped?.Invoke(dicePointCount);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RollDices();
        }
    }

    [Button]
    public void RollDices()
    {
        diceCount = 0;
        dicePointCount = 0;
        foreach (Dice dice in dices)
        {
            dice.RollDice(ThrowPower);
        }
    }
}
