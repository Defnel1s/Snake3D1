using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float SpeedForward;
    public GameObject Tail;
    public float Distance;
    public List<GameObject> Tails = new List<GameObject>();
    public GameObject loss;

    public TextMeshProUGUI textCoin;
    public GameObject ButtonRestart;

    private void Start()
    {
        Time.timeScale = 1;
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
                Clone = Instantiate(Tail);
                Clone.transform.parent = transform;
                Clone.transform.position = transform.position + new Vector3(0, 0, -Distance);
            }
            else
            {
                Clone = Instantiate(Tail);
                Clone.transform.parent = transform;
                Clone.transform.position = Tails[Tails.Count - 1].transform.position + new Vector3(0, 0, -Distance);
            }


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
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Coint")
        {
            Destroy(other.gameObject);
            AddTail(other.gameObject.GetComponent<Coin>().LifeCount);
        }
        else if (other.tag == "Cube")
        {
            int Count = other.gameObject.GetComponent<Cube>().LifeCount;

            if(Tails.Count >= Count )
            {
                RemoveTail(Count);
                Destroy(other.gameObject);
            }
            else
            {
                loss.SetActive(true);
                ButtonRestart.SetActive(false);
                Time.timeScale = 0;
                Debug.Log("you loss");
            }
            Debug.Log(Count);
        }
        textCoin.text = "Life: "  + Tails.Count;
    }
}
