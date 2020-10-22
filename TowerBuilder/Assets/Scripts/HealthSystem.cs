using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public event EventHandler OnDamageTaken;
    public event EventHandler OnRepair;
    public event EventHandler OnDestroyed;
    public event EventHandler OnMaxHPChanged;

    [SerializeField] private int maxHP;
    private int currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHP -= damageAmount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        OnDamageTaken?.Invoke(this, EventArgs.Empty);

        if (IsDestroyed())
        {
            OnDestroyed?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Repair(int rocoveryAmount)
    {
        currentHP += rocoveryAmount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        OnRepair?.Invoke(this, EventArgs.Empty);
    }

    public void FullRepair()
    {
        currentHP = maxHP;

        OnRepair?.Invoke(this, EventArgs.Empty);
    }

    public void SetHPMax(int maxHP, bool updateHPAmount)
    {
        this.maxHP = maxHP;

        if (updateHPAmount)
        {
            currentHP = maxHP;
        }

        OnMaxHPChanged?.Invoke(this, EventArgs.Empty);
    }

    public float GetHPNormalized()
    {
        return (float)currentHP / maxHP;
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }

    public int GetMaxHP()
    {
        return maxHP;
    }

    public bool IsFullHP()
    {
        return currentHP == maxHP;
    }

    public bool IsDestroyed()
    {
        return currentHP == 0;
    }
}
