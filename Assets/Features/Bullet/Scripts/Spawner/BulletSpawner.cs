using System.Collections.Generic;
using Features.Bullet.Data;
using Features.Bullet.Scripts.Element;
using Features.Bullet.Scripts.Factory;
using Features.Services.Cleanup;
using UnityEngine;

namespace Features.Bullet.Scripts.Spawner
{
  public class BulletSpawner : ICleanup
  {
    private readonly BulletFactory factory;
    private readonly Transform bulletSpawnParent;
    private readonly BulletsContainer bulletsContainer;
    private readonly Dictionary<BulletType, Queue<BulletPresenter>> bulletsPool;

    public BulletSpawner(BulletFactory factory, Transform bulletSpawnParent, BulletsContainer bulletsContainer, ICleanupService cleanupService)
    {
      this.factory = factory;
      this.bulletSpawnParent = bulletSpawnParent;
      this.bulletsContainer = bulletsContainer;
      cleanupService.Register(this);
      bulletsPool = new Dictionary<BulletType, Queue<BulletPresenter>>(4);
    }

    public void Cleanup()
    {
      foreach (KeyValuePair<BulletType,Queue<BulletPresenter>> bulletPool in bulletsPool)
      {
        foreach (BulletPresenter bullet in bulletPool.Value)
        {
          bullet.Hidden -= OnBulletHide;
          bullet.Cleanup();
        }
      }
      
      bulletsPool.Clear();
    }

    public BulletPresenter Create(BulletType type)
    {
      BulletPresenter bulletPresenter;
      if (IsHaveBulletInPool(type))
        bulletPresenter = BulletFromPool(type);
      else
        bulletPresenter = CreateFromFactory(type);
      
      return bulletPresenter;
    }

    private BulletPresenter CreateFromFactory(BulletType type)
    {
      BulletPresenter bulletPresenter = factory.Create(bulletsContainer.Settings[type], bulletsContainer.Prefab, bulletSpawnParent);
      bulletPresenter.Hidden += OnBulletHide;
      return bulletPresenter;
    }

    private BulletPresenter BulletFromPool(BulletType type) => 
      bulletsPool[type].Dequeue();

    private bool IsHaveBulletInPool(BulletType type) => 
      bulletsPool.ContainsKey(type) && bulletsPool[type].Count > 0;

    private void OnBulletHide(BulletPresenter bulletPresenter)
    {
      bulletPresenter.Hide();
      if (bulletsPool.ContainsKey(bulletPresenter.Type) == false)
        bulletsPool.Add(bulletPresenter.Type, new Queue<BulletPresenter>(10));
      
      bulletsPool[bulletPresenter.Type].Enqueue(bulletPresenter);
    }
  }
}