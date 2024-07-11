using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T _itemPrefab;
    private List<T> _pool;

    protected void InitPool(T itemPrefab)
    {
        _itemPrefab = itemPrefab;
    }

    public T GetItem()
    {
        var item = _pool.FirstOrDefault(i => i.gameObject.activeSelf == false);

        if (item == null)
        {
            item = Object.Instantiate(_itemPrefab);
            _pool.Add(item);
        }

        return item;
    }
}
