using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager GM;

    private void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //print(col);
        if (col.tag == "Player")
        {
            //print("Set checkpoint");
            GM.SetCheckpoint(transform);
        }
    }
}
