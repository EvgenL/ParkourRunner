using UnityEngine;
using AEngine;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private CharacterKinds _kind;
    [SerializeField] private RectTransform _avatar;
    [SerializeField] private RectTransform _avatarPivot;

    public void OnSelectButtonClick()
    {
        PlayerPrefs.SetString(CharactersData.CHARACTER_KEY, _kind.ToString());
        PlayerPrefs.Save();

        AudioManager.Instance.PlaySound(Sounds.Bonus);
    }

    private void Update()
    {
        Vector3 position = _avatar.position;
        position.x = _avatarPivot.position.x;
        _avatar.position = position;
    }
}
