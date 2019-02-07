using UnityEngine.UI;
using UnityEngine;

public class WalletUIController : MonoBehaviour
{
    [SerializeField] private Wallet.WalletMode _walletMode;
    [SerializeField] private Text _coinsText;

    private Wallet _wallet;

    private void Awake()
    {
        _wallet = Wallet.Instance;
    }
        
    private void OnEnable()
    {
        _coinsText.text = _walletMode == Wallet.WalletMode.Global ? _wallet.AllCoins.ToString() : _wallet.InGameCoins.ToString();

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

    #region Events
    private void OnChangeCoins(int coins)
    {
        _coinsText.text = coins.ToString();
    }
    #endregion
}
