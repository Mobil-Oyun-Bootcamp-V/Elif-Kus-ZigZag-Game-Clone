using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundSpawner : MonoBehaviour
{
    public GameObject lastGround; //son zemini tanitiyorum.
    public GameObject pickUp;

    void Start()
    {
        for (int i = 0; i < 17; i++)
        {
            GroundCreate();
        }
    }


    void Update()
    {
    }

    public void GroundCreate()
    {
        Vector3 dir; //yon ya forward ya left olacak

        int spawnPickup = Random.Range(0, 10);

        if (Random.Range(0, 2) == 0) //0-1 arasinda
        {
            dir = Vector3.left;
        }
        else
        {
            dir = Vector3.forward;
        }

        if (spawnPickup == 0)
        {
            lastGround.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            lastGround.transform.GetChild(0).gameObject.SetActive(false);
        }
        //spawnlamak icin Instantiate kullaniriz
        lastGround = Instantiate(lastGround, lastGround.transform.position + dir,
            lastGround.transform.rotation);
    }
}