using Features.Services.UI.Factory.BaseUI;
using Features.UI.Windows.Base;
using Zenject;

namespace Features.Bootstrapp.Scripts
{
  public class ShipMainMenuBootstrapper : MonoInstaller
  {
    public override void Start()
    {
      base.Start();
      Container.Resolve<IUIFactory>();
    }

    public override void InstallBindings()
    {
      BindUIFactory();
    }
    
    private void BindUIFactory() =>
      Container.BindFactoryCustomInterface<BaseWindow, UIFactory, IUIFactory>().AsSingle();
  }
}