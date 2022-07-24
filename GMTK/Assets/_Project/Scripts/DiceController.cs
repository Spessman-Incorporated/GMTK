using System;
using System.Collections.Generic;
using System.Linq;
using Coimbra;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

public class DiceController : MonoBehaviour
{
    public SpriteRenderer DiceSprite;
    public List<Sprite> DiceNumbers =  new List<Sprite>();
    public float ChangeSideTime;

    public List<GameObject> UpExplosions = new List<GameObject>();
    public List<GameObject> DownExplosions = new List<GameObject>();
    public List<GameObject> LeftExplosions = new List<GameObject>();
    public List<GameObject> RightExplosions = new List<GameObject>();
    
    private void Awake()
    {
        RollTheDice();
    }
    
    private async void RollTheDice()
    {
        PlayerController.CanPlaceDice = false;
        
        Random rng = new Random();
        List<Sprite> spriteList = new List<Sprite>(DiceNumbers.OrderBy(item => rng.Next()));

        foreach (Sprite sprite in spriteList)
        {
            DiceSprite.sprite = sprite;
            await WaitForSeconds(ChangeSideTime);
        }

        PlayerController.CanPlaceDice = true;

        ExplodeCells();
    }

    public async void ExplodeCells()
    {
        DiceSprite.gameObject.SetActive(false);

        foreach (GameObject explostion in UpExplosions)
        {
            explostion.GetComponent<DiceExplosionController>().Explode();
        }
        
        foreach (GameObject explostion in DownExplosions)
        {
            explostion.GetComponent<DiceExplosionController>().Explode();
        }
        
        foreach (GameObject explostion in LeftExplosions)
        {
            explostion.GetComponent<DiceExplosionController>().Explode();
        }
        
        foreach (GameObject explostion in RightExplosions)
        {
            explostion.GetComponent<DiceExplosionController>().Explode();
        }
        
        await WaitForSeconds(0.5f);

        gameObject.Destroy();
    }

    private async UniTask WaitForSeconds(float seconds)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(seconds));
    }
}
