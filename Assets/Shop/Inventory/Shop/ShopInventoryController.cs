using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopInventoryController : Inventory
{
    [SerializeField] private ScrollRect _slotContainer;
    [SerializeField] private int _slotCount;
    [SerializeField, Range(0f, 1f)] private float _shopCost;
    [SerializeField, Range(0f, 1f)] private float _priceModify;
    
    private const InventorySlotType SLOT_TYPE = InventorySlotType.Shop;
    
    private IInventorySlotFactory _slotFactory;
    private IMoneyService _moneyService;
    private bool _sell = true;
    [Inject]
    private void Construct(IInventorySlotFactory slotFactory, IMoneyService moneyService)
    {
        _slotFactory = slotFactory;
        _moneyService = moneyService;
    }
    
    public override void Init()
    {
        for (var i = 0; i < _slotCount; i++)
        {
            var slotID = SlotID.GetNew(gameObject);
            AddSlot(_slotFactory.Create(SLOT_TYPE, slotID, _slotContainer.transform), slotID);
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
        if (_sell)
        {
            if (from != this)
                _moneyService.SellItem(item, count, _shopCost * _priceModify);
            else
                _moneyService.SellItem(item, count, _priceModify);
        }

        while ( count > 0 && AddItem(item))
            count--;

        if (count == 0)
            item = null;
        
        from.Unlock();
        Unlock();
    }
    public override void PlaceItemInSlot(Inventory from, SlotID slotID, ref ObtainableItem item, ref int count)
    {
        PlaceItem(from, ref item, ref count);
    }
    
    public override void ReturnItem(SlotID slotID,  ref ObtainableItem item, ref int count)
    {
        if(_sell)
            _moneyService.SellItem(item, count, _priceModify);
        
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
            if (_moneyService.PayItem(item, count, _priceModify))
            {
                if (item != null)
                    Lock();
            }
            else
            {
                _sell = false;
                PlaceItemInSlot(this, slotID, ref item, ref count);
                _sell = true;
            }

            return slot;
        }

        return null;
    }
    
}