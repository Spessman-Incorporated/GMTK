using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCollisionDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        new DiceCollidedEvent(collision.gameObject).Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        new DiceTriggeredEvent(other.gameObject).Invoke(this);
    }
}
