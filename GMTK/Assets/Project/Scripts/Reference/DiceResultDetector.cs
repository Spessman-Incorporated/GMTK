using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceResultDetector : MonoBehaviour
{
    public int Side;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DiceResultHelper diceResultHelper));
        {
            if (diceResultHelper != null)
            {
                Side = diceResultHelper.Side;

                DiceResultSystem.DiceResult = Side;
            }
        }
    }
}
