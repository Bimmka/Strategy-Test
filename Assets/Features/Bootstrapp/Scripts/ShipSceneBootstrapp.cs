using Features.Bullet.Data;
using Features.Bullet.Scripts.Factory;
using Features.Bullet.Scripts.Spawner;
using Features.Level.Data;
using Features.Level.Scripts.Flow;
using Features.Level.Scripts.Observer;
using Features.Services.Cleanup;
using Features.Services.UI.Factory.BaseUI;
using Features.Ship.Scripts.Base;
using Features.Ship.Scripts.Factory;
using Features.Ship.Scripts.Spawn;
using Features.UI.Windows.Base;
using UnityEngine;
using Zenject;

namespace Features.Bootstrapp.Scripts
{
  public class ShipSceneBootstrapp : MonoInstaller
  {
    [SerializeField] private Transform shipSpawnParent;
    [SerializeField] private ShipPresenter shipPrefab;
    [SerializeField] private Transform bulletSpawnParent;
    [SerializeField] private BulletsContainer bulletsContainer;
    [SerializeField] private LevelFlowObserver levelFlowObserver;
    [SerializeField] private LevelSettings levelSettings;

    public override void Start()
    {
      base.Start();
      Container.Resolve<IUIFactory>();
      Container.Resolve<LevelFlowObserver>();
    }

    public override void InstallBindings()
    {
      BindShipSpawner();
      BindShipFactory();
      BindBulletSpawner();
      BindBulletFactory();
      BindCleanupService();
      BindLevelFlow();
      BindLevelFlowObserver();
      BindUIFactory();
    }

    private void BindShipSpawner() => 
      Container.Bind<ShipSpawner>().ToSelf().FromNew().AsSingle().WithArguments(shipSpawnParent, shipPrefab);

    private void BindShipFactory() => 
      Container.Bind<ShipFactory>().ToSelf().FromNew().AsSingle();

    private void BindBulletSpawner() => 
      Container.Bind<BulletSpawner>().ToSelf().FromNew().AsSingle().WithArguments(bulletSpawnParent, bulletsContainer);

    private void BindBulletFactory() => 
      Container.Bind<BulletFactory>().ToSelf().FromNew().AsSingle();

    private void BindCleanupService() => 
      Container.Bind<ICleanupService>().To<CleanupService>().FromNew().AsSingle();

    private void BindLevelFlow() => 
      Container.Bind<LevelFlow>().ToSelf().FromNew().AsSingle().WithArguments(levelSettings);
    
    private void BindLevelFlowObserver() => 
      Container.Bind<LevelFlowObserver>().ToSelf().FromComponentInNewPrefab(levelFlowObserver).AsSingle();
    
    private void BindUIFactory() =>
      Container.BindFactoryCustomInterface<BaseWindow, UIFactory, IUIFactory>().AsSingle();
  }
}