using UnityEngine;

public class CloseButton : ButtonClickHandler
{
    [SerializeField] private GameObject _closableObject;

    protected override void OnButtonClicked()
    {
        _closableObject.gameObject.SetActive(false);
    }
}
