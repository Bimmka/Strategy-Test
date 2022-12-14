using Features.Extensions;
using Features.Ship.Scripts.Move.Data;
using Features.Ship.Scripts.Rotate.Scripts;
using UnityEngine;

namespace Features.Ship.Scripts.Move.Scripts
{
  public class ShipMove
  {
    private readonly Transform ship;
    private readonly ShipMoveSettings moveData;
    private readonly ShipRotate rotate;
    private readonly Rigidbody shipBody;

    public ShipMove(Transform ship, ShipMoveSettings moveData, ShipRotate rotate, Rigidbody shipBody)
    {
      this.ship = ship;
      this.moveData = moveData;
      this.rotate = rotate;
      this.shipBody = shipBody;
    }

    public void Move(Vector2 direction, float deltaTime)
    {
      Vector3 moveDirection = MoveDirection(direction);
      if (ship.right.IsEqualMoveDirection(moveDirection) == false)
        rotate.Rotate(direction);

      shipBody.MovePosition(shipBody.position + moveDirection * (moveData.MoveSpeed * deltaTime));
    }

    private Vector3 MoveDirection(Vector2 inputDirection)
    {
      Vector3 worldMoveVector = Vector3.zero;
      if (inputDirection.x != 0)
        worldMoveVector.x = inputDirection.x;

      if (inputDirection.y != 0)
        worldMoveVector.y = inputDirection.y;

      worldMoveVector.z = 0;
      return worldMoveVector.normalized;
    }
  }
}