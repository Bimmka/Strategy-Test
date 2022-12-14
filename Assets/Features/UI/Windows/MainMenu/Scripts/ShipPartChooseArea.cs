using System.Collections.Generic;
using Features.Services.ShipParts;
using Features.Ship.Data.Settings;
using Features.Ship.Scripts.Modules.Data;
using Features.Ship.Scripts.Weapons.Data;
using TMPro;
using UnityEngine;

namespace Features.UI.Windows.MainMenu.Scripts
{
  public class ShipPartChooseArea : MonoBehaviour
  {
    [SerializeField] private ShipsContainer shipsContainer;
    [SerializeField] private WeaponSettingsContainer weaponsContainer;
    [SerializeField] private ModulesSettingsContainer modulesContainer;
    [SerializeField] private ShipPartView view;
    [SerializeField] private TMP_Dropdown shipTypeDropdown;
    [SerializeField] private TMP_Dropdown firstWeaponTypeDropdown;
    [SerializeField] private TMP_Dropdown secondWeaponTypeDropdown;
    [SerializeField] private TMP_Dropdown[] moduleTypeDropdowns;

    public void Initialize()
    {
      InitializeShips();
      InitializeWeapons();
      InitializeModules();
    }

    public void Subscribe() => 
      shipTypeDropdown.onValueChanged.AddListener(OnChangeShipType);

    public void Cleanup() => 
      shipTypeDropdown.onValueChanged.RemoveListener(OnChangeShipType);

    public ShipChosenParts ChosenParts()
    {
      ShipType shipType = CollectShipType();
      WeaponType[] weaponTypes = CollectWeapons();
      ModuleType[] moduleTypes = CollectModules(ModulesCount(shipType));
      return new ShipChosenParts(shipType, weaponTypes, moduleTypes);
    }

    private void InitializeShips()
    {
      List<TMP_Dropdown.OptionData> shipOptions = new List<TMP_Dropdown.OptionData>(shipsContainer.Ships.Count);
      foreach (KeyValuePair<ShipType,ShipSettings> ship in shipsContainer.Ships)
      {
        shipOptions.Add(new TMP_Dropdown.OptionData(ship.Value.UIName));
      }
      
      shipTypeDropdown.AddOptions(shipOptions);
    }

    private void InitializeWeapons()
    {
      List<TMP_Dropdown.OptionData> weaponOptions = new List<TMP_Dropdown.OptionData>(shipsContainer.Ships.Count);
      foreach (KeyValuePair<WeaponType,WeaponSettings> weapon in weaponsContainer.Weapons)
      {
        weaponOptions.Add(new TMP_Dropdown.OptionData(weapon.Value.UIName));
      }
      
      firstWeaponTypeDropdown.AddOptions(weaponOptions);
      secondWeaponTypeDropdown.AddOptions(weaponOptions);
    }

    private void InitializeModules()
    {
      List<TMP_Dropdown.OptionData> moduleOptions = new List<TMP_Dropdown.OptionData>(modulesContainer.Modules.Count);
      foreach (KeyValuePair<ModuleType,ModuleSettings> module in modulesContainer.Modules)
      {
        moduleOptions.Add(new TMP_Dropdown.OptionData(module.Value.UIName));
      }

      for (int i = 0; i < moduleTypeDropdowns.Length; i++)
      {
        moduleTypeDropdowns[i].AddOptions(moduleOptions);
      }
    }

    private void OnChangeShipType(int index)
    {
      view.UpdateVisibleModulesCount(ModulesCount((ShipType) index));
    }

    private int ModulesCount(ShipType shipType) => 
      shipsContainer.Ships[shipType].ModulesCount;

    private ShipType CollectShipType() => 
      (ShipType) shipTypeDropdown.value;

    private WeaponType[] CollectWeapons() => 
      new[] {(WeaponType)firstWeaponTypeDropdown.value, (WeaponType)secondWeaponTypeDropdown.value};

    private ModuleType[] CollectModules(int modulesCount)
    {
      ModuleType[] types = new ModuleType[modulesCount];

      for (int i = 0; i < modulesCount; i++)
      {
        types[i] = (ModuleType) moduleTypeDropdowns[i].value;
      }

      return types;
    }
  }
}