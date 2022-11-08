using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Inventory : SelectableUI, IInventory, IInventoryPlace
{
    protected readonly Dictionary<SlotID, IInventorySlot> AllSlots = new Dictionary<SlotID, IInventorySlot>();

    public abstract void Init();
    
    public SlotID AddSlot(IInventorySlot slot, SlotID slotID)
    {
        AllSlots.Add(slotID, slot);
        return slotID;
    }
    
    public bool AddItem(SlotID slotID, ObtainableItem item)
    {
        if (AllSlots.TryGetValue(slotID, out var value))
        {
            value.isEmpty()?.SetItem(item);
            return value.AddItem();
        }
        return false;
    }

    public abstract void Lock();
    public abstract void Unlock();

    public bool AddItem(ObtainableItem item)
    {
        IInventorySlot slot = AllSlots.FirstOrDefault(pair => 
            pair.Value.isEmpty() || (pair.Value.isEmptySpace() && pair.Value.Item.Equals(item))).Value;

        if (!(slot?.isEmpty()?.SetItem(item) is null) || (!(slot.isEmptySpace() is null) && slot.Item.Equals(item)))
            return slot.isEmptySpace()?.AddItem() != null;

        return false;
    }
    
    public virtual void PlaceItem(Inventory from, ref ObtainableItem item, ref int count) {}
    public virtual void PlaceItemInSlot(Inventory from, SlotID slotID, ref ObtainableItem item, ref int count) {}
    public virtual void ReturnItem(SlotID slotID,  ref ObtainableItem item, ref int count) {}
    public virtual IInventorySlot TakeFromSlot(SlotID slotID, ref ObtainableItem item, ref int count) => null;
    
}