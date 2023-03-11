using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField] private TextMeshProUGUI coinsText;
    [Header("Strength")] 
    [SerializeField] private TextMeshProUGUI attackStrengthTextCost;
    [SerializeField] private TextMeshProUGUI attackStrengthText;
    [SerializeField] private GameObject strengthButton;
    [Header("Speed")] 
    [SerializeField] private TextMeshProUGUI attackSpeedTextCost;
    [SerializeField] private TextMeshProUGUI attackSpeedText;
    [SerializeField] private GameObject speedButton;
    [Header("Health")] 
    [SerializeField] private TextMeshProUGUI maxHealthTextCost;
    [SerializeField] private TextMeshProUGUI maxHealthText;
    [SerializeField] private GameObject maxHealthButton;
    
    [Header("Additional Required")] 
    [SerializeField] private Wallet wallet;
    [SerializeField] private Player player;

    public bool canUpSpeed = true;


    public void Awake()
    {
        UpdateUI();
    }

    public void UpdateCoinsUI()
    {
        coinsText.SetText(wallet.coins.ToString());
    }


    public void UpdateCurrentValues()
    {
        attackSpeedText.SetText((Mathf.Round(player.throwDelay * 10f) / 10f).ToString());
        attackStrengthText.SetText(player.strength.ToString());
        maxHealthText.SetText(player.maxHealth.ToString());
    }

    public void UpdateCurrentCosts()
    {
        if (canUpSpeed)
        {
            attackSpeedTextCost.SetText(wallet.speedCost.ToString());
        }
        else
        {
            attackSpeedTextCost.SetText("МАКС");
        }
        
        attackStrengthTextCost.SetText(wallet.strengthCost.ToString());
        maxHealthTextCost.SetText(wallet.healthCost.ToString());
    }


    void SetActiveSpeedButton(bool value)
    {
        if (canUpSpeed)
        {
            speedButton.GetComponent<Button>().interactable = value;
        }
        else
        {
            speedButton.GetComponent<Button>().interactable = false;
            
        }
        
    }

    void SetActiveStrengthButton(bool value)
    {
        strengthButton.GetComponent<Button>().interactable = value;
    }

    void SetActiveHealthButton(bool value)
    {
        maxHealthButton.GetComponent<Button>().interactable = value;
    }

    public void UpdateUI()
    {
        SetActiveHealthButton(wallet.coins >= wallet.healthCost);
        SetActiveSpeedButton(wallet.coins >= wallet.speedCost);
        SetActiveStrengthButton(wallet.coins >= wallet.strengthCost);
        UpdateCoinsUI();
        UpdateCurrentCosts();
        UpdateCurrentValues();
    }
}