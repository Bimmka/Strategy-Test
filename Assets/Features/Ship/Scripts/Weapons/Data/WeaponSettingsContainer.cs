using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Ship.Scripts.Weapons.Data
{
  [CreateAssetMenu(fileName = "WeaponSettingsContainer", menuName = "StaticData/Ship/Weapon/Create Weapons Settings Container", order = 52)]
  public class WeaponSettingsContainer : SerializedScriptableObject
  {
    [ValidateInput("ValidateDictionaryKeys", "Keys Not Dictionary Does Not Equal Weapon Type", IncludeChildren = true)]
    public Dictionary<WeaponType, WeaponSettings> Weapons;
    
    private bool ValidateDictionaryKeys(Dictionary<WeaponType, WeaponSettings> weapons)
    {
      if (weapons == null || weapons.Count == 0)
        return true;

      foreach (KeyValuePair<WeaponType,WeaponSettings> weapon in weapons)
      {
        if (weapon.Key != weapon.Value.Type)
          return false;
      }

      return true;
    }
  }
}