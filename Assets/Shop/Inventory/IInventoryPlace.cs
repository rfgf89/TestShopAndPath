public interface IInventoryPlace
{
    public void PlaceItem(Inventory from, ref ObtainableItem item, ref int count);
    public void PlaceItemInSlot(Inventory from, SlotID slotID, ref ObtainableItem item, ref int count);
    public void ReturnItem(SlotID slotID, ref ObtainableItem item, ref int count);
    public IInventorySlot TakeFromSlot(SlotID slotID, ref ObtainableItem item, ref int count);
}