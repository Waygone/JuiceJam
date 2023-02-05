using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PitchRandomize : MonoBehaviour

{
   
    public AudioSource source;
    private int pitchChanges = 0;
    

    void Start()
    {
        source = GetComponent<AudioSource>();

    }


    void Update()
    {
        if (source.isPlaying == true && pitchChanges == 0)
        {
            source.pitch = Random.Range((float)0.90, (float)1.12);
            pitchChanges ++;

        }

        if (source.isPlaying == false)
        {
            pitchChanges = 0;
        }

    }
}
