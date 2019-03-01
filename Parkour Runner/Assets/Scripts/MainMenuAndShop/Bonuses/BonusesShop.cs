using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusesShop : MonoBehaviour
{
    [SerializeField] private List <BonusesShopData> _allBonuses = new List<BonusesShopData>();
    [SerializeField] private GameObject _bonusesPanel;
    [SerializeField] private GameObject _bonusesPlace;
        
    private void Start()
    {
        InstantBonusesPanels();
    }

    private void InstantBonusesPanels()
    {
        for(int i = 0; i < _allBonuses.Count; i++)
        {
            var temporalPanel = Instantiate(_bonusesPanel, _bonusesPlace.transform).GetComponent<ShopBonusesPanel>();

            temporalPanel.MyImage.sprite = _allBonuses[i].BonusesIcon;
            temporalPanel.BonusKind = _allBonuses[i].BonusKind;
            temporalPanel.Prices = _allBonuses[i].Prices;
            temporalPanel.RefreshPrice();
                        
            temporalPanel.name = _allBonuses[i].BonusKind.ToString();
            
            temporalPanel.GetComponentInChildren<Button>().onClick.AddListener(() => temporalPanel.Buy());
        }
    }
}
