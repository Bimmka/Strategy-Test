using Features.Ship.Data.InputBindings;
using UnityEngine;

namespace Features.Ship.Scripts.Input.Scripts
{
  public class ShipInput
  {
    private readonly ShipControlBindings bindings;

    public ShipInput(ShipControlBindings bindings)
    {
      this.bindings = bindings;
    }

    public Vector2Int MoveDirection()
    {
      Vector2Int modeDirection = Vector2Int.zero;

      if (UnityEngine.Input.GetKey(bindings.MoveForwardKey))
        modeDirection.y += 1;
      
      if (UnityEngine.Input.GetKey(bindings.MoveDownKey))
        modeDirection.y -= 1;
      
      if (UnityEngine.Input.GetKey(bindings.MoveLeftKey))
        modeDirection.x -= 1;
      
      if (UnityEngine.Input.GetKey(bindings.MoveRightKey))
        modeDirection.x += 1;

      return modeDirection;
    }
  }
}