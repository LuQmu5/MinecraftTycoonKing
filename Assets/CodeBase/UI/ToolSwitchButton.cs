using UnityEngine;
using UnityEngine.UI;

public class ToolSwitchButton : ButtonClickHandler
{
    [SerializeField] private Image _icon;

    [Header("Icons")]
    [SerializeField] private Sprite _swordIcon;
    [SerializeField] private Sprite _pickaxeIcon;

    protected override void OnButtonClicked()
    {
        
    }
}

