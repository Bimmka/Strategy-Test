using Features.Bullet.Data;
using Features.Bullet.Scripts.Factory;
using Features.Bullet.Scripts.Spawner;
using Features.Services.Cleanup;
using Features.Ship.Data.InputBindings;
using Features.Ship.Data.Settings;
using Features.Ship.Scripts.Base;
using Features.Ship.Scripts.Factory;
using Features.Ship.Scripts.Modules.Data;
using Features.Ship.Scripts.Spawn;
using Features.Ship.Scripts.Weapons.Data;
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

    public override void Start()
    {
      base.Start();
      WeaponType[] weapons = new WeaponType[] {WeaponType.Gun, WeaponType.RocketLauncher};
      ModuleType[] modules = new ModuleType[] {ModuleType.AddHealth, ModuleType.AddShield};
      ShipSpawner spawner = Container.Resolve<ShipSpawner>();
      spawner.Create(ShipType.Small, weapons, modules, PlayerType.First, Vector3.left * 10);
      spawner.Create(ShipType.Big, weapons, modules, PlayerType.Second, Vector3.right * 10);
    }

    public override void InstallBindings()
    {
      BindShipSpawner();
      BindShipFactory();
      BindBulletSpawner();
      BindBulletFactory();
      BindCleanupService();
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
  }
}