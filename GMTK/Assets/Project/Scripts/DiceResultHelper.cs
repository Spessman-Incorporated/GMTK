using System;
using UnityEngine;

public class DiceResultHelper : MonoBehaviour
{
    public int Side;

    private void OnTriggerEnter(Collider other)
    {
        DiceResultDetector detector = other.transform.GetComponent<DiceResultDetector>();

        if (detector != null)
        {
            new DiceResultEvent(Side).Invoke(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        throw new NotImplementedException();
    }
}
