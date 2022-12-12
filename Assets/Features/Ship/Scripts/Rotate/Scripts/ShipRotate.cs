using Features.Ship.Scripts.Rotate.Data;
using UnityEngine;

namespace Features.Ship.Scripts.Rotate.Scripts
{
  public class ShipRotate
  {
    private readonly Transform ship;
    private readonly ShipRotateSettings rotateData;

    public ShipRotate(Transform ship, ShipRotateSettings rotateData)
    {
      this.ship = ship;
      this.rotateData = rotateData;
    }
    public void Rotate(Vector2 moveDirection)
    {
      float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
      Quaternion rotation = Quaternion.Euler(0,0,angle);
      Quaternion lerpedRotation = Quaternion.Slerp(ship.rotation, rotation,  rotateData.LerpRotate);
      ship.rotation = lerpedRotation;
    }
  }
}