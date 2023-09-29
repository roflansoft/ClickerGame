using TMPro;
using UnityEngine;

[System.Serializable]
public class ResourceVisual : MonoBehaviour
{
    [SerializeField] private GameResource resource;
    
    [SerializeField] private TextMeshProUGUI countText;

    private GameManager _gameManager;

    private int _count;

    private void Awake() => _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    private void Update()
    {
        var recourseCount = _gameManager.ResourceBank.GetResource(resource);
        if (_count == recourseCount)
        {
            return;
        }

        _count = recourseCount;
        countText.text = $"{_count}";
    }
}
