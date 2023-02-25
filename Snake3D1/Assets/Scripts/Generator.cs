using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject prefabPlane;
    public int PlaneCount;

    public GameObject CoinPrefab;
    public int CoinCount;
    public float minDistance;
    public float maxDistance;

    public GameObject CubePrefab;
    public int CubeCount;
    public GameObject Player;

    private GameObject Clone;
    public GameObject Win;

    public GameObject System;
    void Start()
    {
        SpawnPlane();
    }
    public void SpawnPlane()
    {
        for (int i = 0; i < PlaneCount; i++)
        {
            if (Clone == null)
            {
                Clone = Instantiate(prefabPlane);
                SpawnCoin();
                SpawnCube();
            }
            else
            {
                SpawnCoin();
                SpawnCube();
                Clone = Instantiate(prefabPlane, Clone.transform.position + (Clone.transform.localScale.z * Vector3.forward), Quaternion.identity);
            }

            if(i == PlaneCount - 1)
            {
                System.transform.position = Clone.transform.position +  new Vector3(0, Clone.transform.localScale.y / 2, Clone.transform.localScale.z / 2);
            }
           
        }
    }
    public void SpawnCoin()
    {
        Vector3 vector = Clone.transform.position + new Vector3(0, Clone.transform.localScale.y / 2 + CoinPrefab.transform.localScale.y / 2 + 0.5f, -Clone.transform.localScale.z);

        for (int i = 0; i < CoinCount; i++)
        {
            vector += Random.Range(minDistance, maxDistance)  * Vector3.forward;

            if (vector.z > Clone.transform.position.z + Clone.transform.localScale.z / 2)
                break;

            Instantiate(CoinPrefab, vector + Random.Range(vector.x - Clone.transform.localScale.x / 2.5f, vector.x + Clone.transform.localScale.x / 2.5f) * Vector3.right, Quaternion.identity);

            
        }
    }
    public void SpawnCube()
    {
        Vector3 vector = Clone.transform.position - new Vector3(Clone.transform.localScale.x / 2.5f, Clone.transform.localScale.y / 2) + CubePrefab.transform.localScale.y * Vector3.up;

        for (int i = 0; i < CubeCount; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                if(Random.Range(1, 101) < 20)
                {
                    Instantiate(CubePrefab, vector + CubePrefab.transform.localScale.x * j * Vector3.right, Quaternion.identity);
                }
            }
            vector += Random.Range(minDistance, maxDistance) * Vector3.forward;


        }
    }
    private void Update()
    {
        if (Clone != null && Player != null && Clone.transform.position.z  < Player.transform.position.z)
        {
            System.SetActive(true);
        }

        if (Clone != null &&  Player != null && Clone.transform.position.z + Clone.transform.localScale.z / 2 < Player.transform.position.z)
        {
            Time.timeScale = 0;
            Win.SetActive(true);
            Debug.Log("you win!");
        }
    }
}
