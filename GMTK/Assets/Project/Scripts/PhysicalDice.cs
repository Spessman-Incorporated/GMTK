using System;
using System.Collections.Generic;
using System.Threading;
using Coimbra.Services.Events;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PhysicalDice : MonoBehaviour
{
    public Action OnDiceRollFinish;
    
    public float DiceMovementDuration;
    public Transform ObjectToRotate;

    public int sideX;
    public int sideY;
    public int sideZ;

    public int[] Sides = new[] { 0, 90, 180, 270 };

    public List<DiceResultHelper> ResultHelpers;

    public bool HitSomething;
    public bool CanBeHit;

    private CancellationTokenSource _cancellationToken = new CancellationTokenSource();

    private void Start()
    {
        DiceCollidedEvent.AddListener(HandleDiceCollided);
        DiceTriggeredEvent.AddListener(HandleDiceTriggered);
    }

    private void HandleDiceTriggered(ref EventContext context, in DiceTriggeredEvent e)
    {
        if (e.Hit.layer == LayerMask.NameToLayer("Walls"))
        {
            HitSomething = true;
        }
    }

    private void HandleDiceCollided(ref EventContext context, in DiceCollidedEvent e)
    {
        Debug.Log("dice collided");
    }

    public void Update()
    {
        if (Application.isEditor && Input.GetKeyDown(KeyCode.Space))
        {
            ShakeDice(1f);
        }
    }

    [ContextMenu("Shake dice")]
    public void ShakeDice(float duration)
    {
        sideX = Sides[Random.Range(0, 4)];
        sideY = Sides[Random.Range(0, 4)];
        sideZ = Sides[Random.Range(0, 4)];

        ObjectToRotate.rotation = Quaternion.Euler(sideX, sideY, sideZ);

        float strength = Random.Range(1000f, 10000f);

        ObjectToRotate.DOShakeRotation(duration, strength, 150, 40f).SetEase(Ease.Linear);
    }

    public async void MoveUntilHitWall(Vector3 direction)
    {
        TimeSpan duration = TimeSpan.FromSeconds(DiceMovementDuration);
        CanBeHit = false;

        while (!HitSomething)
        {
            transform.DOMove(transform.position + direction, DiceMovementDuration).SetEase(Ease.Linear);
            ShakeDice(DiceMovementDuration);
            await UniTask.Delay(duration, cancellationToken: _cancellationToken.Token);
        }

        CanBeHit = true;
        HitSomething = true;
        
        await WaitForSeconds(0.1f);
        
        OnDiceRollFinish?.Invoke();
    }
    
    private async UniTask WaitForSeconds(float seconds)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(seconds));
    }
}
