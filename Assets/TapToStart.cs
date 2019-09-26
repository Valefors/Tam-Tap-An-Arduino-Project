using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToStart : MonoBehaviour
{
    bool _start = false;

    // Update is called once per frame
    void Update()
    {
        if (_start) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("PlaySong", 1);
        }
    }

    void PlaySong()
    {
        _start = true;
        GetComponent<AudioSource>().Play();
    }
}
