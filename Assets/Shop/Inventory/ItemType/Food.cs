public class Food : ObtainableItem
{
    protected Food() : base("Food")
    {
        Price = 5;
        MaxStack = 16;
    }
}