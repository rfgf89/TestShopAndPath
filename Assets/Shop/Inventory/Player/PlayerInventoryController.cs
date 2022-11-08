using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class PlayerInventoryController : Inventory
{

    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private ScrollRect _slotContainer;

    private IInventorySlotFactory _slotFactory;
    private const InventorySlotType SLOT_TYPE = InventorySlotType.Player;

    [Inject]
    private void Construct(IInventorySlotFactory slotFactory)
    {
        _slotFactory = slotFactory;
    }

    public override void Init()
    {
        for (var i = 0; i < _width; i++)
        {
            for (var j = 0; j < _height; j++)
            {
                var slotID = SlotID.GetNew(gameObject);
                AddSlot(_slotFactory.Create(SLOT_TYPE, slotID, _slotContainer.transform), slotID);
            }
        }
    }
    
    public override void Lock() => _slotContainer.enabled = false;
    public override void Unlock() => _slotContainer.enabled = true;
    
    private ObtainableItem GetAllItemFromSlot(SlotID slotID, out int count)
    {
        ObtainableItem item = null;
        count = 0;
        if (AllSlots.TryGetValue(slotID, out var slot))
        {
            if (slot.Count == 0)
                return null;
            
            count = slot.Count;
            item = slot.GetItem(slot.Count);
        }
        
        return item;
    }
    
    public override void PlaceItem(Inventory from, ref ObtainableItem item, ref int count)
    {
        while ( count > 0 && AddItem(item))
            count--;

        if (count == 0)
            item = null;
        from.Unlock();
        Unlock();
    }
    public override void PlaceItemInSlot(Inventory from, SlotID slotID, ref ObtainableItem item, ref int count)
    {
        while (count > 0 && AddItem(slotID, item))
            count--;
        
        if (count == 0)
            item = null;
        from.Unlock();
        Unlock();
    }
    
    public override void ReturnItem(SlotID slotID,  ref ObtainableItem item, ref int count)
    {
        if (AllSlots.TryGetValue(slotID, out var slot))
        {
            slot.SetItem(item);
            for (int i = 0; i < count; i++)
                slot.AddItem();

            count = 0;
            item = null;
            Unlock();
        }
    }
    public override IInventorySlot TakeFromSlot(SlotID slotID, ref ObtainableItem item, ref int count)
    {
        count = 0;
        
        if (AllSlots.TryGetValue(slotID, out var slot))
        {
            item = GetAllItemFromSlot(slotID, out count);
            if(item!=null)
                Lock();
            
            return slot;
        }

        return null;
    }
    
}