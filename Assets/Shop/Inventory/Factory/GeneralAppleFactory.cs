using UnityEngine;

public class GeneralAppleFactory : MonoBehaviour, IAppleFactory
{
    [SerializeField] private Sprite _greenApple;
    public void Create(AppleItemMarker appleItemMarker, Inventory inventory)
    {
        switch (appleItemMarker.appleType)
        {
            case AppleType.Green :
                for (int i = 0; i < appleItemMarker.count; i++)
                    inventory.AddItem(new Apple("Green Apple", _greenApple, 5));
                return;
        }
        
        Debug.LogError("Not implemented Apple for GeneralAppleFactory");
    }
}