using UnityEngine;

namespace ParkourRunner.Scripts.Player
{
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
}
