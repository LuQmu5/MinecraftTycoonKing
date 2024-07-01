using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class CharacterControl : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private CharacterView _view;
    [SerializeField] private Health _health;
    [SerializeField] private Tool[] _tools;

    private int _currentToolIndex = 0;
    private PlayerInput _input;

    public event Action<ToolData> ToolSwitched;

    [Inject]
    public void Construct(PlayerInput input)
    {
        _input = input;

        SwitchTool();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Actions.SwitchTool.performed += SwitchToolKeyPressed;
        ToolSwitchButton.Clicked += SwitchTool;
    }

    private void OnDisable()
    {
        _input.Actions.SwitchTool.performed -= SwitchToolKeyPressed;
        ToolSwitchButton.Clicked -= SwitchTool;
        _input.Disable();
    }

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Sword sword))
        {
            _health.ApplyDamage(1);
            StartCoroutine(KnocningBack(sword.transform.position));
        }
    }

    private void Move()
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
        _characterController.Move(Vector2.down);
        _view.SetWalkState(movementVector != Vector3.zero);
    }

    private IEnumerator KnocningBack(Vector3 from)
    {
        int directionX = transform.position.x < from.x ? -1 : 1;
        Vector3 firstPoint = transform.position + new Vector3(directionX * 2.2f, 0);
        Vector3 secondPoint = transform.position + new Vector3(directionX * 1.7f, 2);
        Vector3 thirdPoint = transform.position + new Vector3(directionX * 1.2f, 3);
        Vector3 fourthPoint = transform.position;
        float time = 1; // 0 - 1
        float speed = 2;

        while (time > 0)
        {
            transform.position = Bezier.GetPoint(firstPoint, secondPoint, thirdPoint, fourthPoint, time);

            time -= Time.deltaTime * speed;

            yield return null;
        }
    }


    private void SwitchToolKeyPressed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        SwitchTool();
    }

    private void SwitchTool()
    {
        _tools[_currentToolIndex].gameObject.SetActive(false);

        _currentToolIndex++;

        if (_currentToolIndex == _tools.Length)
            _currentToolIndex = 0;

        _tools[_currentToolIndex].gameObject.SetActive(true);

        ToolSwitched?.Invoke(_tools[_currentToolIndex].Data);
    }
}
