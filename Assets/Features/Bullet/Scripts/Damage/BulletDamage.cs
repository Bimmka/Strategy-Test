using Features.Ship.Scripts.Base;

namespace Features.Bullet.Scripts.Damage
{
  public class BulletDamage
  {
    private int damage;

    public void SetDamage(int newDamage)
    {
      damage = newDamage;
    }

    public void ApplyDamage(ShipPresenter presenter)
    {
      presenter.TakeDamage(damage);
    }
  }
}