using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class CharacterControl : MonoBehaviour, ICollectableItemPicker
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private CharacterView _view;
    [SerializeField] private Health _health;
    [SerializeField] private Tool[] _tools;
    [SerializeField] private Transform _backpack;
    [SerializeField] private Transform _backpackPickUpPosition;

    private int _currentToolIndex = 1;
    private PlayerInput _input;
    private CharacterInventory _inventory;

    public event Action<ToolData> ToolSwitched;

    [Inject]
    public void Construct(PlayerInput input, CharacterInventory inventory)
    {
        _input = input;
        _inventory = inventory;

        SwitchTool();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Actions.SwitchTool.started += SwitchToolKeyPressed;

        ToolSwitchButton.Clicked += SwitchTool;
    }

    private void OnDisable()
    {
        _input.Actions.SwitchTool.started -= SwitchToolKeyPressed;

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

    public void PickUp(OresChunk oreChunk)
    {
        StartCoroutine(PickingUp(oreChunk));
    }

    private IEnumerator PickingUp(OresChunk oreChunk)
    {
        float flySpeed = 10;

        while (oreChunk.transform.position.y < _backpackPickUpPosition.position.y)
        {
            oreChunk.transform.Translate(Vector3.up * Time.deltaTime * flySpeed);

            oreChunk.transform.position = Vector3.MoveTowards(oreChunk.transform.position, 
                new Vector3(_backpackPickUpPosition.position.x, oreChunk.transform.position.y, _backpackPickUpPosition.transform.position.z),
                flySpeed * Time.deltaTime);

            Debug.Log("Up");

            yield return null;
        }

        while (oreChunk.transform.position.y > _backpack.position.y)
        {
            oreChunk.transform.Translate(Vector3.down * Time.deltaTime * flySpeed);

            oreChunk.transform.position = Vector3.MoveTowards(oreChunk.transform.position,
                new Vector3(_backpack.position.x, oreChunk.transform.position.y, _backpack.transform.position.z),
                flySpeed * Time.deltaTime);

            Debug.Log("Down");

            yield return null;
        }

        oreChunk.gameObject.SetActive(false);
        _inventory.Add(oreChunk.Type);
        Debug.Log(oreChunk.Type + " picked up");
    }

    private void Move()
    {
        Vector3 movementVector = Vector3.zero;

        Vector3 inputVector = _input.Movement.Move.ReadValue<Vector2>();
        // Vector3 inputVector = new Vector2(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical"));

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

    // animation curve
    // tween
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
