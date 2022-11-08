using UnityEngine;
using Zenject;

public class InventorySlotFactory : MonoBehaviour, IInventorySlotFactory
{
    [SerializeField] private GameObject _playerSlot;
    [SerializeField] private GameObject _shopSlot;
    
    private DiContainer Container;
    

    public InventorySlotFactory Init(DiContainer diContainer)
    {
        Container = diContainer;
        return this;
    }
    
    public IInventorySlot Create(InventorySlotType type, SlotID slotID, Transform container)
    {
        Container
            .Rebind<SlotID>()
            .FromInstance(slotID)
            .AsTransient();
        
        switch (type)
        {
            case InventorySlotType.Player : return Container
                .InstantiatePrefab(_playerSlot, container)
                .GetComponent<IInventorySlot>();
            
            case InventorySlotType.Shop : return Container
                .InstantiatePrefab(_shopSlot, container)
                .GetComponent<IInventorySlot>();
        }
        
        
        Debug.LogError("Not implementation in Inventory Slot Factory");
        return null;
    }
}