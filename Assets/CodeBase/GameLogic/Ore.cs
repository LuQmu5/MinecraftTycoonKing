using UnityEngine;

public class Ore : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Pickaxe pickaxe))
        {
            Destroy(gameObject);
        }
    }
}