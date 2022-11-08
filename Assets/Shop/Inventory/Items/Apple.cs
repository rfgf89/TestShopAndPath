using UnityEngine;

public class Apple : Food
{
    public Apple(string name, Sprite sprite, int price)
    {
        Name = name;
        Sprite = sprite;
        Price = price;
        MaxStack = 8;
    }
}