using UnityEngine;

namespace Features.Ship.Scripts.Rotate.Data
{
  [CreateAssetMenu(fileName = "ShipRotateSettings", menuName = "StaticData/Ship/Create Rotate Settings", order = 52)]
  public class ShipRotateSettings : ScriptableObject
  {
    public float LerpRotate = 0.01f;
  }
}