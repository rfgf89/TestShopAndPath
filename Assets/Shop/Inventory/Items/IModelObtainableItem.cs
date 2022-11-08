using System;

public interface IModelObtainableItem
{
    public event Action updateSlot;
    public ObtainableItem Item { get; }
    public int Count { get; }
}