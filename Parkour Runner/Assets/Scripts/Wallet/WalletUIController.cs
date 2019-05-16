using UnityEngine.UI;
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
        if (coins > 0)
        {
            int n = coins;
            string txt = string.Empty;

            int rank = 0;
            while (n >= 10)
            {
                int last = n % 10;
                rank++;

                txt = (rank > 3) ? last.ToString() + " " + txt : last.ToString() + txt;
                rank = (rank > 3) ? 0 : rank;

                n = n / 10;
            }

            if (n > 0)
            {
                rank++;
                txt = (rank > 3) ? n.ToString() + " " + txt : n.ToString() + txt;
            }

            txt = _textBefore + txt;

            _coinsText.text = _formatPrice ? txt : _textBefore + coins.ToString();
        }
        else
            _coinsText.text = "0";
            
        //if (coins == 0)
        //    _coinsText.text = "0";
        //else
        //    _coinsText.text = _formatPrice ? string.Format("{0}{1:### ### ### ### ###.#}", _textBefore, coins) : _textBefore + coins.ToString();
    }

    #region Events
    private void OnChangeCoins(int coins)
    {
        SetCoins(coins);
    }
    #endregion
}
