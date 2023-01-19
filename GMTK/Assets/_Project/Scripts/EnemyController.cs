using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    
    [Header("Movement")]
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
    private bool _isAttacking;
    private float HorizontalInput;
    private float VerticalInput;
    private const float MovementTolerance = .05f;

    private void Start()
    {
        InvokeRepeating(nameof(SetMovementDirection), 0.5f, 0.5f);
    }

    private void SetMovementDirection()
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

    private async UniTask Attack(float side)
    {
        _isAttacking = true;
        
        await UniTask.Delay(TimeSpan.FromSeconds(TimeUntilCharge));
        
        MovementPoint.position += new Vector3(side, 0f, 0f);
        MovementSpeed = ChargeMovementSpeed;
        
        await UniTask.Delay(TimeSpan.FromSeconds(TimeToChaseAgaing));
        
        //MovementSpeed = NormalMovementSpeed;
        _isAttacking = false;
    }
}
