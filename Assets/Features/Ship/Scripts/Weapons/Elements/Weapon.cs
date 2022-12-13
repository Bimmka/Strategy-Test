using Features.Bullet.Data;
using Features.Bullet.Scripts.Element;
using Features.Bullet.Scripts.Spawner;
using Features.Reloader;
using Features.Ship.Data.InputBindings;
using Features.Ship.Scripts.Weapons.Data;
using UnityEngine;

namespace Features.Ship.Scripts.Weapons.Elements
{
  public abstract class Weapon
  {
    private readonly WeaponSettings settings;
    private readonly PlayerType playerType;
    private readonly Transform firePoint;
    private readonly BulletSpawner bulletSpawner;
    private readonly ItemReloader reloader;
    public Weapon(WeaponSettings settings, PlayerType playerType, Transform firePoint, BulletSpawner bulletSpawner)
    {
      this.settings = settings;
      this.playerType = playerType;
      this.firePoint = firePoint;
      this.bulletSpawner = bulletSpawner;
      reloader = new ItemReloader((int) settings.ShootCooldown);
      reloader.TimeOut += Shoot;
    }

    public void Cleanup() => 
      reloader.TimeOut -= Shoot;

    public void Tick(in float deltaTime) => 
      reloader.Tick(deltaTime);

    protected abstract BulletType Type();

    private void Shoot() => 
      CreateBullet();

    private void CreateBullet()
    {
      BulletPresenter bulletPresenter = bulletSpawner.Create(Type());
      InitializeBullet(bulletPresenter);
    }

    private void InitializeBullet(BulletPresenter bulletPresenter)
    {
      bulletPresenter.transform.position = firePoint.position;
      bulletPresenter.transform.right = firePoint.right;
      bulletPresenter.SetDamage(settings.Damage);
      bulletPresenter.SetOwner(playerType);
      bulletPresenter.Show();
      bulletPresenter.StartFly();
    }
  }
}