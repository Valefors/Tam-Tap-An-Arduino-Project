using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField] AudioClip _menuClip;
    [SerializeField] AudioClip _gameClip;
    [SerializeField] AudioClip _endClip;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
        EventsManager.Instance.AddListener<OnGameStateChanged>(ChangeMusic);
    }

    void ChangeMusic(OnGameStateChanged e)
    {
        switch (GameManager.instance.state)
        {
            case Enums.GAME_STATE.MENU:
                _audioSource.clip = _menuClip;
                break;

            case Enums.GAME_STATE.SELECTION:
                return;

            case Enums.GAME_STATE.GAME:
                _audioSource.clip = _gameClip;
                break;

            case Enums.GAME_STATE.END:
                _audioSource.clip = _endClip;
                break;
        }

        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
