using UnityEngine;

public class Ore : MonoBehaviour
{
    [SerializeField] private OresChunk _dropableItem;
    [SerializeField] private ResourceTypes _type;
    [SerializeField] private int _durability = 3;

    private void Awake()
    {
        _dropableItem.Init(_type);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Pickaxe pickaxe))
        {
            _durability--;

            if (_durability > 0)
                return;

            _dropableItem.gameObject.SetActive(true);
            _dropableItem.transform.parent = null;

            gameObject.SetActive(false);
        }
    }
}
