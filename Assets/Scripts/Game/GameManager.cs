using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Drum drum = null;
    public Enums.GAME_STATE state = Enums.GAME_STATE.MENU;
    public GameObject prefabStarsParticles;
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
        EventsManager.Instance.AddListener<OnEndGame>(OnEndGame);

        if (drum == null) Debug.LogError("NO DRUM INSERT");
    }
    #endregion

    void OnEndGame(OnEndGame e)
    {
        state = Enums.GAME_STATE.END;
        EventsManager.Instance.Raise(new OnGameStateChanged());
    }

    void OnTapReceive(OnTap e)
    {
        bool lRight = e.isRight;

        switch (state)
        {
            case Enums.GAME_STATE.MENU:
                state = Enums.GAME_STATE.SELECTION;
                EventsManager.Instance.Raise(new OnGameStateChanged());
                return;

            case Enums.GAME_STATE.SELECTION:

                if (lRight) print("hard mode");
                else print("ez mode");

                state = Enums.GAME_STATE.GAME;
                EventsManager.Instance.Raise(new OnGameStateChanged());
                return;

            case Enums.GAME_STATE.GAME:
                CheckCorrectTap(lRight);
                return;

            case Enums.GAME_STATE.END:
                if (lRight)
                {
                    state = Enums.GAME_STATE.MENU;
                }

                else
                {
                    state = Enums.GAME_STATE.GAME;
                }

                EventsManager.Instance.Raise(new OnGameStateChanged());
                return;

        }
    }

    void CheckCorrectTap(bool pIsRight)
    {
        switch (drum.drumState)
        {
            case Enums.TYPE_NOTE.RIGHT:
                if (pIsRight)
                {
                    print("CORRECT RIGHT TAP");
                    Instantiate(prefabStarsParticles,new Vector3( drum.transform.position.x, drum.transform.position.y, 5), Quaternion.identity);
                }
                else print("FAILED");

                drum.DestroyNote();
                break;

            case Enums.TYPE_NOTE.LEFT:
                if (!pIsRight)
                {
                    print("CORRECT LEFT TAP");
                    Instantiate(prefabStarsParticles, drum.transform.position, Quaternion.identity);
                }
                else print("FAILED");

                drum.DestroyNote();
                break;

            case Enums.TYPE_NOTE.ALL:
                Instantiate(prefabStarsParticles, drum.transform.position, Quaternion.identity);
                print("CORRECT TAP");
                break;

            case Enums.TYPE_NOTE.NONE:
                print("FAILED");
                break;
        }

    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
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
        }*/
        if(Input.GetKeyDown(KeyCode.LeftArrow)) EventsManager.Instance.Raise(new OnTap(false));
        if(Input.GetKeyDown(KeyCode.RightArrow)) EventsManager.Instance.Raise(new OnTap(true));
    }

    void OnDestroy()
    {
        EventsManager.Instance.RemoveListener<OnTap>(OnTapReceive);
    }
}
