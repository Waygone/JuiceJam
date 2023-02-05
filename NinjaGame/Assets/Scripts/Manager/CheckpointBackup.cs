using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CheckpointBackup : MonoBehaviour
{
    /*private GameManager GM;
    private AudioSource chekpointSound;
    [SerializeField] private Light2D light1;
    [SerializeField] private Light2D light2;
    [SerializeField] private Light2D light3;
    private void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        chekpointSound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        print(col);
        if (col.tag == "Player")
        {
            print("Set checkpoint");
            GM.SetCheckpoint(transform);
            light1.intensity = 2f;
            light2.intensity = 2f;
            light3.intensity = 2f;
            chekpointSound.PlayOneShot(chekpointSound.clip);
        }
    }*/
}
