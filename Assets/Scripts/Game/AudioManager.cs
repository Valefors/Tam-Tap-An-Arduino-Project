using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource _audioMusicSource;
    [SerializeField] AudioSource _audioSFXSource;

    [SerializeField] AudioClip _menuClip;
    [SerializeField] AudioClip _gameClip;
    [SerializeField] AudioClip _endClip;

    [SerializeField] AudioClip _failSFX;
    [SerializeField] AudioClip _fail2SFX;
    [SerializeField] AudioClip _rightTapSFX;
    [SerializeField] AudioClip _leftTapSFX;
    [SerializeField] AudioClip _neutralMonkeySFX;
    [SerializeField] AudioClip _happyMonkeySFX;

    // Start is called before the first frame update
    void Start()
    {
        _audioMusicSource.Play();
        EventsManager.Instance.AddListener<OnGameStateChanged>(ChangeMusic);
        EventsManager.Instance.AddListener<OnSFXPlay>(PlaySFX);
    }

    void ChangeMusic(OnGameStateChanged e)
    {
        switch (GameManager.instance.state)
        {
            case Enums.GAME_STATE.MENU:
                _audioMusicSource.clip = _menuClip;
                break;

            case Enums.GAME_STATE.SELECTION:
                return;

            case Enums.GAME_STATE.GAME:
                _audioMusicSource.clip = _gameClip;
                break;

            case Enums.GAME_STATE.END:
                _audioMusicSource.clip = _endClip;
                break;
        }

        _audioMusicSource.Play();
    }

    void PlaySFX(OnSFXPlay e)
    {
        switch (e.sfxType)
        {
            case Enums.TYPE_SFX.FAIL:
                float rand = Random.Range(0, 3);
                if (rand > 1f)
                {
                    _audioSFXSource.clip = _failSFX;
                }
                else
                {
                    _audioSFXSource.clip = _fail2SFX;
                }
                break;

            case Enums.TYPE_SFX.TAP_LEFT:
                _audioSFXSource.clip = _leftTapSFX;
                break;

            case Enums.TYPE_SFX.TAP_RIGHT:
                _audioSFXSource.clip = _rightTapSFX;
                break;

            case Enums.TYPE_SFX.MOKEY_NEUTRAL:
                _audioSFXSource.clip = _neutralMonkeySFX;
                break;

            case Enums.TYPE_SFX.MOKEY_HAPPY:
                _audioSFXSource.clip = _happyMonkeySFX;
                break;
        }

        _audioSFXSource.Play();
    }

    private void OnDestroy()
    {
        EventsManager.Instance.RemoveListener<OnGameStateChanged>(ChangeMusic);
        EventsManager.Instance.RemoveListener<OnSFXPlay>(PlaySFX);
    }
}
