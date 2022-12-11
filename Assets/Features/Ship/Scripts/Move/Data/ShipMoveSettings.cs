using UnityEngine;

namespace Features.Ship.Scripts.Move.Data
{
  [CreateAssetMenu(fileName = "ShipMoveSettings", menuName = "StaticData/Ship/Create Move Settings", order = 52)]
  public class ShipMoveSettings : ScriptableObject
  {
    public float MoveSpeed;
  }
}