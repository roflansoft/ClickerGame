using System.Collections.Generic;

public class ResourceBank
{
    private ResourceBank() => _bank = new Dictionary<GameResource, int>();

    public ResourceBank(int humansCount, int foodCount, int woodCount, int stoneCount, int goldCount) : this()
    {
        _bank[GameResource.Humans] = humansCount;
        _bank[GameResource.Food] = foodCount;
        _bank[GameResource.Wood] = woodCount;
        _bank[GameResource.Stone] = stoneCount;
        _bank[GameResource.Gold] = goldCount;
        _bank[GameResource.HumansProdLvl] = 0;
        _bank[GameResource.FoodProdLvl] = 0;
        _bank[GameResource.WoodProdLvl] = 0;
        _bank[GameResource.StoneProdLvl] = 0;
        _bank[GameResource.GoldProdLvl] = 0;
    }

    private readonly Dictionary<GameResource, int> _bank;

    public void ChangeResource(GameResource gameResource, int value) => _bank[gameResource] += value;

    public int GetResource(GameResource gameResource) => _bank[gameResource];
}
