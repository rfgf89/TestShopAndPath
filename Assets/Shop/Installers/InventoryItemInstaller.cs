using UnityEngine;
using Zenject;

public class InventoryItemInstaller : MonoInstaller, IInitializable
{
    [SerializeField] private PlayerInventoryController _playerInventoryController;
    [SerializeField] private ShopInventoryController _shopInventoryController;
    
    [SerializeField] private MoneyView _moneyView;
    
    [SerializeField] private AppleItemMarker[] _appleItemMarkers;
    [SerializeField] private SwordItemMarker[] _swordItemMarkers;
    
    public override void InstallBindings()
    {
        BindSelf();
    }

    private void BindSelf()
    {
        Container
            .BindInterfacesTo<InventoryItemInstaller>()
            .FromInstance(this)
            .AsSingle();
    }

    public void Initialize()
    {
        Container.Inject(_playerInventoryController);
        _playerInventoryController.Init();
        
        Container.Inject(_shopInventoryController);
        _shopInventoryController.Init();

        Container.Inject(_moneyView);
        
        var appleFactory = Container.Resolve<IAppleFactory>();
        var swordFactory = Container.Resolve<ISwordFactory>();
        
        foreach (var appleItemMarker in _appleItemMarkers)
            appleFactory.Create(appleItemMarker, _playerInventoryController);
        
        foreach (var swordItemMarker in _swordItemMarkers)
            swordFactory.Create(swordItemMarker, _playerInventoryController);
        
    }
}
