using Features.CustomCoroutine;
using Features.Services.Assets;
using Features.Services.StaticData;
using Features.Ship.Data.InputBindings;
using Features.Ship.Data.Settings;
using Features.Ship.Scripts.Modules.Data;
using Features.Ship.Scripts.Weapons.Data;
using UnityEngine;
using Zenject;

namespace Features.Bootstrapp.Scripts
{
  public class ProjectBootstrapper : MonoInstaller, ICoroutineRunner
  {
    [SerializeField] private ShipsContainer shipsContainer;
    [SerializeField] private WeaponSettingsContainer weaponsContainer;
    [SerializeField] private ModulesSettingsContainer modulesContainer;
    [SerializeField] private ShipControlBindingsContainer bindingsContainer;

    public override void InstallBindings()
    {
      BindAssetProvider();
      BindStaticData();
      BindCoroutineRunner();
    }

    private void BindAssetProvider() => 
      Container.Bind<IAssetProvider>().To<AssetProvider>().FromNew().AsSingle();

    private void BindStaticData()
    {
      Container.Bind<IStaticDataService>().To<StaticDataService>().FromNew().AsSingle()
        .WithArguments(shipsContainer, weaponsContainer, modulesContainer, bindingsContainer);
    }

    private void BindCoroutineRunner() => 
      Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();
  }
}