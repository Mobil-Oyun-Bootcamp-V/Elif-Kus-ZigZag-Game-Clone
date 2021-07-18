using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform ballPlace;
    private Vector3 diff;
    void Start()
    {
        //kendi pozisyonumuz-topun konumu = fark
        diff = transform.position - ballPlace.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (BallMovement.isFall==false)
        {
            transform.position = ballPlace.position + diff;
        }
        
    }
}
