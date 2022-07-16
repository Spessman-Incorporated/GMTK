using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PhysicalDice : MonoBehaviour
{
    public int sideX;
    public int sideY;
    public int sideZ;

    public int[] Sides = new[] { 0, 90, 180, 270 };

    public List<DiceResultHelper> ResultHelpers;

    public void Update()
    {
        if (Application.isEditor && Input.GetKeyDown(KeyCode.Space))
        {
            ShakeDice();
        }
    }

    [ContextMenu("Shake dice")]
    public void ShakeDice()
    {
        sideX = Sides[Random.Range(0, 4)];
        sideY = Sides[Random.Range(0, 4)];
        sideZ = Sides[Random.Range(0, 4)];

        transform.rotation = Quaternion.Euler(sideX, sideY, sideZ);

        float duration = Random.Range(0.2f, 0.8f);
        float strength = Random.Range(1000f, 10000f);

        transform.DOShakeRotation(duration, strength, 150, 40f);
    }
}
