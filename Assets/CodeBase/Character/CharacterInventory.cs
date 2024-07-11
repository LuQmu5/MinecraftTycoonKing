using System.Collections.Generic;

public class CharacterInventory
{
    private Dictionary<ResourceTypes, int> _resourcesMap;

    public CharacterInventory()
    {
        _resourcesMap = new Dictionary<ResourceTypes, int>();
    }

    public void Add(ResourceTypes resourceType)
    {
        if (_resourcesMap.ContainsKey(resourceType))
        {
            _resourcesMap[resourceType]++;
        }
        else
        {
            _resourcesMap[resourceType] = 1;
        }
    }

    public int GetResourceCount(ResourceTypes resourceType)
    {
        if (_resourcesMap.ContainsKey(resourceType) == false)
            return 0;

        return _resourcesMap[resourceType];
    }
}