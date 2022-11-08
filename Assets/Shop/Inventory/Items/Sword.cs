using UnityEngine;

public class Sword : Weapon
{
    public Sword(string name, Sprite sprite, int price)
    {
        Name = name;
        Sprite = sprite;
        Price = price;
        MaxStack = 1;
    }
}