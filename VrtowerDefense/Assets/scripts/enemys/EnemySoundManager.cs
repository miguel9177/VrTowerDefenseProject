using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    [SerializeField]
    AudioClip[] audios;
    int currentAudio = 0;
    AudioSource thisAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        //get the current audio source
        thisAudioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the audio is playing
        if(!thisAudioSource.isPlaying)
        {
            //if the current audio is not the same as the lengths of the audop
            if(currentAudio<audios.Length)
            {
                //change the audio clip to the next audio, and then play
                thisAudioSource.clip = audios[currentAudio];
                thisAudioSource.Play();
            }
            //if we were at the last audio
            else
            {
                //change the current audio to 0, to restart the loop
                currentAudio = 0;
                //change the audio clip to the next audio
                thisAudioSource.clip = audios[currentAudio];
                thisAudioSource.Play();
            }
            currentAudio += 1;
          
        }
        
    }
}
