using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public Enums.TYPE_NOTE type = Enums.TYPE_NOTE.ALL;
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
                _sr.color = Color.green;
                return;

            case Enums.TYPE_NOTE.RIGHT:
                _sr.color = Color.blue;
                return;

            case Enums.TYPE_NOTE.LEFT:
                _sr.color = Color.red;
                return;

            case Enums.TYPE_NOTE.NONE:
                _sr.color = Color.grey;
                return;

        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * _speed);
    }
}
