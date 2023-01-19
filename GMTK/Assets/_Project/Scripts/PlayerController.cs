using System;
using FullscreenEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public static bool CanPlaceDice = true;

    [SerializeField] private MovementController _movementController;

    public Transform MovementPoint;
    public LayerMask CollidersLayer;
    
    public GameObject DicePrefab;

    private void Update()
    {
        Vector2 inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (inputAxis != Vector2.zero)
        {
            _movementController.TryMoveToDirection(inputAxis);
        }

        if (Input.GetKeyDown(KeyCode.Space) && CanPlaceDice)
        {
            PlaceDice();
        }
    }

    private void PlaceDice()
    {
        Instantiate(DicePrefab, MovementPoint.position, Quaternion.identity);
    }
}
