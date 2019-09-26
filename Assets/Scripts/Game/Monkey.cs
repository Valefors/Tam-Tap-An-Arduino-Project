using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Monkey : MonoBehaviour
{
    //SpriteRenderer _sr;

    [SerializeField] SpriteRenderer _leftHand;
    [SerializeField] SpriteRenderer _rightHand;
    [SerializeField] SpriteRenderer _expression;

    float _rightHandTimer = 0;
    float _leftHandTimer = 0;
    Enums.TYPE_NOTE _currentState = Enums.TYPE_NOTE.NONE;

    const float TIMER_LEFT = 0.15f;
    const float TIMER_RIGHT = 0.15f;

    const int LAYER_BACK = 3;
    const int LAYER_FRONT = 5;

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.Instance.AddListener<OnTap>(UpdatePose);
    }

    void UpdatePose(OnTap e)
    {
        if (GameManager.instance.state != Enums.GAME_STATE.GAME) return;

        if (e.isRight)
        {
            SetHandMode(Enums.TYPE_NOTE.RIGHT);
            _rightHandTimer = TIMER_RIGHT;
        }

        else
        {
            SetHandMode(Enums.TYPE_NOTE.LEFT);
            _leftHandTimer = TIMER_LEFT;
        }
    }

    private void Update()
    {
        if (_leftHandTimer < 0) _leftHandTimer = 0;
        if (_rightHandTimer < 0) _rightHandTimer = 0;

        if (_leftHandTimer > 0) _leftHandTimer -= Time.deltaTime;
        if (_rightHandTimer > 0) _rightHandTimer -= Time.deltaTime;

        if (_leftHandTimer <= 0 && _rightHandTimer <= 0 && _currentState != Enums.TYPE_NOTE.NONE)
        {
            SetHandMode(Enums.TYPE_NOTE.NONE);

            _leftHandTimer = 0;
            _rightHandTimer = 0;
        }

    }

    void SetHandMode(Enums.TYPE_NOTE pWantedState)
    {
        _currentState = pWantedState;

        switch (_currentState)
        {
            case Enums.TYPE_NOTE.RIGHT:
                _rightHand.sprite = AssetsManager.monkeyRightHandDownSprite;
                _rightHand.sortingOrder = LAYER_FRONT;

                _leftHand.sprite = AssetsManager.monkeyLeftHandUpSprite;
                _leftHand.sortingOrder = LAYER_BACK;
                break;

            case Enums.TYPE_NOTE.NONE:
                _rightHand.sprite = AssetsManager.monkeyRightHandUpSprite;
                _rightHand.sortingOrder = LAYER_BACK;

                _leftHand.sprite = AssetsManager.monkeyLeftHandUpSprite;
                _leftHand.sortingOrder = LAYER_BACK;
                break;

            case Enums.TYPE_NOTE.LEFT:
                _leftHand.sprite = AssetsManager.monkeyLeftHandDownSprite;
                _leftHand.sortingOrder = LAYER_FRONT;

                _rightHand.sprite = AssetsManager.monkeyRightHandUpSprite;
                _rightHand.sortingOrder = LAYER_BACK;
                break;
        }
    }

    private void OnDestroy()
    {
        EventsManager.Instance.RemoveListener<OnTap>(UpdatePose);
    }
}
