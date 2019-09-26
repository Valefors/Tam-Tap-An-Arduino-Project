using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] RectTransform menuScreen;
    [SerializeField] RectTransform selectionScreen;
    [SerializeField] RectTransform levelScreen;
    [SerializeField] RectTransform endScreen;

    RectTransform _currentScreen;

	[SerializeField] Text _gameScore;
	[SerializeField] Text _endScore;

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.Instance.AddListener<OnGameStateChanged>(DisplayScreen);
        EventsManager.Instance.AddListener<OnScoreChanged>(DisplayScore);
        _currentScreen = menuScreen;
        _currentScreen.gameObject.SetActive(true);

		_gameScore.text = 0.ToString ();
		_endScore.text = "Ton score : 0";
	}

    void DisplayScreen(OnGameStateChanged e)
    {
        switch (GameManager.instance.state)
        {
            case Enums.GAME_STATE.SELECTION:
                ChangeScreen(selectionScreen);
                return;

            case Enums.GAME_STATE.GAME:
                ChangeScreen(levelScreen);
                return;

            case Enums.GAME_STATE.END:
                ChangeScreen(endScreen);
                return;

            case Enums.GAME_STATE.MENU:
                ChangeScreen(menuScreen);
                return;
        }
    }

    void ChangeScreen(RectTransform pScreen)
    {
        _currentScreen.gameObject.SetActive(false);
        _currentScreen = pScreen;
        _currentScreen.gameObject.SetActive(true);
    }

	void DisplayScore(OnScoreChanged e) {
		_gameScore.text = GameManager.instance.score.ToString ();
		_endScore.text = "Ton score : " + GameManager.instance.score.ToString ();
	}

    private void OnDestroy()
    {
        EventsManager.Instance.RemoveListener<OnGameStateChanged>(DisplayScreen);
        EventsManager.Instance.RemoveListener<OnScoreChanged>(DisplayScore);
    }
}
