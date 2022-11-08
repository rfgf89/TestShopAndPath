public class Weapon : ObtainableItem
{
    protected Weapon() : base("Weapon")
    {
        Price = 100;
        MaxStack = 1;
    }
}