using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _max;

    private float _current;

    private void Awake()
    {
        _current = _max;
    }

    public void ApplyDamage(float amount)
    {
        _current -= amount;

        if (_current <= 0)
            Destroy(gameObject);
    }
}
