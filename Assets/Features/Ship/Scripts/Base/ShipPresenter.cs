using System;
using Features.Services.Cleanup;
using Features.Ship.Data.InputBindings;
using UnityEngine;

namespace Features.Ship.Scripts.Base
{
  public class ShipPresenter : MonoBehaviour
  {
    private ShipView view;
    private ShipModel model;

    private bool isEnable;

    public PlayerType PlayerType => model.PlayerType;

    public event Action Disabled;

    public void Construct(ShipView shipView, ShipModel shipModel)
    {
      view = shipView;
      model = shipModel;
      isEnable = true;
    }

    private void Update()
    {
      if (isEnable)
        model.Tick(Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
      if (isEnable)
        model.TakeDamage(damage);
    }

    public void Disable()
    {
      view.Disable();
      Disabled?.Invoke();
    }

    public void Stop() => 
      isEnable = false;
  }
}