using UnityEngine;

public class GeneralSwordFactory : MonoBehaviour, ISwordFactory
{
    [SerializeField] private Sprite _steelSword;
    public void Create(SwordItemMarker swordItemMarker, Inventory inventory)
    {
        switch (swordItemMarker.swordType)
        {
            case SwordType.Steel :
                for (int i = 0; i < swordItemMarker.count; i++)
                    inventory.AddItem(new Sword("Steel Sword", _steelSword, 25));
                return;
        }
        
        Debug.LogError("Not implemented Sword for GeneralSwordFactory");
     
    }
}