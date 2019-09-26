using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Color _alpha = _text.color;
        _alpha.a = 0.3f + 0.7f * Mathf.Sin(Time.frameCount * 0.05f);
        _text.color = _alpha;
    }
}
