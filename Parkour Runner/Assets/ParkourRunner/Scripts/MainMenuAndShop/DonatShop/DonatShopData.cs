using UnityEngine;

[CreateAssetMenu(fileName = "New DonatShopData", menuName = "DonatShopData", order = 52)]
public class DonatShopData : ScriptableObject
{
    public enum DonatKinds
    {
        NoAds,
        ByCoins1,
        ByCoins2
    }

    [SerializeField] private DonatKinds _kind;
    [SerializeField] private string _donatValue;

    public DonatKinds DonatKind { get { return _kind; } }

    public string DonatValue { get { return _donatValue; } }
}
