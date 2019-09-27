using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public Enums.TYPE_NOTE type = Enums.TYPE_NOTE.ALL;
    public bool isLastNote = false;
    SpriteRenderer _sr;
    [SerializeField] float _speed = 5;
 
    // Start is called before the first frame update
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        AddColor();
    }

    void AddColor()
    {
        switch (type)
        {
            case Enums.TYPE_NOTE.ALL :
                _sr.color = Color.white;
                return;

            case Enums.TYPE_NOTE.RIGHT:
                _sr.sprite = AssetsManager.rightNote;
                return;

            case Enums.TYPE_NOTE.LEFT:
                _sr.sprite = AssetsManager.leftNote;
                return;

            case Enums.TYPE_NOTE.NONE:
                _sr.color = Color.grey;
                return;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state != Enums.GAME_STATE.GAME && GameManager.instance.state != Enums.GAME_STATE.CREATE) return;
        transform.Translate(Vector3.left * Time.deltaTime * _speed);
    }
}
