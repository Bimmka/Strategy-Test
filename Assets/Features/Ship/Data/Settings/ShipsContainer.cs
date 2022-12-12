using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Ship.Data.Settings
{
  [CreateAssetMenu(fileName = "ShipsContainer", menuName = "StaticData/Ship/Settings/Create Ships Container", order = 52)]
  public class ShipsContainer : SerializedScriptableObject
  {
    [ValidateInput("ValidateDictionaryKeys", "Dictionary Keys Does Not Equal Ship Key", IncludeChildren = true)]
    public Dictionary<ShipType, ShipSettings> Ships;

    private bool ValidateDictionaryKeys(Dictionary<ShipType, ShipSettings> ships)
    {
      if (ships == null || ships.Count == 0)
        return true;

      foreach (KeyValuePair<ShipType,ShipSettings> ship in ships)
      {
        if (ship.Key != ship.Value.Type)
          return false;
      }

      return true;
    }
  }
}