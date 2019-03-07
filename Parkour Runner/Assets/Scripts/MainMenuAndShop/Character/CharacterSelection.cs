using UnityEngine;
using AEngine;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private CharacterKinds _kind;

    public void OnSelectButtonClick()
    {
        PlayerPrefs.SetString("Character", _kind.ToString());
        PlayerPrefs.Save();

        AudioManager.Instance.PlaySound(Sounds.Bonus);
    }
}
