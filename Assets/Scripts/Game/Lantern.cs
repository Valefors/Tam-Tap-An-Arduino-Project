using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    int MIN_SPEED = 1;
    int MAX_SPEED = 5;

    int MIN_ANGLE = 2;
    int MAX_ANGLE = 5;

    int _angle = 20;
    int _speed = 10;

    Quaternion _originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        _originalRotation = transform.rotation;

        _angle = Random.Range(MIN_ANGLE, MAX_ANGLE);
        _speed = Random.Range(MIN_SPEED, MAX_SPEED);
    }

    // Update is called once per frame
    void Update()
    {
        if (Quaternion.Angle(_originalRotation, transform.rotation) >= _angle) _speed *= -1;

        transform.Rotate(Vector3.forward * (_speed * Time.deltaTime));
        transform.Translate(Vector3.right * _speed  * 0.1f * Time.deltaTime);
    }
}
