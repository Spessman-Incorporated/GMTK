using UnityEngine;

public class DiceExplosionController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionVfx;

    public bool IsObstructed { get; private set; }

    public void Explode()
    {
        _explosionVfx.Play();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Colliders"))
        {
            IsObstructed = true;
        }
    }
}
