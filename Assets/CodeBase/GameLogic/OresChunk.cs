using UnityEngine;

public class OresChunk : MonoBehaviour
{
    public ResourceTypes Type { get; private set; }

    public void Init(ResourceTypes type)
    {
        Type = type;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectableItemPicker picker))
        {
            picker.PickUp(this);
        }
    }
}
