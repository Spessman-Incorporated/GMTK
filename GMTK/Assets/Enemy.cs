using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float CharacterMovementDelay;

    public bool CanMove;

    public bool CanMoveForward;
    public bool CanMoveBackward;
    public bool CanMoveLeft;
    public bool CanMoveRight;

    public Transform mesh;

    public Transform Player;

    private void Update()
    {
        ProcessPlayerInput();
    }

    private void Move(Vector2Int input)
    {
        bool movingOnBlocked = false;

        // if both movement axis are pressed
        if (input.x != 0 && input.y != 0)
        {
            input.x = 0;
            movingOnBlocked = true;
        }

        if (!CanMoveForward && input.y == 1)
        {
            input.y = 0;
            movingOnBlocked = true;
        }

        if (!CanMoveBackward && input.y == -1)
        {
            input.y = 0;
            movingOnBlocked = true;
        }

        if (!CanMoveRight && input.x == 1)
        {
            input.x = 0;
            movingOnBlocked = true;
        }

        if (!CanMoveLeft && input.x == -1)
        {
            input.x = 0;
            movingOnBlocked = true;
        }

        Vector3 targetPosition = transform.position + (transform.forward * input.y) + (transform.right * input.x);

        if (input.magnitude == 0 || movingOnBlocked)
        {
            return;
        }

        InputDelayCoroutine();
        transform.DOMove(targetPosition, CharacterMovementDelay).SetEase(Ease.OutQuad);
    }

    private void ProcessPlayerInput()
    {
        if (!CanMove)
        {
            return;
        }

        Debug.Log("Move enemy");

        Vector3 position = transform.position;
        position.y += 0.5f;

        if (Physics.Raycast(position, transform.forward, out RaycastHit hit, .8f))
        {
            Debug.DrawRay(position, transform.forward, Color.magenta);
            CanMoveForward = false;

            if (hit.transform.gameObject != null)
            {
                Debug.Log(hit.transform.name);
            }
        }
        else
        {
            CanMoveForward = true;
        }

        if (Physics.Raycast(position, -transform.forward, out RaycastHit hit2, .8f))
        {
            Debug.DrawRay(position, -transform.forward, Color.green);
            CanMoveBackward = false;

            if (hit2.transform.gameObject != null)
            {
                Debug.Log(hit2.transform.name);
            }
        }
        else
        {
            CanMoveBackward = true;
        }

        if (Physics.Raycast(position, transform.right, out RaycastHit hit3, .8f))
        {
            Debug.DrawRay(position, transform.right, Color.red);
            CanMoveRight = false;

            if (hit3.transform.gameObject != null)
            {
                Debug.Log(hit3.transform.name);
            }
        }
        else
        {
            CanMoveRight = true;
        }

        if (Physics.Raycast(position, -transform.right, out RaycastHit hit4, .8f))
        {
            Debug.DrawRay(position, -transform.right, Color.blue);
            CanMoveLeft = false;

            if (hit4.transform.gameObject != null)
            {
                Debug.Log(hit4.transform.name);
            }
        }
        else
        {
            CanMoveLeft = true;
        }

        Vector3 input = transform.position + mesh.forward;
        Vector2Int movement = new Vector2Int((int)input.x, (int)input.y);
        Move(movement);
    }

    private async void InputDelayCoroutine()
    {
        CanMove = false;

        TimeSpan duration = TimeSpan.FromSeconds(CharacterMovementDelay);
        await UniTask.Delay(duration);

        CanMove = true;
    }

}
