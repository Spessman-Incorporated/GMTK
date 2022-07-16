using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float CharacterMovementDelay;
    public Transform mesh;

    public bool CanMove;

    private void Update()
    {
        ProcessPlayerInput();
    }

    private void ProcessPlayerInput()
    {
        if (!CanMove)
        {
            return;
        }

        Vector2Int input = new Vector2Int((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"));

        // if both movement axis are pressed
        if (input.x != 0 && input.y != 0)
        {
            input.x = 0;
        }

        Vector3 targetPosition = transform.position + (transform.forward * input.y) + (transform.right * input.x);

        if (input.magnitude == 0)
        {
            return;
        }

        InputDelayCoroutine();
        //mesh.LookAt(targetPosition);
        mesh.DOLookAt(targetPosition, CharacterMovementDelay);
        transform.DOMove(targetPosition, CharacterMovementDelay).SetEase(Ease.OutQuad);
    }

    private async void InputDelayCoroutine()
    {
        CanMove = false;

        TimeSpan duration = TimeSpan.FromSeconds(CharacterMovementDelay);
        await UniTask.Delay(duration);
        
        CanMove = true;
    }
}
