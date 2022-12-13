using System;
using System.Collections;
using Features.Bullet.Data;
using Features.Ship.Data.InputBindings;
using Features.Ship.Scripts.Base;
using Features.Wall;
using UnityEngine;

namespace Features.Bullet.Scripts.Element
{
  public class BulletPresenter : MonoBehaviour
  {
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    [SerializeField] private Collider collisionCollider;
    
    private BulletModel model;
    private bool isEnable;
    private BulletView view;
    public BulletType Type => model.Type;

    public event Action<BulletPresenter> Hidden;

    public void Construct(BulletModel model, BulletView view)
    {
      this.view = view;
      this.model = model;
      this.model.Hiden += NotifyAboutHidden;
    }

    public void Cleanup() => 
      model.Hiden -= NotifyAboutHidden;

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out ShipPresenter shipPresenter))
        model.OnPlayerCollision(shipPresenter);
      else if (other.TryGetComponent(out WallMarker wall))
        model.OnWallCollision();
    }

    public void SetOwner(PlayerType owner) =>
      model.UpdateOwner(owner);

    public void SetDamage(int damageCount) => 
      model.UpdateDamage(damageCount);

    public void Show()
    {
      view.Show();
      isEnable = true;
      collisionCollider.enabled = true;
    }

    public void Hide()
    {
      view.Hide();
      isEnable = false;
      collisionCollider.enabled = false;
      StopAllCoroutines();
    }

    public void StartFly() => 
      StartCoroutine(Fly());

    private IEnumerator Fly()
    {
      while (isEnable)
      {
        model.Tick(Time.deltaTime);
        yield return null;
      }
    }

    private void NotifyAboutHidden() => 
      Hidden?.Invoke(this);
  }
}