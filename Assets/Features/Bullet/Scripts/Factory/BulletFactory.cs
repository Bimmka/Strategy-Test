using Features.Bullet.Data;
using Features.Bullet.Scripts.Damage;
using Features.Bullet.Scripts.Element;
using Features.Bullet.Scripts.Move;
using Features.Services.Assets;
using UnityEngine;

namespace Features.Bullet.Scripts.Factory
{
  public class BulletFactory
  {
    private readonly IAssetProvider assetProvider;

    public BulletFactory(IAssetProvider assetProvider)
    {
      this.assetProvider = assetProvider;
    }
    
    public BulletPresenter Create(BulletSettings settings, BulletPresenter prefab, Transform spawnParent)
    {
      BulletPresenter presenter = assetProvider.Instantiate(prefab, spawnParent);
      BulletView view = assetProvider.Instantiate(settings.BulletView, presenter.transform);
      BulletMove move = new BulletMove(settings.MoveSpeed, presenter.Rigidbody);
      BulletDamage damage = new BulletDamage();
      BulletModel model = new BulletModel(settings.Type, move, damage);
      
      presenter.Construct(model, view);
      return presenter;
    }
  }
}