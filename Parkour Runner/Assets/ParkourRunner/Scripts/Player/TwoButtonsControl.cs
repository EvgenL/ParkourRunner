using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoButtonsControl : MonoBehaviour {

    public void OnLeftButtonDown()
    {
        InputManager.Instance.Left();
    }
    public void OnRightButtonDown()
    {
        InputManager.Instance.Right();
    }

    public void OnButtonsUp()
    {
        InputManager.Instance.DontTurn();
    }

}
