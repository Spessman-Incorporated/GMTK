using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceExplosionSystem : MonoBehaviour
{
    public PhysicalDice PhysicalDice;
    public List<GameObject> DownExplosions;
    public List<GameObject> TopExplosions;
    public List<GameObject> LeftExplosions;
    public List<GameObject> RightExplosions;

    private void OnEnable()
    {
        PhysicalDice.OnDiceRollFinish += ExplodeTiles;
    }

    private void OnDisable()
    {
        PhysicalDice.OnDiceRollFinish -= ExplodeTiles;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ExplodeTiles();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTiles();
        }
    }

    public void ExplodeTiles()
    {
        int DiceResult = DiceResultSystem.DiceResult;

        for (int i = 0; i < DiceResult; i++)
        {
            DownExplosions[i].GetComponent<Explode>().ExplodeTile();
        }
        
        for (int i = 0; i < DiceResult; i++)
        {
            TopExplosions[i].GetComponent<Explode>().ExplodeTile();
        }
        
        for (int i = 0; i < DiceResult; i++)
        {
            LeftExplosions[i].GetComponent<Explode>().ExplodeTile();
        }
        
        for (int i = 0; i < DiceResult; i++)
        {
            RightExplosions[i].GetComponent<Explode>().ExplodeTile();
        }
    }

    public void ResetTiles()
    {
        int DiceResult = DiceResultSystem.DiceResult;
        
        for (int i = 0; i < DiceResult; i++)
        {
            DownExplosions[i].GetComponent<Explode>().ResetTile();
        }
        
        for (int i = 0; i < DiceResult; i++)
        {
            TopExplosions[i].GetComponent<Explode>().ResetTile();
        }
        
        for (int i = 0; i < DiceResult; i++)
        {
            LeftExplosions[i].GetComponent<Explode>().ResetTile();
        }
        
        for (int i = 0; i < DiceResult; i++)
        {
            RightExplosions[i].GetComponent<Explode>().ResetTile();
        }
    }
}
