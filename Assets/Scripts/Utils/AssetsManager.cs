using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AssetsManager
{
    public static Sprite monkeyRightHandDownSprite = Resources.Load("Monkey/Poses/right_hand_down", typeof(Sprite)) as Sprite;
    public static Sprite monkeyRightHandUpSprite = Resources.Load("Monkey/Poses/right_hand_up", typeof(Sprite)) as Sprite;
    public static Sprite monkeyLeftHandDownSprite = Resources.Load("Monkey/Poses/left_hand_down", typeof(Sprite)) as Sprite;
    public static Sprite monkeyLeftHandUpSprite = Resources.Load("Monkey/Poses/left_hand_up", typeof(Sprite)) as Sprite;

    public static Sprite rightNote = Resources.Load("Notes/blue_note", typeof(Sprite)) as Sprite;
    public static Sprite leftNote = Resources.Load("Notes/red_note", typeof(Sprite)) as Sprite;

    public static Sprite bambooHard = Resources.Load("Decors/bambou", typeof(Sprite)) as Sprite;
    public static Sprite bambooEasy = Resources.Load("Decors/bambou_2", typeof(Sprite)) as Sprite;

    public static Sprite monkeyNeutralFace = Resources.Load("Monkey/Expressions/monkey_neutral", typeof(Sprite)) as Sprite;
    public static Sprite monkeyHappyFace = Resources.Load("Monkey/Expressions/monkey_happy", typeof(Sprite)) as Sprite;
    public static Sprite monkeyVeryHappyFace = Resources.Load("Monkey/Expressions/monkey_very_happy", typeof(Sprite)) as Sprite;
}
