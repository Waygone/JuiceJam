using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager GM;
    public AudioSource audioSource;
    public AudioClip audioClip;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D light1;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D light2;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D light3;
    private bool clipPlayed = false;

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
            light1.intensity = 2f;
            light2.intensity = 2f;
            light3.intensity = 2f;
            if (clipPlayed == false)
            {
                audioSource.PlayOneShot(audioClip);
                clipPlayed = true;
            }
        }


    }
}
