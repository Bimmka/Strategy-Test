using Features.Extensions;
using Features.Ship.Scripts.Move.Data;
using Features.Ship.Scripts.Rotate.Scripts;
using UnityEngine;

namespace Features.Ship.Scripts.Move.Scripts
{
  public class ShipMove
  {
    private readonly Transform heroTransform;
    private readonly ShipMoveSettings moveData;
    private readonly Transform camera;
    private readonly ShipRotate rotate;
    private readonly CharacterController heroController;

    public ShipMove(Transform heroTransform, ShipMoveSettings moveData, Transform camera, ShipRotate rotate, CharacterController heroController)
    {
      this.heroTransform = heroTransform;
      this.moveData = moveData;
      this.camera = camera;
      this.rotate = rotate;
      this.heroController = heroController;
    }

    public void Move(Vector2 direction, float deltaTime)
    {
      Vector3 moveDirection = MoveDirection(direction);
      if (heroTransform.forward.IsEqualMoveDirection(moveDirection) == false)
        rotate.Rotate(moveDirection);

      heroController.Move(moveDirection * (moveData.MoveSpeed * deltaTime));
    }

    private Vector3 MoveDirection(Vector2 inputDirection)
    {
      Vector3 worldMoveVector = Vector3.zero;
      if (inputDirection.x != 0)
        worldMoveVector += camera.transform.right * inputDirection.x;

      if (inputDirection.y != 0)
        worldMoveVector += camera.transform.forward * inputDirection.y;

      worldMoveVector.y = 0;
      return worldMoveVector.normalized;
    }
  }
}