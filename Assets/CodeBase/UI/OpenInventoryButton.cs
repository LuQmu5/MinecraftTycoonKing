using System;

public class OpenInventoryButton : ButtonClickHandler
{
    public static event Action Clicked;

    protected override void OnButtonClicked()
    {
        Clicked?.Invoke();
    }
}
