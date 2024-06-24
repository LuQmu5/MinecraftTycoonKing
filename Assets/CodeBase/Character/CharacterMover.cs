using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private CharacterView _view;

    private PlayerInput _input;
    private CharacterController _characterController;

    private void Awake()
    {
        _input = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input?.Disable();
    }

    private void Update()
    {
        Vector3 movementVector = Vector3.zero;
        Vector3 inputVector = _input.Movement.Move.ReadValue<Vector2>();

        if (inputVector.sqrMagnitude > 0.1f)
        {
            movementVector = Camera.main.transform.TransformDirection(inputVector);
            movementVector.y = 0;
            movementVector.Normalize();

            transform.forward = movementVector;
        }

        _characterController.Move(_speed * movementVector * Time.deltaTime);
        _view.SetWalkState(movementVector != Vector3.zero);
    }
}

public class ToolSwitcher : MonoBehaviour
{
    [SerializeField] private Tool _currentTool;
}

public class Tool : MonoBehaviour
{

}