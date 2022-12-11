using UnityEngine;

namespace Features.Ship.Data.InputBindings
{
  [CreateAssetMenu(fileName = "ShipControlBindings", menuName = "StaticData/Ship/Create Ship Control Bindings", order = 52)]
  public class ShipControlBindings : ScriptableObject
  {
    public KeyCode MoveForwardKey;
    public KeyCode MoveRightKey;
    public KeyCode MoveLeftKey;
    public KeyCode MoveDownKey;
  }
}