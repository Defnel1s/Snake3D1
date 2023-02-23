using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float SpeedForward;
    public GameObject Tail;
    public float Distance;
    public List<GameObject> Tails = new List<GameObject>();
    void Start()
    {
        AddTail(5);
        RemoveTail(2);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * Speed * Time.deltaTime; // 0.002
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Speed * Time.deltaTime;
        }
        transform.position += Vector3.forward * SpeedForward * Time.deltaTime;
    }
    public void AddTail(int tailCount)
    {
        for (int i = 0; i < tailCount; i++)
        {
            GameObject Clone;

            if (Tails.Count == 0)
            {
                Clone = Instantiate(Tail, transform.position + new Vector3(0, 0, -Distance), Quaternion.identity);
            }
            else
            {
                Clone = Instantiate(Tail, Tails[Tails.Count - 1].transform.position + new Vector3(0, 0, -Distance), Quaternion.identity);
            }

            Clone.transform.parent = transform;

            Tails.Add(Clone);
        } 
    }
    public void RemoveTail(int tailCount)
    {
        for (int i = 0; i < tailCount; i++)
        {
            if (Tails.Count == 0) 
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(Tails[Tails.Count - 1]);
                Tails.RemoveAt(Tails.Count - 1);
            }
        }
    }

}
