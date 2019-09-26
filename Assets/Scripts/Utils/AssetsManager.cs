using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AssetsManager
{
    public static Sprite monkeyRightHandDownSprite = Resources.Load("Monkey/Poses/right_hand_down", typeof(Sprite)) as Sprite;
    public static Sprite monkeyRightHandUpSprite = Resources.Load("Monkey/Poses/right_hand_up", typeof(Sprite)) as Sprite;
    public static Sprite monkeyLeftHandDownSprite = Resources.Load("Monkey/Poses/left_hand_down", typeof(Sprite)) as Sprite;
    public static Sprite monkeyLeftHandUpSprite = Resources.Load("Monkey/Poses/left_hand_up", typeof(Sprite)) as Sprite;
}
