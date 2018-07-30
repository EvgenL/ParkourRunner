using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    public static HUDController Instance;

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

    private bool _flashing = false;

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
                flashImage.color = Color.white;
        }
        else if (flashImage != null)
            flashImage.color = Color.Lerp(flashImage.color, Color.clear, FlashSpeed * Time.deltaTime);

        if (flashImage.color == Color.clear)
        {
            flashImage.enabled = false;
        }
    }
}
