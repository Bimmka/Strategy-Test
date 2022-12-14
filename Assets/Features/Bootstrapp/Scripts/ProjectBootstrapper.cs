using Features.CustomCoroutine;
using Features.GameStates;
using Features.GameStates.Observer;
using Features.GameStates.Observer.Scripts;
using Features.GameStates.States;
using Features.SceneLoading.Scripts;
using Features.Services.Assets;
using Features.Services.StaticData;
using Features.Services.UI.Windows;
using Features.Ship.Data.InputBindings;
using Features.Ship.Data.Settings;
using Features.Ship.Scripts.Modules.Data;
using Features.Ship.Scripts.Weapons.Data;
using Features.UI.Windows.Data;
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
    [SerializeField] private WindowsContainer windowsContainer;
    [SerializeField] private LoadingCurtain loadingCurtain;
    [SerializeField] private GameStatesObserver gameStatesObserver;

    public override void Start()
    {
      base.Start();
      ResolveGameStates();
      ResolveGameStatesObserver();
    }

    public override void InstallBindings()
    {
      BindAssetProvider();
      BindStaticData();
      BindCoroutineRunner();
      BindSceneLoading();
      BindLoadingCurtain();
      BindWindowsService();
      BindGameStateMachine();
      BindGameStates();
      BindGameStatesObserver();
    }

    private void ResolveGameStates()
    {
      Container.Resolve<GameEndState>();
      Container.Resolve<GameLoadState>();
      Container.Resolve<GameLoopState>();
      Container.Resolve<MainMenuState>();
    }

    private void ResolveGameStatesObserver() => 
      Container.Resolve<GameStatesObserver>().StartGame();

    private void BindAssetProvider() => 
      Container.Bind<IAssetProvider>().To<AssetProvider>().FromNew().AsSingle();

    private void BindStaticData() =>
      Container.Bind<IStaticDataService>().To<StaticDataService>().FromNew().AsSingle()
        .WithArguments(shipsContainer, weaponsContainer, modulesContainer, bindingsContainer, windowsContainer);

    private void BindCoroutineRunner() => 
      Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();

    private void BindSceneLoading() => 
      Container.Bind<ISceneLoader>().To<SceneLoader>().FromNew().AsSingle();

    private void BindLoadingCurtain() => 
      Container.Bind<LoadingCurtain>().ToSelf().FromComponentInNewPrefab(loadingCurtain).AsSingle();

    private void BindWindowsService() => 
      Container.Bind<IWindowsService>().To<WindowsService>().FromNew().AsSingle();

    private void BindGameStateMachine() => 
      Container.Bind<IGameStateMachine>().To<GameStateMachine>().FromNew().AsSingle();

    private void BindGameStates()
    {
      Container.Bind<GameEndState>().ToSelf().FromNew().AsSingle();
      Container.Bind<GameLoadState>().ToSelf().FromNew().AsSingle();
      Container.Bind<GameLoopState>().ToSelf().FromNew().AsSingle();
      Container.Bind<MainMenuState>().ToSelf().FromNew().AsSingle();
    }

    private void BindGameStatesObserver() => 
      Container.Bind<GameStatesObserver>().ToSelf().FromComponentInNewPrefab(gameStatesObserver).AsSingle();
  }
}