using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ResourceBank ResourceBank { get; private set; }

    private void Awake() => ResourceBank = new ResourceBank(10, 5, 5, 0, 0);
}
