using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Monkey : MonoBehaviour
{
    SpriteRenderer _sr;

    // Start is called before the first frame update
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        print(AssetsManager.monkeyNoHandSprite);
        _sr.sprite = AssetsManager.monkeyNoHandSprite;

        EventsManager.Instance.AddListener<OnTap>(UpdatePose);
    }

    void UpdatePose(OnTap e)
    {
        if (e.isRight) _sr.sprite = AssetsManager.monkeyRightHandSprite;
        else _sr.sprite = AssetsManager.monkeyLeftHandSprite;
    }

    private void OnDestroy()
    {
        EventsManager.Instance.RemoveListener<OnTap>(UpdatePose);
    }
}
