using UnityEngine;
using System;
using AEngine;

public class Wallet : MonoSingleton<Wallet>
{
    private const string ALL_COINS_KEY = "Coins";

    public event Action<int> OnChangeInGameCoins;
    public event Action<int> OnChangePlayerCoins;

    public enum WalletMode
    {
        Global,
        InGame
    }

    public int AllCoins { get; private set; }

    public int InGameCoins { get; private set; }
    
    protected override void Init()
    {
        base.Init();

        CheckKeys(ALL_COINS_KEY);

        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        CheckKeys(ALL_COINS_KEY);
        Load();
    }

    private void OnDisable()
    {
        CheckKeys(ALL_COINS_KEY);
        Save();
    }

    #region Interface
    public void AddCoins(int value, WalletMode mode)
    {
        this.AllCoins += value;
        OnChangePlayerCoins.SafeInvoke(this.AllCoins);

        if (mode == WalletMode.InGame)
        {
            this.InGameCoins += value;
            OnChangeInGameCoins.SafeInvoke(this.InGameCoins);
        }
    }

    public void ResetInGameCoins()
    {
        this.InGameCoins = 0;
        OnChangeInGameCoins.SafeInvoke(this.InGameCoins);
    }

    public bool SpendCoins(int value)
    {
        if (this.AllCoins - value >= 0 && value >= 0)
        {
            this.AllCoins -= value;
            this.InGameCoins = Mathf.Clamp(this.InGameCoins - value, 0, this.AllCoins);

            OnChangePlayerCoins.SafeInvoke(this.AllCoins);
            OnChangeInGameCoins.SafeInvoke(this.InGameCoins);

            return true;
        }

        return false;
    }

    public void CleanAll()
    {
        this.AllCoins = 0;
        this.InGameCoins = 0;

        OnChangePlayerCoins.SafeInvoke(this.AllCoins);
        OnChangeInGameCoins.SafeInvoke(this.InGameCoins);
    }
    #endregion

    private void CheckKeys(string key)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, 0);
            PlayerPrefs.Save();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt(ALL_COINS_KEY, this.AllCoins);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        this.AllCoins = PlayerPrefs.GetInt(ALL_COINS_KEY);
        ResetInGameCoins();

        OnChangePlayerCoins.SafeInvoke(this.AllCoins);
    }
}
