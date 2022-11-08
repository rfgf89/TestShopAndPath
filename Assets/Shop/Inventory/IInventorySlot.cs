public interface IInventorySlot
{
    public SlotID SlotID { get; }
    public ObtainableItem Item { get; }
    public int Count { get; }
    public ObtainableItem GetItem(int count = 1);
    public InventorySlot SetItem(ObtainableItem item);
    public InventorySlot AddItem();
    public void Clear();
    public InventorySlot isEmpty();
    public InventorySlot isEmptySpace();
}