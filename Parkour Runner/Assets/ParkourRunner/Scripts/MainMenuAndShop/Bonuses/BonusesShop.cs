using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BonusesShop : MonoBehaviour

{
    [SerializeField] private List <BonusesShopData> _allBonuses = new List<BonusesShopData>();
    [SerializeField] private GameObject _bonusesPanel;
    [SerializeField] private GameObject _bonusesPlace;

    private List<GameObject> _allPanels = new List<GameObject>();
    private void Start()
    {
        InstantBonusesPanels();
    }

    private void InstantBonusesPanels()
    {
        for(int i = 0; i < _allBonuses.Count; i++)
        {
            GameObject temporalPanel = Instantiate(_bonusesPanel, _bonusesPlace.transform);
            temporalPanel.GetComponent<ShopBonusesPanel>().MyImage.sprite = _allBonuses[i].BonusesIcon;
            temporalPanel.GetComponent<ShopBonusesPanel>().MyPrice.text = _allBonuses[i].PayForThis;
            temporalPanel.name = _allBonuses[i].BonusesName;
            temporalPanel.GetComponentInChildren<Button>().onClick.AddListener(() => temporalPanel.GetComponent<ShopBonusesPanel>().BuyThisThing());
            _allPanels.Add(temporalPanel);
        }
        _allPanels[0].GetComponentInChildren<Button>().onClick.AddListener(() => MakeAPurchase(0));
        _allPanels[1].GetComponentInChildren<Button>().onClick.AddListener(() => MakeAPurchase(1));
        _allPanels[2].GetComponentInChildren<Button>().onClick.AddListener(() => MakeAPurchase(2));
        _allPanels[3].GetComponentInChildren<Button>().onClick.AddListener(() => MakeAPurchase(3));
        _allPanels[4].GetComponentInChildren<Button>().onClick.AddListener(() => MakeAPurchase(4));
    }

    private void MakeAPurchase(int index)
    {
        _allPanels[index].GetComponent<ShopBonusesPanel>().MyPrice.text = _allBonuses[index].PayForThis;
    }



}
