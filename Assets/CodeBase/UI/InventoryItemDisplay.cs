using TMPro;
using UnityEngine;

public class InventoryItemDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _count;
    [SerializeField] private ResourceTypes _resourceType;

    public ResourceTypes ResourceType => _resourceType;

    public void SetCount(int amount)
    {
        _count.text = "X" + amount;
    }
}