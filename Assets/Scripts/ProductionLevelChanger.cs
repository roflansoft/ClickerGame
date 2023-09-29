using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductionLevelChanger : MonoBehaviour
{
    [SerializeField] private Button buyButton;

    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private GameResource productionLevelResource;

    [SerializeField] private int cost;

    private int Cost
    {
        get => cost;

        set
        {
            if (value < 0)
            {
                throw new ArgumentException($"Cost value {value} is less than zero!");
            }

            cost = value;
            _buyButtonText.text = $"Buy for {cost} gold";
        }
    }
    
    private ProductionBuilding _productionBuilding;
    
    private GameManager _gameManager;

    private TextMeshProUGUI _buyButtonText;

    private int _level;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _productionBuilding = GetComponent<ProductionBuilding>();
        _buyButtonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();
        buyButton.onClick.AddListener(Buy);
    }

    private void Start()
    {
        _buyButtonText.text = $"Buy for {cost} gold";
        levelText.text = $"{_level + 1}";
    }

    private void Update()
    {
        var productionLevel = _gameManager.ResourceBank.GetResource(productionLevelResource);
        if (productionLevel == _level)
        {
            return;
        }

        _level = productionLevel;
        levelText.text = $"{_level + 1}";
    }

    private void Buy()
    {
        var resourceBank = _gameManager.ResourceBank;
        if (resourceBank.GetResource(GameResource.Gold) < Cost)
        {
            StartCoroutine(ShowMessageOnButton("Not enough gold"));
            return;
        }
        
        resourceBank.ChangeResource(GameResource.Gold, -Cost);
        resourceBank.ChangeResource(productionLevelResource, 1);
        _productionBuilding.UpdateProductionTime();
        Cost *= 2;
    }

    private IEnumerator ShowMessageOnButton(string message)
    {
        buyButton.interactable = false;
        string currentText = _buyButtonText.text;
        _buyButtonText.text = message;
        yield return new WaitForSeconds(1);
        _buyButtonText.text = currentText;
        buyButton.interactable = true;
    }
}
