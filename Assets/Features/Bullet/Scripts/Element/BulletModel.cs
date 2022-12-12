using System;
using Features.Bullet.Data;
using Features.Bullet.Scripts.Damage;
using Features.Bullet.Scripts.Move;
using Features.Ship.Data.InputBindings;
using Features.Ship.Scripts.Base;

namespace Features.Bullet.Scripts.Element
{
  public class BulletModel
  {
    private readonly BulletMove move;
    private readonly BulletDamage damage;
    
    private PlayerType currentOwner;
    
    public BulletType Type { get; }

    public event Action Hiden;

    public BulletModel(BulletType type, BulletMove move, BulletDamage damage)
    {
      this.move = move;
      this.damage = damage;
      Type = type;
    }

    public void Tick(float deltaTime)
    {
      move.Move(deltaTime);
    }

    public void OnPlayerCollision(ShipPresenter shipPresenter)
    {
      if (shipPresenter.PlayerType != currentOwner)
      {
        damage.ApplyDamage(shipPresenter);
        NotifyAboutDisable();
      }
    }

    public void OnWallCollision() => 
      NotifyAboutDisable();

    public void UpdateOwner(PlayerType owner) => 
      currentOwner = owner;

    public void UpdateDamage(int damageCount) => 
      damage.SetDamage(damageCount);

    private void NotifyAboutDisable() => 
      Hiden?.Invoke();
  }
}