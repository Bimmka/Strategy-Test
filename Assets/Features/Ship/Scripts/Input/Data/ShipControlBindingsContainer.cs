using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Ship.Data.InputBindings
{
  [CreateAssetMenu(fileName = "ShipControlBindingsContainer", menuName = "StaticData/Ship/Input/Create Inputs Container", order = 52)]
  public class ShipControlBindingsContainer : SerializedScriptableObject
  {
    [ValidateInput("ValidateDictionaryKeys", "Keys Not Dictionary Does Not Equal Weapon Type", IncludeChildren = true)]
    public Dictionary<PlayerType, ShipControlBindings> Bindings;
    
    
    private bool ValidateDictionaryKeys(Dictionary<PlayerType, ShipControlBindings> bindings)
    {
      if (bindings == null || bindings.Count == 0)
        return true;

      foreach (KeyValuePair<PlayerType,ShipControlBindings> binding in bindings)
      {
        if (binding.Key != binding.Value.Type)
          return false;
      }

      return true;
    }
  }
}