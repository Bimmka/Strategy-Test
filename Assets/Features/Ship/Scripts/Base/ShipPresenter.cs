using System;
using Features.Ship.Data.InputBindings;
using UnityEngine;

namespace Features.Ship.Scripts.Base
{
  public class ShipPresenter : MonoBehaviour
  {
    private ShipView view;
    private ShipModel model;

    public PlayerType PlayerType => model.PlayerType;

    public void Construct(ShipView shipView, ShipModel shipModel)
    {
      view = shipView;
      model = shipModel;
    }

    private void OnDestroy() => 
      model.Cleanup();

    private void Update() => 
      model.Tick(Time.deltaTime);

    public void TakeDamage(int damage) => 
      model.TakeDamage(damage);
  }
}