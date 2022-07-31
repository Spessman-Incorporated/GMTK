using System;
using System.Collections.Generic;
using System.Linq;
using Coimbra;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceController : MonoBehaviour
{
    
    public SpriteRenderer DiceSprite;
    public List<Sprite> DiceNumbers =  new List<Sprite>();
    public float ChangeSideTime;

    [Tooltip("How many times the die will change sides before exploding")] 
    [SerializeField] private int _changeSideAmount;

    public List<GameObject> UpExplosions = new List<GameObject>();
    public List<GameObject> DownExplosions = new List<GameObject>();
    public List<GameObject> LeftExplosions = new List<GameObject>();
    public List<GameObject> RightExplosions = new List<GameObject>();

    private int _dieSide;
    
    private void Awake()
    {
        RollTheDice();
    }
    
    private async void RollTheDice()
    {
        int dieSpriteIndex = 0;
        int lastSideUp = 0;
        
        PlayerController.CanPlaceDice = false;

        for (int i = 0; i < _changeSideAmount; i++)
        {
            while (dieSpriteIndex == lastSideUp)
            {
                dieSpriteIndex = Random.Range(0, DiceNumbers.Count);
            }

            lastSideUp = dieSpriteIndex;
            _dieSide = dieSpriteIndex + 1;
            DiceSprite.sprite = DiceNumbers[dieSpriteIndex];
            
            await WaitForSeconds(ChangeSideTime);
        }

        PlayerController.CanPlaceDice = true;

        ExplodeCells(_dieSide);
    }

    private async void ExplodeCells(int cellsToBeExploded)
    {
        int cellsExploded = 0;
        
        DiceSprite.gameObject.SetActive(false);

        ExplodeCellLine(UpExplosions);
        ExplodeCellLine(DownExplosions);
        ExplodeCellLine(LeftExplosions);
        ExplodeCellLine(RightExplosions);

        await WaitForSeconds(0.5f);

        gameObject.Destroy();

        void ExplodeCellLine(List<GameObject> cellLine)
        {
            foreach (GameObject explosion in cellLine)
            {
                DiceExplosionController diceExplosion = explosion.GetComponent<DiceExplosionController>();

                if (diceExplosion.IsObstructed)
                {
                    return;
                }
                
                diceExplosion.Explode();
                cellsExploded++;

                if (cellsExploded >= cellsToBeExploded)
                {
                    cellsExploded = 0;
                    return;
                }
            }
        }
    }

    private async UniTask WaitForSeconds(float seconds)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(seconds));
    }
}
