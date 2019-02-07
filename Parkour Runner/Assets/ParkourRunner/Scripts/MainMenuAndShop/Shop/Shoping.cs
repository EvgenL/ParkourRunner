using UnityEngine;

public static class Shoping
{
    public static void GetDonat(string donatsName)
    {
        if (donatsName == "NoAds")
        {
            PlayerPrefs.SetInt("NoAds", 1);
        }
        else
        {
            Wallet.Instance.AddCoins(100, Wallet.WalletMode.Global);
        }
    }

    public static void GetBonus(string bonusName)
    {
        PlayerPrefs.SetInt(bonusName, PlayerPrefs.GetInt(bonusName) +1);
        Debug.Log(string.Format("Name = [{0}], level = {1}", bonusName, PlayerPrefs.GetInt(bonusName)));
    }
}
