using UnityEngine;
using UnityEngine.UI;

public class HealthBarDisplay : MonoBehaviour
{
    [Header("Icons")]
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;

    [Header("UI components")]
    [SerializeField] private Image[] _images;

    [Header("Player Health")]
    [SerializeField] private Health _playerHealth;

    private int _currentIndex = 9;

    private void OnEnable()
    {
        _playerHealth.Changed += OnPlayerHealthChanged;
    }

    private void OnDisable()
    {
        _playerHealth.Changed -= OnPlayerHealthChanged;
    }

    private void OnPlayerHealthChanged(float amount)
    {
        int heartsCount = (int)amount;

        if (heartsCount <= _currentIndex)
        {
            for (int i = heartsCount; i < _images.Length; i++)
            {
                _images[i].sprite = _emptyHeart;
            }

            _currentIndex = heartsCount;
        }
    }
}