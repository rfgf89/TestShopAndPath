using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class InventorySlot : SelectableUI, IInventorySlot, IModelObtainableItem
{
    
    public event Action updateSlot;
    public ObtainableItem Item { get; private set;}
    public int Count { get; private set; }
    public SlotID SlotID { get; private set; }
    
    [Inject]
    public void Construct(SlotID slotID)
    {
        SlotID = slotID;
    }

    private void Start() => updateSlot?.Invoke();

    public ObtainableItem GetItem(int count = 1)
    {
        if(count == 0)
            Debug.LogError("Inventory Slot : trying get zero item");
        if (count > Count)
            Debug.LogError("Inventory Slot : can't get more items in the slot");
        
        ObtainableItem result = null;
        
        if (count == Count)
        {
            result = Item;
            Item = null;
            Count = 0;
        }
        else
            Count -= count;
        
        updateSlot?.Invoke();
        return result;
    }

    public InventorySlot AddItem()
    {
        if (Item == null)
        {
            Debug.LogError("Inventory Slot : can't add item in Null ");
            return null;
        }

        if (Count < Item.MaxStack)
        {
            Count++;
            updateSlot?.Invoke();
            return this;
        }
        
        return null;
    }
    
    public InventorySlot SetItem(ObtainableItem item)
    {
        if (Item == null)
        {
            Item = item;
            updateSlot?.Invoke();
            return this;
        }

        return null;
    }
    public void Clear()
    {
        Count = 0;
        Item = null;
        updateSlot?.Invoke();
    }

    public InventorySlot isEmpty() => Count == 0 ? this : null;
    public InventorySlot isEmptySpace() => Count == 0 ? this : (Count < Item.MaxStack ? this : null);

}