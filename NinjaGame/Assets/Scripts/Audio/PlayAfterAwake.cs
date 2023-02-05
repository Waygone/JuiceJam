using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAfterAwake : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayDelayed(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
