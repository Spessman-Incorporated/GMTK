using System;
using System.Collections;
using System.Collections.Generic;
using Coimbra.Services.Events;
using UnityEngine;

public class DiceResultSystem : MonoBehaviour
{
    public static int DiceResult;
    public int Diceresult2;
    
    private void Start()
    {
        DiceResultEvent.AddListener(HandleDiceResult);
    }

    private void HandleDiceResult(ref EventContext context, in DiceResultEvent e)
    {
        //Debug.Log($"New result: {e.Result}");
        DiceResult = e.Result;
        Diceresult2 = e.Result;
    }
}
