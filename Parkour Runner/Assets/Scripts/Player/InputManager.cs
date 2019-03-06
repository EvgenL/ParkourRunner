using ParkourRunner.Scripts.Player.InvectorMods;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace ParkourRunner.Scripts.Player
{
    public enum ControlsMode
    {
        HalfScreenButtonsAndSwipe,
        FourButtons,
        TiltAndSwipe
    }

    public class InputManager : MonoBehaviour
    {
        [SerializeField] private GameObject _screenButtonsContaner;
        [SerializeField] private GameObject _fourButtonsContaner;
        [SerializeField] private GameObject _tiltContaner;

        [SerializeField] private ParkourThirdPersonInput _playerInput;

        public Dropdown DebugDropdown;

        [SerializeField] private ControlsMode _controlsMode;
        [SerializeField] private Vector2 _startTouch;
        [SerializeField] private Vector2 _swipeDelta;
        [SerializeField] private bool _hold;

        [SerializeField] private int _circleRadius = 50;

        #region Singleton

        public static InputManager Instance;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        #endregion

        private void Start()
        {
            SwitchMode(Configuration.Instance.GetInputConfiguration());

            if (_playerInput == null)
            {
                _playerInput = FindObjectOfType<ParkourThirdPersonInput>();
            }
        }

        public void OnSwitchModeDebug()
        {
            SwitchMode((ControlsMode)DebugDropdown.value);
        }

        public void SwitchMode(ControlsMode mode)
        {
            _controlsMode = mode;
            switch (_controlsMode)
            {
                case ControlsMode.HalfScreenButtonsAndSwipe:
                    _screenButtonsContaner.SetActive(true);
                    _fourButtonsContaner.SetActive(false);
                    _tiltContaner.SetActive(false);
                    break;

                case ControlsMode.FourButtons:
                    _screenButtonsContaner.SetActive(false);
                    _fourButtonsContaner.SetActive(true);
                    _tiltContaner.SetActive(false);
                    break;

                case ControlsMode.TiltAndSwipe:
                    _screenButtonsContaner.SetActive(false);
                    _fourButtonsContaner.SetActive(false);
                    _tiltContaner.SetActive(true);
                    break;
            }
        }
        
        void Update()
        {
            switch (_controlsMode)
            {
                case ControlsMode.HalfScreenButtonsAndSwipe:
                    MouseSwipesInput();
                    MobileSwipesInput();
                    CalculateDelta();
                    CheckCircle();
                    break;

                case ControlsMode.FourButtons:
                    break;

                case ControlsMode.TiltAndSwipe:
                    //TODO постоянный угол свайпа относительно поворота экрана
                    //MobileTiltInput();
                    MouseSwipesInput();
                    MobileSwipesInput();
                    CalculateDelta();
                    CheckCircle();
                    break;
            }
        }


        private void MouseSwipesInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _hold = true;
                _startTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ResetPositions();
            }
        }

        private void MobileSwipesInput()
        {
            if (Input.touchCount > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    _hold = true;
                    _startTouch = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    ResetPositions();
                }
            }
        }

        private void CalculateDelta()
        {
            _swipeDelta = Vector2.zero;
            if (_hold)
            {
                if (Input.touchCount > 0) //mob
                {
                    _swipeDelta = Input.touches[0].position - _startTouch;
                }
                else if (Input.GetMouseButton(0)) //pc
                {
                    _swipeDelta = (Vector2) Input.mousePosition - _startTouch;
                }
            }
        }

        private void CheckCircle()
        {
            if (_swipeDelta.magnitude > _circleRadius)
            {
                float x = _swipeDelta.x;
                float y = _swipeDelta.y;

                if (Mathf.Abs(x) < Mathf.Abs(y)) //Up or Down
                {
                    if (y < 0) //Down
                    {
                        Roll();
                    }
                    else //Up
                    {
                        Jump();
                    }
                }
                /* else //Left or right
            {
                if (x < 0) //Left
                {
                    SwipeLeft();
                }
                else //right
                {
                    SwipeRight();
                }
            }*/

                ResetPositions();
            }
        }
    
        private void ResetPositions()
        {
            _hold = false;
            _swipeDelta = _startTouch = Vector2.zero;
        }

        public void Jump()
        {
            _playerInput.Jump();
            /*CrossPlatformInputManager.SetButtonDown("Jump");
        CrossPlatformInputManager.SetButtonUp("Jump");*/
        }

        public void Roll()
        {
            _playerInput.Roll();
            /*CrossPlatformInputManager.SetButtonDown("Roll");
        CrossPlatformInputManager.SetButtonUp("Roll");*/
        }

        public void Left()
        {
            CrossPlatformInputManager.SetAxis("Horizontal", -1);
            print("Left");
        }

        public void Right()
        {
            CrossPlatformInputManager.SetAxis("Horizontal", 1);
        }

        public void DontTurn()
        {
            CrossPlatformInputManager.SetAxis("Horizontal", 0);
        }
    }
}