using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerTempSlot : MonoBehaviour, IModelObtainableItem
{
    [SerializeField] private PlayerInput _playerInput;
    private ObtainableItem _item;
    public int _count;
    private IInventorySlot _currentSlot;
    
    public event Action updateSlot;
    public ObtainableItem Item => _item;
    public int Count => _count;

    private void Start()
    {
        _playerInput.currentActionMap["Click"].Enable();
        _playerInput.currentActionMap["Click"].performed += Click;
    }

    private void Click(InputAction.CallbackContext obj)
    {
        GameObject select = EventSystem.current.currentSelectedGameObject;

        if (select != null)
        {
            
            var slot = select.GetComponent<IInventorySlot>();
            if (slot != null)
            {
                var place = slot.SlotID.GetParent().GetComponent<Inventory>();
                
                if (_count == 0)
                    _currentSlot = place.TakeFromSlot(slot.SlotID, ref _item, ref _count);
                else if (slot.Item == null)
                    place.PlaceItemInSlot(_currentSlot.SlotID.GetParent().GetComponent<Inventory>(), slot.SlotID, ref _item, ref _count);
                else
                    place.ReturnItem(_currentSlot.SlotID, ref _item, ref _count);
                updateSlot?.Invoke();
            }
            else if (_count != 0)
            {
                var place = select.GetComponent<Inventory>();
                if (place != null && _count != 0)
                {
                    place.PlaceItem(place, ref _item, ref _count);
                    updateSlot?.Invoke();
                }
            }
        }
        else if (_count != 0)
        {
            var place = _currentSlot.SlotID.GetParent().GetComponent<IInventoryPlace>();
            var inventory = _currentSlot.SlotID.GetParent().GetComponent<IInventory>();
            place.ReturnItem(_currentSlot.SlotID, ref _item, ref _count);
            inventory.Unlock();
            updateSlot?.Invoke();
        }
    }

}