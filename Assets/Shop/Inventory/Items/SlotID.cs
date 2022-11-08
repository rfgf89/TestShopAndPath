using UnityEngine;

public readonly struct SlotID
{
    private readonly uint _id;
    private readonly GameObject _parent;
    private static uint slotCreateIndex;
    private SlotID(uint id, GameObject parent)
    {
        _id = id;
        _parent = parent;
    }

    public static SlotID GetNew(GameObject parent) => new SlotID(++slotCreateIndex, parent);
    public GameObject GetParent() => _parent;
    public override string ToString() => "SlotID = " + _id;
    
}