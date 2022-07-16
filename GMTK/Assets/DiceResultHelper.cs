using System;
using System.Collections;
using System.Collections.Generic;
using Coimbra.Services.Events;
using UnityEngine;

public partial struct DiceResultEvent : IEvent
{
    public int Result;

    public DiceResultEvent(int result)
    {
        Result = result;
    }
}

public class DiceResultHelper : MonoBehaviour
{
    public int Side;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);

        DiceResultDetector detector = other.transform.GetComponent<DiceResultDetector>();

        if (detector != null)
        {
            new DiceResultEvent(Side).Invoke(this);
        }

    }
}
