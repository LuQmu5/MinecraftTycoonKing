using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Image))]
public class CurrentToolDisplay : MonoBehaviour
{
    [SerializeField] private Image _image;

    private CharacterControl _player;

    [Inject]
    public void Construct(CharacterControl player)
    {
        _player = player;
        _player.ToolSwitched += OnPlayerToolSwitched;
    }

    private void OnDestroy()
    {
        _player.ToolSwitched -= OnPlayerToolSwitched;
    }

    private void OnPlayerToolSwitched(ToolData data)
    {
        _image.sprite = data.Icon;
    }
}