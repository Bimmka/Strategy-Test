using Features.Ship.Data.InputBindings;
using Features.Ship.Data.Settings;
using Features.Ship.Scripts.Modules.Data;
using Features.Ship.Scripts.Weapons.Data;

namespace Features.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private readonly ShipsContainer shipsContainer;
    private readonly WeaponSettingsContainer weaponsContainer;
    private readonly ModulesSettingsContainer modulesContainer;
    private readonly ShipControlBindingsContainer inputContainer;

    public StaticDataService(ShipsContainer shipsContainer, WeaponSettingsContainer weaponsContainer, ModulesSettingsContainer modulesContainer,
      ShipControlBindingsContainer inputContainer)
    {
      this.shipsContainer = shipsContainer;
      this.weaponsContainer = weaponsContainer;
      this.modulesContainer = modulesContainer;
      this.inputContainer = inputContainer;
    }
    
    public ShipSettings ForShip(ShipType shipType) => 
      shipsContainer.Ships[shipType];

    public WeaponSettings ForWeapon(WeaponType weaponType) => 
      weaponsContainer.Weapons[weaponType];

    public ModuleSettings ForModule(ModuleType moduleType) => 
      modulesContainer.Modules[moduleType];

    public ShipControlBindings ForInput(PlayerType playerType) => 
      inputContainer.Bindings[playerType];
  }
}