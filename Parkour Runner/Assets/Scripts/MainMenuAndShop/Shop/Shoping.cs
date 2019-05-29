using UnityEngine;

public static class Shoping
{
    public static void GetDonat(DonatShopData data)
    {
        switch (data.DonatKind)
        {
            case DonatShopData.DonatKinds.NoAds:
                PlayerPrefs.SetInt(data.DonatKind.ToString(), 1);
                PlayerPrefs.Save();
                break;

            case DonatShopData.DonatKinds.ByCoins1:
            case DonatShopData.DonatKinds.ByCoins2:
            case DonatShopData.DonatKinds.ByCoins3:
            case DonatShopData.DonatKinds.ByCoins4:
                Wallet.Instance.AddCoins(int.Parse(data.DonatValue), Wallet.WalletMode.Global);
                break;
        }
    }

    public static void GetBonus(string bonusName)
    {
        PlayerPrefs.SetInt(bonusName, PlayerPrefs.GetInt(bonusName) +1);
    }
}