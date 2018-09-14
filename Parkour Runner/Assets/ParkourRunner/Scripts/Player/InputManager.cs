using System;
using ParkourRunner.Scripts.Player.InvectorMods;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace ParkourRunner.Scripts.Player
{
    public enum ControlsMode
    {
        TwoButtons,
        FourButtons,
        Tilt,
        Slider
    }

    public class InputManager : MonoBehaviour
    {
        public GameObject TwoButtonsContaner;
        public GameObject FourButtonsContaner;
        public GameObject TiltContaner;
        public GameObject SliderContaner;

        [SerializeField] private ParkourThirdPersonInput _playerInput;

        public Dropdown DebugDropdown;

        [SerializeField] private ControlsMode _controlsMode;
        [SerializeField] private Vector2 _startTouch;
        [SerializeField] private Vector2 _swipeDelta;
        [SerializeField] private bool _hold;

        [SerializeField] private int _circleRadius = 50;

        private bool _isUsingSlider;

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
            //TODO сохранить настройку
            SwitchMode(_controlsMode);

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
                case ControlsMode.TwoButtons:
                    TwoButtonsContaner.SetActive(true);
                    FourButtonsContaner.SetActive(false);
                    TiltContaner.SetActive(false);
                    SliderContaner.SetActive(false);
                    break;

                case ControlsMode.FourButtons:
                    TwoButtonsContaner.SetActive(false);
                    FourButtonsContaner.SetActive(true);
                    TiltContaner.SetActive(false);
                    SliderContaner.SetActive(false);
                    break;

                case ControlsMode.Tilt:
                    TwoButtonsContaner.SetActive(false);
                    FourButtonsContaner.SetActive(false);
                    TiltContaner.SetActive(true);
                    SliderContaner.SetActive(false);
                    break;

                case ControlsMode.Slider:
                    TwoButtonsContaner.SetActive(false);
                    FourButtonsContaner.SetActive(false);
                    TiltContaner.SetActive(false);
                    SliderContaner.SetActive(true);
                    break;
            }
        }


        void Update()
        {
            switch (_controlsMode)
            {
                case ControlsMode.TwoButtons:
                    ReadSwipes();
                    break;

                case ControlsMode.FourButtons:
                    break;

                case ControlsMode.Tilt:
                    //TODO постоянный угол свайпа относительно поворота экрана
                    //MobileTiltInput();
                    ReadSwipes();
                    break;

                case ControlsMode.Slider:
                    ReadSliderAndSwipes();
                    break;

    
            }
        }

        private void ReadSliderAndSwipes()
        {
            if (_isUsingSlider)
            {
                ReadSwipes(1); //Если мы держим слайдер пальцем [0], то свайпы будем читать пальцем [1]
            }
            else
            {
                ReadSwipes(0);
            }
        }

        private void ReadSwipes(int touchN = 0)
        {
            MouseSwipesInput();
            MobileSwipesInput(touchN);
            CalculateDelta(touchN);
            CheckCircle();
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

        private void MobileSwipesInput(int touchN = 0)
        {
            if (Input.touchCount > 0)
            {
                if (Input.touches[touchN].phase == TouchPhase.Began)
                {
                    _hold = true;
                    _startTouch = Input.touches[touchN].position;
                }
                else if (Input.touches[touchN].phase == TouchPhase.Ended || Input.touches[touchN].phase == TouchPhase.Canceled)
                {
                    ResetPositions();
                }
            }
        }

        private void CalculateDelta(int touchN = 0)
        {
            _swipeDelta = Vector2.zero;
            if (_hold)
            {
                if (Input.touchCount > 0) //mob
                {
                    _swipeDelta = Input.touches[touchN].position - _startTouch;
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
        }

        public void Right()
        {
            CrossPlatformInputManager.SetAxis("Horizontal", 1);
        }

        public void DontTurn()
        {
            CrossPlatformInputManager.SetAxis("Horizontal", 0);
        }

        public void OnSliderDragBegin()
        {
            _isUsingSlider = true;
        }
        public void OnSliderDragEnd()
        {
            _isUsingSlider = false;
        }

        public void OnSliderValueChanged(float value)
        {
            CrossPlatformInputManager.SetAxis("Horizontal", value);
        }

        public void SliderReset()
        {
            DontTurn();
        }
    }
}