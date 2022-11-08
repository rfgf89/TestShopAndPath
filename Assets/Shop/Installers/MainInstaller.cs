using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private InventorySlotFactory _slotFactory;
    [SerializeField] private GeneralAppleFactory _appleFactory;
    [SerializeField] private GeneralSwordFactory _swordFactory;

    public override void InstallBindings()
    {
        BindMoneyService();
        BindAppleFactory();
        BindSwordFactory();
        BindSlotFactory();
    }

    private void BindMoneyService()
    {
        Container
            .Bind<IMoneyService>()
            .To<GoldService>()
            .FromInstance(new GoldService())
            .AsSingle();
    }

    private void BindSwordFactory()
    {
        Container
            .Bind<ISwordFactory>()
            .To<GeneralSwordFactory>()
            .FromInstance(_swordFactory)
            .AsSingle();
    }

    private void BindAppleFactory()
    {
        Container
            .Bind<IAppleFactory>()
            .To<GeneralAppleFactory>()
            .FromInstance(_appleFactory)
            .AsSingle();
    }
    
    private void BindSlotFactory()
    {
        Container
            .Bind<IInventorySlotFactory>()
            .To<InventorySlotFactory>()
            .FromInstance(_slotFactory.Init(Container))
            .AsSingle();
    }
    
}