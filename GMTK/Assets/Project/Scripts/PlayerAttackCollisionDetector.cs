using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollisionDetector : MonoBehaviour
{
    public Transform dice;

    private void Update()
    {
        ProcessDiceAttack();
    }

    private void ProcessDiceAttack()
    {
        if (Input.GetKeyDown(KeyCode.E) && dice != null)
        {
            PhysicalDice physicalDice = dice.GetComponent<PhysicalDice>();

            if (!physicalDice.CanBeHit)
            {
                return;
            }

            if (Vector3.Dot(Vector3.forward, transform.InverseTransformPoint(dice.transform.position)) > 0)
            {
                Debug.Log("dice in front of player :)");

                //raycast forward to get wall?

                physicalDice.MoveUntilHitWall(transform.forward);               
            }            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DiceCollisionDetector diceCollisionDetector = other.GetComponent<DiceCollisionDetector>();

        if (diceCollisionDetector != null)
        {
            dice = diceCollisionDetector.transform.root;
            Debug.Log("dice");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DiceCollisionDetector diceCollisionDetector = other.GetComponent<DiceCollisionDetector>();

        if (diceCollisionDetector != null)
        {
            dice = null;
            Debug.Log("no dice :(");
        }
    }
}
