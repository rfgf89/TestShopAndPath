
using System;
using UnityEngine;

public class GoldService : IMoneyService
{
    private float _gold;
    public event Action<float> moneyUpdate; 
    public bool PayItem(ObtainableItem item, int count, float prc)
    {
  
        if (item == null || count == 0) return true;
    
        
        var price = item.Price * count * prc;
        if (_gold - price >= 0)
        {
            _gold -= price;
            moneyUpdate?.Invoke(_gold);
            return true;
        }
            
        return false;

    }

    public void SellItem(ObtainableItem item, int count, float prc)
    {
        if (item == null || count == 0) return;
        
        _gold += item.Price * count * prc;
        moneyUpdate?.Invoke(_gold);
    }
}