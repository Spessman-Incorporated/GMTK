using UnityEngine;

public class DiceExplosionController : MonoBehaviour
{
    public void Explode()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
    }
}
