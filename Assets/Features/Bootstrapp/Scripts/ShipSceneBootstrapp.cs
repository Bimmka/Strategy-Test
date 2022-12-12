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

    public override void Start()
    {
      base.Start();
      WeaponType[] weapons = new WeaponType[] {WeaponType.Gun, WeaponType.Rocket};
      ModuleType[] modules = new ModuleType[] {ModuleType.AddHealth, ModuleType.AddShield};
      Container.Resolve<ShipSpawner>().Create(ShipType.Small, weapons, modules, PlayerType.First);
    }

    public override void InstallBindings()
    {
      BindShipSpawner();
      BindShipFactory();
    }

    private void BindShipSpawner() => 
      Container.Bind<ShipSpawner>().ToSelf().FromNew().AsSingle().WithArguments(shipSpawnParent, shipPrefab);

    private void BindShipFactory() => 
      Container.Bind<ShipFactory>().ToSelf().FromNew().AsSingle();
  }
}