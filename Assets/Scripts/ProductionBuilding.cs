using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProductionBuilding : MonoBehaviour
{
    [SerializeField] private GameResource resource;

    [SerializeField] private Slider productionSlider;

    [SerializeField] private Button addButton;

    [SerializeField] private int count;

    [SerializeField] private float defaultProductionTime;

    private GameManager _gameManager;
    
    private float _realProductionTime;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        addButton.onClick.AddListener(() => StartCoroutine(AddResource()));
        addButton.onClick.AddListener(() => StartCoroutine(MoveSlider()));
        _realProductionTime = defaultProductionTime;
        productionSlider.minValue = 0;
        productionSlider.enabled = false;
        productionSlider.gameObject.SetActive(false);
    }

    private IEnumerator AddResource()
    {
        addButton.interactable = false;
        yield return new WaitForSeconds(_realProductionTime);
        _gameManager.ResourceBank.ChangeResource(resource, count);
        addButton.interactable = true;
    }

    private IEnumerator MoveSlider()
    {
        productionSlider.maxValue = _realProductionTime;
        productionSlider.value = 0;
        productionSlider.gameObject.SetActive(true);
        var startMovingTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup - startMovingTime < _realProductionTime)
        {
            productionSlider.value = Time.realtimeSinceStartup - startMovingTime;
            yield return null;
        }

        productionSlider.gameObject.SetActive(false);
    }

    public void UpdateProductionTime()
    {
        _realProductionTime = resource switch
        {
            GameResource.Humans => defaultProductionTime /
                                   (1 + _gameManager.ResourceBank.GetResource(GameResource.HumansProdLvl)),
            GameResource.Food => defaultProductionTime /
                                 (1 + _gameManager.ResourceBank.GetResource(GameResource.FoodProdLvl)),
            GameResource.Wood => defaultProductionTime /
                                 (1 + _gameManager.ResourceBank.GetResource(GameResource.WoodProdLvl)),
            GameResource.Stone => defaultProductionTime /
                                  (1 + _gameManager.ResourceBank.GetResource(GameResource.StoneProdLvl)),
            GameResource.Gold => defaultProductionTime /
                                 (1 + _gameManager.ResourceBank.GetResource(GameResource.GoldProdLvl)),
            _ => throw new ArgumentException($"Resource {resource} can not have production time!")
        };
    }
}
