using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    private const string IsWalking = nameof(IsWalking);

    private Animator _animator;

    private void Awake()
    {
        // лучше всегда через serializefield-ы
        _animator = GetComponent<Animator>();
    }

    public void SetWalkState(bool state)
    {
        _animator.SetBool(IsWalking, state);
    }
}