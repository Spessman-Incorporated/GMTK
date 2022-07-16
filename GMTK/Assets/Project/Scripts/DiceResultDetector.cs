using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceResultDetector : MonoBehaviour
{
    public int Side;
    public int Side2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DiceResultHelper diceResultHelper));
        {
            Side = diceResultHelper.Side;
            Side2 = Side;
        }
    }
}
