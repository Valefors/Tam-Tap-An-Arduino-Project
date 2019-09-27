using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enums
{
    public enum TYPE_NOTE
    {
        RIGHT,
        LEFT,
        ALL,
        NONE
    };

    public enum GAME_STATE
    {
        MENU,
        SELECTION,
        GAME,
        CREATE,
        END
    };

    public enum TYPE_SFX
    {
        TAP_LEFT,
        TAP_RIGHT,
        FAIL,
        MOKEY_NEUTRAL,
        MOKEY_HAPPY
    };
}
