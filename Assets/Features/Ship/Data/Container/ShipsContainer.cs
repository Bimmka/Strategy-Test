using System.Collections.Generic;
using Features.Ship.Data.Settings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Ship.Data.Container
{
  [CreateAssetMenu(fileName = "ShipsContainer", menuName = "StaticData/Ship/Create Ships Container", order = 52)]
  public class ShipsContainer : SerializedScriptableObject
  {
    public Dictionary<ShipPlayerType, ShipSettings> Ships;
  }
}