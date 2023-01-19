using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementController : MonoBehaviour
{
    [SerializeField] private int _movementRange;
    [SerializeField] private LayerMask _collisionLayerMask;
    [SerializeField] private Tilemap _walkableTilemap;

    private bool _isMoving;

    public void TryMoveToDirection(Vector3 direction)
    {
        Vector3Int targetTile = GetTileOnDirection(direction);

        Move(targetTile);
    }

    private Vector3Int GetTileOnDirection(Vector3 direction)
    {
        if (direction == Vector3.zero || (int)Mathf.Abs(direction.x) == (int)Mathf.Abs(direction.y))
        {
            return Vector3Int.back;
        }

        int currentRangeCheck = _movementRange;
        bool collisionHit = GetCollisionOnDirection(direction * currentRangeCheck);

        while (collisionHit && currentRangeCheck > 1)
        {
            currentRangeCheck--;
            collisionHit = GetCollisionOnDirection(direction * currentRangeCheck);
        }

        if (collisionHit)
        {
            return Vector3Int.back;
        }

        Vector3Int tilePosition = _walkableTilemap.WorldToCell(
            transform.position + direction * currentRangeCheck);

        return tilePosition;
    }

    private bool GetCollisionOnDirection(Vector3 direction)
    {
        RaycastHit2D collisionHit = Physics2D.Raycast(transform.position,
            direction, direction.magnitude, _collisionLayerMask);

        return collisionHit;
    }

    private void Move(Vector3Int targetTile)
    {
        if (_isMoving || targetTile == Vector3Int.back)
        {
            return;
        }

        _isMoving = true;

        Vector3 targetTilePosition = _walkableTilemap.GetCellCenterWorld(targetTile);

        transform.DOMove(targetTilePosition, 0.4f).OnComplete(() => _isMoving = false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawRay(transform.position, Vector3.right * _movementRange);
        Gizmos.DrawRay(transform.position, Vector3.down * _movementRange);
        Gizmos.DrawRay(transform.position, Vector3.left * _movementRange);
        Gizmos.DrawRay(transform.position, Vector3.up * _movementRange);
    }
}
