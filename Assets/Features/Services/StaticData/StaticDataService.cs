using Features.Services.UI.Factory;
using Features.Ship.Data.InputBindings;
using Features.Ship.Data.Settings;
using Features.Ship.Scripts.Modules.Data;
using Features.Ship.Scripts.Weapons.Data;
using Features.StaticData.Windows;
using Features.UI.Windows.Data;

namespace Features.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private readonly ShipsContainer shipsContainer;
    private readonly WeaponSettingsContainer weaponsContainer;
    private readonly ModulesSettingsContainer modulesContainer;
    private readonly ShipControlBindingsContainer inputContainer;
    private readonly WindowsContainer windowsContainer;

    public StaticDataService(ShipsContainer shipsContainer, WeaponSettingsContainer weaponsContainer, ModulesSettingsContainer modulesContainer,
      ShipControlBindingsContainer inputContainer, WindowsContainer windowsContainer)
    {
      this.shipsContainer = shipsContainer;
      this.weaponsContainer = weaponsContainer;
      this.modulesContainer = modulesContainer;
      this.inputContainer = inputContainer;
      this.windowsContainer = windowsContainer;
    }
    
    public ShipSettings ForShip(ShipType shipType) => 
      shipsContainer.Ships[shipType];

    public WeaponSettings ForWeapon(WeaponType weaponType) => 
      weaponsContainer.Weapons[weaponType];

    public ModuleSettings ForModule(ModuleType moduleType) => 
      modulesContainer.Modules[moduleType];

    public ShipControlBindings ForInput(PlayerType playerType) => 
      inputContainer.Bindings[playerType];

    public WindowInstantiateData ForWindow(WindowId id) => 
      windowsContainer.InstantiateData[id];
  }
}