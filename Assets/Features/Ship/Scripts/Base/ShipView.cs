using Features.Ship.Scripts.Weapons.Marker;
using UnityEngine;

namespace Features.Ship.Scripts.Base
{
  public class ShipView : MonoBehaviour
  {
    [field: SerializeField] public ShipFirePointMarker[] FirePointMarkers { get; private set; }
  }
}