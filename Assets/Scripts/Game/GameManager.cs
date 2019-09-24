using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Drum drum = null;
    public Enums.GAME_STATE state = Enums.GAME_STATE.MENU;

    #region Singleton
    public static GameManager instance {
        get { return _instance; }
    }

    static GameManager _instance = null;

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null) Destroy(_instance);
        _instance = this;

        EventsManager.Instance.AddListener<OnTap>(OnTapReceive);

        if (drum == null) Debug.LogError("NO DRUM INSERT");
    }
    #endregion

    void OnTapReceive(OnTap e)
    {
        bool lRight = e.isRight;
        CheckCorrectTap(lRight);
    }

    void CheckCorrectTap(bool pIsRight)
    {
        switch (drum.drumState)
        {
            case Enums.TYPE_NOTE.RIGHT:
                if (pIsRight) print("CORRECT RIGHT TAP");
                else print("FAILED");

                drum.DestroyNote();
                break;

            case Enums.TYPE_NOTE.LEFT:
                if (!pIsRight) print("CORRECT LEFT TAP");
                else print("FAILED");

                drum.DestroyNote();
                break;

            case Enums.TYPE_NOTE.ALL:
                print("CORRECT TAP");
                break;

            case Enums.TYPE_NOTE.NONE:
                print("FAILED");
                break;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (state == Enums.GAME_STATE.MENU)
            {
                state = Enums.GAME_STATE.SELECTION;
                print("1");
            }

            else if (state == Enums.GAME_STATE.SELECTION)
            {
                state = Enums.GAME_STATE.GAME;
                print("2");
            }

            else if (state == Enums.GAME_STATE.GAME)
            {
                state = Enums.GAME_STATE.END;
                print("3");
            }

            else if (state == Enums.GAME_STATE.END)
            {
                state = Enums.GAME_STATE.MENU;
                print("4");
            }

            else print("tg");

            EventsManager.Instance.Raise(new OnGameStateChanged());
        }
        //if(Input.GetKeyDown(KeyCode.LeftArrow)) EventsManager.Instance.Raise(new OnTap(false));
        //if(Input.GetKeyDown(KeyCode.RightArrow)) EventsManager.Instance.Raise(new OnTap(true));
    }

    void OnDestroy()
    {
        EventsManager.Instance.RemoveListener<OnTap>(OnTapReceive);
    }
}
