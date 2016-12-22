using UnityEngine;
using System.Collections;

public class MyMobileInput : MonoBehaviour
{
    public static bool moveRight;
    public static bool moveLeft;
    public static bool jump;
    public static bool interact;
    bool isPressed;

    public void JumpingIsPressed()
    {
        jump = true;
    }

    public void JumpingIsNotPressed()
    {
        jump = false;
    }

    public void InteractingIsPressed()
    {
        interact = true;
    }

    public void InteractingIsNotPressed()
    {
        interact = false;
    }
}
