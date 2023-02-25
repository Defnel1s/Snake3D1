using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int LifeCount;
    public int minCoin;
    public int maxCoin;
    public TextMeshPro textMesh;


    private void Start()
    {
        LifeCount = Random.Range(minCoin, maxCoin);
        textMesh.text = LifeCount.ToString();
    }
}
