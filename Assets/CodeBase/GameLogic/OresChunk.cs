using DG.Tweening;
using UnityEngine;

public class OresChunk : MonoBehaviour
{
    public ResourceTypes Type { get; private set; } = ResourceTypes.Iron;

    private bool _isTriggered = false;

    public void Init(ResourceTypes type)
    {
        Type = type;

        transform.DOMoveY(1.25f, 1).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(new Vector3(0, 360f, 0), 4, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectableItemPicker picker) && _isTriggered == false)
        {
            _isTriggered = true;
            transform.DOKill();

            picker.PickUp(this);
        }
    }
}
