using System;
using TMPro;
using UnityEngine;
using Zenject;

public class MoneyView : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textValueMoney;
    private IMoneyService _moneyService;
        
    [Inject]
    private void Construct(IMoneyService moneyService)
    {
        _moneyService = moneyService;
        _moneyService.moneyUpdate+= MoneyUpdate;
    }

    private void MoneyUpdate(float money)
    {
        textValueMoney.text = Mathf.Floor(money).ToString();
    }

    private void OnDestroy()
    {
        _moneyService.moneyUpdate-= MoneyUpdate;
    }
}
