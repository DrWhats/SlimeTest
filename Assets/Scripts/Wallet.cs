using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public int coins;
    [SerializeField] private Player player;
    [SerializeField] private UiManager ui;
    
    [Header("Health")]
    [SerializeField] private int addHealthValue;
    [SerializeField] private int healthCostMultiplier;
    public int healthCost;
    
    [Header("Speed")]
    [SerializeField] private float addSpeedValue;
    [SerializeField] private int speedCostMultiplier;
    private float maxSpeed = 0.3f;
    public int speedCost;

    [Header("Strength")]
    [SerializeField] private int addStrengthValue;
    [SerializeField] private int strengthCostMultiplier;
    public int strengthCost;

    public void AddCoins(int value)
    {
        coins += value;
        ui.UpdateUI();
    }
    
    public bool RemoveCoins(int value)
    {
        if (value > coins)
        {
            ui.UpdateUI();
            return false;
        }
        else
        {
            coins -= value;
            ui.UpdateUI();
            return true;
        }
        
    }

    public void UpgradeStrength()
    {
        player.UpgradeStrength(addStrengthValue);
        RemoveCoins(strengthCost);
        strengthCost += strengthCostMultiplier;
        ui.UpdateUI();
    }
    
    public void UpgradeSpeed()
    {
        if (player.throwDelay > maxSpeed)
        {
            player.UpgradeSpeed(addSpeedValue);
            RemoveCoins(speedCost);
            speedCost += speedCostMultiplier;
        }
        else
        {
            ui.canUpSpeed = false;
        }
        ui.UpdateUI();

    }
    
    public void UpgradeHealth(int value)
    {
        player.UpgradeHealth(addHealthValue);
        RemoveCoins(healthCost);
        healthCost += healthCostMultiplier;
        ui.UpdateUI();
    }
    
}
