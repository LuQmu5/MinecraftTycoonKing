using UnityEngine;

public class PickaxeCollidesHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ore ore))
        {
            Destroy(ore.gameObject);
        }
    }
}
