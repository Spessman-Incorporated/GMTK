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
        gameObject.Destroy();
    }

    private async UniTask WaitForSeconds(float seconds)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(seconds));
    }
}
