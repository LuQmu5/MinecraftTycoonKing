using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalance 
{
    private int _diamonds = 0;

    public event Action<int> Changed;

    public void AddDiamonds(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException($"{amount} can't be less or equals 0");

        _diamonds += amount;

        Changed?.Invoke(_diamonds);
    }

    public bool TrySpendDiamonds(int requiredAmount)
    {
        if (requiredAmount <= 0)
            throw new ArgumentException($"{requiredAmount} can't be less or equals 0");

        if (_diamonds < requiredAmount)
            return false;

        _diamonds -= requiredAmount;
        Changed?.Invoke(_diamonds);

        return true;
    }
}
