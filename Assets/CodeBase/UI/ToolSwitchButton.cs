using System;
using UnityEngine;

public class ToolSwitchButton : ButtonClickHandler
{
    public static event Action Clicked;

    protected override void OnButtonClicked()
    {
        Clicked?.Invoke();
    }
}
