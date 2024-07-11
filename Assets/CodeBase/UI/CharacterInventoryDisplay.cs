using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CharacterInventoryDisplay : MonoBehaviour
{
    [SerializeField] private InventoryItemDisplay[] _items;

    private CharacterInventory _inventory;
    private PlayerInput _playerInput;

    [Inject]
    public void Construct(CharacterInventory inventory, PlayerInput playerInput)
    {
        _inventory = inventory;
        _playerInput = playerInput;

        _playerInput.Actions.OpenInventory.started += ToggleInventory;
    }

    private void OnDestroy()
    {
        _playerInput.Actions.OpenInventory.started -= ToggleInventory;
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        if (gameObject.activeSelf)
            Hide();
        else
            Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);

        foreach (var item in _items)
        {
            item.SetCount(_inventory.GetResourceCount(item.ResourceType));
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
