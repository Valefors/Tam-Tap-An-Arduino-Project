using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bamboo : MonoBehaviour
{
	SpriteRenderer _sr;
    // Start is called before the first frame update
    void Start()
    {
		_sr = GetComponent<SpriteRenderer> ();
    }

	public void DisplayBamboo(bool hardMode) {
		if (hardMode) {
			_sr.sprite = AssetsManager.bambooHard;
		} else {
			_sr.sprite = AssetsManager.bambooEasy;
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
