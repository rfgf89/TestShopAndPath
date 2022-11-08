using System;

public interface IMoneyService
{
    public event Action<float> moneyUpdate; 
    public bool PayItem(ObtainableItem item, int count, float prc);
    public void SellItem(ObtainableItem item, int count, float prc);
}