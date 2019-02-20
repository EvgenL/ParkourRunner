﻿using UnityEngine.UI;
using UnityEngine;

public class WalletUIController : MonoBehaviour
{
    [SerializeField] private Wallet.WalletMode _walletMode;
    [SerializeField] private Text _coinsText;
    [SerializeField] private string _textBefore;
    [SerializeField] private bool _formatPrice;

    private Wallet _wallet;

    private void Awake()
    {
        _wallet = Wallet.Instance;
    }
        
    private void OnEnable()
    {
        SetCoins(_walletMode == Wallet.WalletMode.Global ? _wallet.AllCoins : _wallet.InGameCoins);

        if (_walletMode == Wallet.WalletMode.Global)
        {
            _wallet.OnChangePlayerCoins += OnChangeCoins;
        }
        else
        {
            _wallet.OnChangeInGameCoins += OnChangeCoins;
        }
    }

    private void OnDisable()
    {
        if (_walletMode == Wallet.WalletMode.Global)
        {
            _wallet.OnChangePlayerCoins -= OnChangeCoins;
        }
        else
        {
            _wallet.OnChangeInGameCoins -= OnChangeCoins;
        }
    }
        
    private void SetCoins(int coins)
    {
        _coinsText.text = _formatPrice ? string.Format("{0}{1:### ### ### ### ###.#}", _textBefore, coins) : _textBefore + coins.ToString();
    }

    #region Events
    private void OnChangeCoins(int coins)
    {
        SetCoins(coins);
    }
    #endregion
}
