using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public GameObject ObjectToExplode;
    public bool CanActivate = true;

    public async void ExplodeTile()
    {
        if (!CanActivate)
        {
            return;
        }

        await WaitForSeconds(1);
        ObjectToExplode.SetActive(true);
    }

    public void ResetTile()
    {
        ObjectToExplode.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            CanActivate = false;
        }
    }

    private async UniTask WaitForSeconds(int seconds)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(seconds));
    }
}
