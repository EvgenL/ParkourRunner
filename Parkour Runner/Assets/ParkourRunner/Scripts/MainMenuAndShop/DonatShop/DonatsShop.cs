using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DonatsShop : MonoBehaviour
{
    [SerializeField] private List<DonatShopData> _allDonats = new List<DonatShopData>();
    [SerializeField] private GameObject _donatsPanel;
    [SerializeField] private GameObject _donatsPlace;

    private void Start()
    {
        InstantDonatsPanels();
    }

    private void InstantDonatsPanels()
    {
        for (int i = 0; i < _allDonats.Count; i++)
        {
            GameObject temporalPanel = Instantiate(_donatsPanel, _donatsPlace.transform);
            temporalPanel.GetComponent<ShopDonatsPanel>().MyImage.sprite = _allDonats[i].DonatsIcon;
            temporalPanel.name = _allDonats[i].DonatsName;
            temporalPanel.GetComponentInChildren<Button>().onClick.AddListener(() => temporalPanel.GetComponent<ShopDonatsPanel>().BuyThisThing());
        }
    }
}
