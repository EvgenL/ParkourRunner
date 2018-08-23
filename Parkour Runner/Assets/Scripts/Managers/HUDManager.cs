using Assets.Scripts.Pick_Ups.Effects;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    public static HUDManager Instance;

    #region Singleton

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

#endregion

    public Image flashImage;
    public float FlashSpeed = 5f;

    public Text CoinsText;

    public BonusPanel BonusPanel;

    private bool _flashing = false;

    void Start()
    {
        SetCoins(0);
    }

    void Update()
    {
        ShowFlash();
    }

    public void Flash()
    {
        _flashing = true;
        flashImage.enabled = true;
    }

    private void ShowFlash()
    {
        if (!flashImage.enabled) return;

        if (_flashing)
        {
            _flashing = false;
            if (flashImage != null)
                flashImage.color = Color.white; //Ставим непрозрачный цвет
        }
        else if (flashImage != null)
            flashImage.color = Color.Lerp(flashImage.color, Color.clear, FlashSpeed * Time.deltaTime);

        if (flashImage.color == Color.clear)
        {
            flashImage.enabled = false;
        }
    }

    public void SetCoins(int value)
    {
        if (value == 0)
            CoinsText.text = "";
        else
            CoinsText.text = value + "$";
    }

    public void UpdateBonus(BonusName bonusName, float value)
    {
        BonusPanel.UpdateBonus(bonusName, value);
    }

    public void DisableBonus(BonusName bonusName)
    {
        BonusPanel.DisableBonus(bonusName);
    }
}
