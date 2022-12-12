using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Ship.Scripts.Modules.Data
{
  [CreateAssetMenu(fileName = "ModulesSettingsContainer", menuName = "StaticData/Ship/Module/Create Modules Settings Container", order = 52)]
  public class ModulesSettingsContainer : SerializedScriptableObject  
  {
    [ValidateInput("ValidateDictionaryKeys", "Keys Not Dictionary Does Not Equal Weapon Type", IncludeChildren = true)]
    public Dictionary<ModuleType, ModuleSettings> Modules;
    
    private bool ValidateDictionaryKeys(Dictionary<ModuleType, ModuleSettings> modules)
    {
      if (modules == null || modules.Count == 0)
        return true;

      foreach (KeyValuePair<ModuleType,ModuleSettings> module in modules)
      {
        if (module.Key != module.Value.Type)
          return false;
      }

      return true;
    }
  }
}