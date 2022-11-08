using UnityEngine;

public interface IInventorySlotFactory
{
    public IInventorySlot Create(InventorySlotType type, SlotID slotID, Transform container);
}