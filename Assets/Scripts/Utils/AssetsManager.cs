using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AssetsManager
{
    public static Sprite monkeyNoHandSprite = Resources.Load("Monkey/Poses/bongo-cat-no-hand", typeof(Sprite)) as Sprite;
    public static Sprite monkeyRightHandSprite = Resources.Load("Monkey/Poses/bongo-cat-right-hand", typeof(Sprite)) as Sprite;
    public static Sprite monkeyLeftHandSprite = Resources.Load("Monkey/Poses/bongo-cat-left-hand", typeof(Sprite)) as Sprite;
    public static Sprite monkeyBothHandsSprite = Resources.Load("Monkey/Poses/bongo-cat-both-hands", typeof(Sprite)) as Sprite;
}
