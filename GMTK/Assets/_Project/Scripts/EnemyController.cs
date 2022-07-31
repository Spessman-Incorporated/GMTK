using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    public float NormalMovementSpeed = 3f;
    public Transform MovementPoint;
    public Transform PlayerMovementPoint;
    public LayerMask CollidersLayer;
    
    [Header("Attack")]
    public float ChargeMovementSpeed = 6f;
    public float TimeUntilCharge = 1f;
    public float TimeToChaseAgaing = 1f;
    public float ChargeDistanceTrigger = 3f;
    public float TilesPerCharge = 3f;

    private float MovementSpeed;
    private Transform _enemyTransform;
    private bool _isAttacking;
    private float HorizontalInput;
    private float VerticalInput;
    private const float MovementTolerance = .05f;

    private void Awake()
    {
        _enemyTransform = transform;
        MovementSpeed = NormalMovementSpeed;
        MovementPoint.parent = null;
    }

    private void FixedUpdate()
    {
        DefineMovementDirection();
        Move();
    }
    
    private void DefineMovementDirection()
    {
        if (_isAttacking)
        {
            return;
        }
        
        float verticalDistance = PlayerMovementPoint.transform.position.y - MovementPoint.position.y;
        HorizontalInput = 0;
        
        if (verticalDistance >= 1)
        {
            Debug.Log("Player está em cima");
            VerticalInput = 1;
            return;
        }
        
        if (verticalDistance <= -1)
        {
            Debug.Log("Player está embaixo");
            VerticalInput = -1;
            return;
        }
        
        Debug.Log("Player está alinhado horizontalmente");
        float horizontalDistace = PlayerMovementPoint.transform.position.x - MovementPoint.position.x;
        VerticalInput = 0;

        if (horizontalDistace >= 1)
        {
            if (horizontalDistace <= ChargeDistanceTrigger)
            {
                Debug.Log("Pronto para atacar: " + horizontalDistace);
                Attack(TilesPerCharge);
                return;
            }
            
            Debug.Log("Player na direita");
            HorizontalInput = 1;
            return;
        }
        
        if (horizontalDistace <= 1)
        {
            if (horizontalDistace >= -5f)
            {
                Debug.Log("Pronto para atacar: " + horizontalDistace);
                Attack(-TilesPerCharge);
                return;
            }
            
            Debug.Log("Player na esquerda");
            HorizontalInput = -1;
        }
    }

    private void Move()
    {
        _enemyTransform.position = Vector3.MoveTowards(_enemyTransform.position, MovementPoint.position,
            MovementSpeed * Time.deltaTime);

        if (Vector3.Distance(_enemyTransform.position, MovementPoint.position) >= MovementTolerance)
        {
            return;
        }

        if (Math.Abs(Mathf.Abs(HorizontalInput) - 1f) < MovementTolerance)
        {
            if (!Physics2D.OverlapCircle(MovementPoint.position + new Vector3(HorizontalInput, 0f, 0f), .2f, CollidersLayer))
            { 
                MovementPoint.position += new Vector3(HorizontalInput, 0f, 0f);
            }
        }
        else if (Math.Abs(Mathf.Abs(VerticalInput) - 1f) < MovementTolerance)
        {
            if (!Physics2D.OverlapCircle(MovementPoint.position + new Vector3(0f, VerticalInput, 0f), .2f, CollidersLayer))
            {
                MovementPoint.position += new Vector3(0f, VerticalInput, 0f);
            }
        }
    }

    private async UniTask Attack(float side)
    {
        _isAttacking = true;
        
        await UniTask.Delay(TimeSpan.FromSeconds(TimeUntilCharge));
        
        MovementPoint.position += new Vector3(side, 0f, 0f);
        MovementSpeed = ChargeMovementSpeed;
        
        await UniTask.Delay(TimeSpan.FromSeconds(TimeToChaseAgaing));
        
        MovementSpeed = NormalMovementSpeed;
        _isAttacking = false;
    }
}
