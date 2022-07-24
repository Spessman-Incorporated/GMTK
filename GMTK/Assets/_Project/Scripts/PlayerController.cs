using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed;
    public Transform MovementPoint;
    public LayerMask CollidersLayer;
    
    private Transform PlayerTransform;
    private const float MovementTolerance = .05f;

    private void Awake()
    {
        PlayerTransform = transform;
        MovementPoint.parent = null;
    }
    
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        PlayerTransform.position = Vector3.MoveTowards(PlayerTransform.position, MovementPoint.position,
            MovementSpeed * Time.deltaTime);

        if (Vector3.Distance(PlayerTransform.position, MovementPoint.position) >= MovementTolerance)
        {
            return;
        }
        
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticallInput = Input.GetAxisRaw("Vertical");

        if (Math.Abs(Mathf.Abs(horizontalInput) - 1f) < MovementTolerance)
        {
            if (!Physics2D.OverlapCircle(MovementPoint.position + new Vector3(horizontalInput, 0f, 0f), .2f, CollidersLayer))
            { 
                MovementPoint.position += new Vector3(horizontalInput, 0f, 0f);
            }
           
        }
        
        if (Math.Abs(Mathf.Abs(verticallInput) - 1f) < MovementTolerance)
        {
            if (!Physics2D.OverlapCircle(MovementPoint.position + new Vector3(0f, verticallInput, 0f), .2f, CollidersLayer))
            {
                MovementPoint.position += new Vector3(0f, verticallInput, 0f);
            }
        }
    }
}
