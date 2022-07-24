using System;
using FullscreenEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public static bool CanPlaceDice = true;

    public float MovementSpeed;
    public Transform MovementPoint;
    public LayerMask CollidersLayer;
    
    public GameObject DicePrefab;

    public Tilemap GroundTileMap;
    
    private Transform PlayerTransform;
    private const float MovementTolerance = .05f;

    private void Awake()
    {
        PlayerTransform = transform;
        MovementPoint.parent = null;
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanPlaceDice)
        {
            PlaceDice();
        }
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

    private void PlaceDice()
    {
        Instantiate(DicePrefab, MovementPoint.position, Quaternion.identity);
    }
}
