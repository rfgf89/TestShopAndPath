using UnityEngine;

public abstract class ObtainableItem
{
    public int ID { get; protected set; }
    public string Name { get; protected set; }
    public float Price { get; protected set; }
    private string Type { get; }
    public float MaxStack { get; protected set; }
    public Sprite Sprite;
    protected ObtainableItem(string type)
    {
        Name = "Untitled";
        Type = type;
        Price = 0;
        MaxStack = 999;
    }

    public bool Equals(ObtainableItem item) => item.Name == Name;

}