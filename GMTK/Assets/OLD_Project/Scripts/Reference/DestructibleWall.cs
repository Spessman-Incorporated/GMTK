using System.Collections;
using System.Collections.Generic;
using Coimbra;
using DG.Tweening;
using UnityEngine;

public class DestructibleWall : Entity
{
    public Transform Mesh;
    public ParticleSystem ParticleSystem;
    public int ScaleOutDuration;

    [ContextMenu("Die")]
    protected override void Die()
    {
        base.Die();

        Mesh.DOScale(0, ParticleSystem.main.duration).SetEase(Ease.OutQuad).OnComplete(OnDestroyed);
        ParticleSystem.Play();
    }

    private void OnDestroyed()
    {
        gameObject.Destroy();
    }
}
