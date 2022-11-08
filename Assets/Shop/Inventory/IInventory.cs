public interface IInventory
{
    public bool AddItem(ObtainableItem item);
    public bool AddItem(SlotID slotID, ObtainableItem item);
    public void Lock();
    public void Unlock();
}