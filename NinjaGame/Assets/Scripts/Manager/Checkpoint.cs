using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager GM;
    public AudioSource audioSource;
    public AudioClip audioClip;
    private bool clipPlayed = false;

    private void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        print(col);
        if (col.tag == "Player")
        {
            print("Set checkpoint");
            GM.SetCheckpoint(transform);
            if (clipPlayed == false)
            {
                audioSource.PlayOneShot(audioClip);
                clipPlayed = true;
            }
        }


    }
}
