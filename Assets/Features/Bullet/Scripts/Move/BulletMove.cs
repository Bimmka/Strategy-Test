using UnityEngine;

namespace Features.Bullet.Scripts.Move
{
  public class BulletMove
  {
    private readonly float moveSpeed;
    private readonly Rigidbody bullet;

    public BulletMove(float moveSpeed, Rigidbody bullet)
    {
      this.moveSpeed = moveSpeed;
      this.bullet = bullet;
    }

    public void Move(float deltaTime) =>
      bullet.MovePosition(bullet.position + bullet.transform.right * moveSpeed * deltaTime);
  }
}