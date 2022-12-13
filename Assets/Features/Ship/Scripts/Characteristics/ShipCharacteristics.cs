using System.Collections.Generic;
using Features.Ship.Scripts.Modules.Data;
using Features.Ship.Scripts.Weapons.Data;

namespace Features.Ship.Scripts.Characteristics
{
  public class ShipCharacteristics
  {
    public HealthCharacteristics Health { get; private set; }
    public ShieldCharacteristics Shield { get; private set; }
    public List<WeaponCharacteristics> Weapons { get; private set; }
    public List<ModuleCharacteristics> Modules { get; private set; }

    public ShipCharacteristics()
    {
      Weapons = new List<WeaponCharacteristics>(2);
      Modules = new List<ModuleCharacteristics>(3);
    }

    public void InitializeHealth(float count) => 
      Health = new HealthCharacteristics(count);

    public void InitializeShield(float count, float restoreValue, float restoreTime) => 
      Shield = new ShieldCharacteristics(count, restoreValue, restoreTime);

    public void InitializeWeapon(WeaponType type, int damage, float reloadTime) => 
      Weapons.Add(new WeaponCharacteristics(type, damage, reloadTime));

    public void InitializeModule(ModuleType type, float value) => 
      Modules.Add(new ModuleCharacteristics(type, value));

    public void AddHealth(float count) => 
     InitializeHealth(Health.Max + count);

    public void AddShield(float count) => 
      InitializeShield(Shield.Max + count, Shield.RestoreValue, Shield.RestoreTime);

    public void IncreaseShieldRestoreValue(float increaseCoefficient) => 
      InitializeShield(Shield.Max, Shield.RestoreValue* (1 + increaseCoefficient), Shield.RestoreTime);

    public void ReduceWeaponsReloadTime(float reduceCoefficient)
    {
      for (int i = 0; i < Weapons.Count; i++)
      {
        Weapons[i] = new WeaponCharacteristics(Weapons[i].Type, Weapons[i].Damage, Weapons[i].ReloadTime * (1-reduceCoefficient));
      }
    }
   
  }
}