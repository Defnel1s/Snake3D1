using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public int LifeCount;
    public int minLife;
    public int maxLife;
    public TextMeshPro textMesh;
    public Material[] materials;


    private void Start()
    {
        LifeCount = Random.Range(minLife, maxLife);
        textMesh.text = LifeCount.ToString();

        int Index = LifeCount / (maxLife / (materials.Length - 1));

        GetComponent<MeshRenderer>().material = materials[Index];
    }

}
