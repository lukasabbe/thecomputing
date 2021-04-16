using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    bool mute = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown("m")) MuteMusic();
    }
    void MuteMusic()
    {
        mute = !mute;

        transform.gameObject.GetComponent<AudioSource>().mute = mute;
    }
}
