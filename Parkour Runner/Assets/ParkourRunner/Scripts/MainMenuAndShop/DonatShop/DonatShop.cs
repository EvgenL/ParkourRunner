using UnityEngine;
using UnityEngine.UI;
using System;

public class DonatShop : MonoBehaviour
{
    [Serializable]
    private struct ContentConfig
    {
        public float rectHeight;
        public float rectY;
    }

    [SerializeField] private ScrollRect _scroll;
    [SerializeField] private GameObject _noAdsPanel;

    [Header("Content configuration")]
    [SerializeField] private RectTransform _contentRect;
    [SerializeField] private ContentConfig _withAdsConfig;
    [SerializeField] private ContentConfig _noAdsConfig;

    void OnEnable ()
    {
        RefreshDonatPanels();		
	}
	
    public void RefreshDonatPanels()
    {
        bool enableAds = PlayerPrefs.GetInt(DonatShopData.DonatKinds.NoAds.ToString()) == 0;

        _scroll.enabled = enableAds;
        _noAdsPanel.SetActive(enableAds);
                
        _contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, enableAds ? _withAdsConfig.rectHeight : _noAdsConfig.rectHeight);

        var pos = _contentRect.anchoredPosition;
        pos.y = enableAds ? _withAdsConfig.rectY : _noAdsConfig.rectY;
        _contentRect.anchoredPosition = pos;
    }
}
