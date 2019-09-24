using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] RectTransform menuScreen;
    [SerializeField] RectTransform selectionScreen;
    [SerializeField] RectTransform levelScreen;
    [SerializeField] RectTransform endScreen;

    RectTransform _currentScreen;

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.Instance.AddListener<OnGameStateChanged>(DisplayScreen);
        _currentScreen = menuScreen;
        _currentScreen.gameObject.SetActive(true);
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

    private void OnDestroy()
    {
        EventsManager.Instance.RemoveListener<OnGameStateChanged>(DisplayScreen);
    }
}
