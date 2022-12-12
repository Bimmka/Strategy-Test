using UnityEngine;

namespace Features.Ship.Scripts.Base
{
  public class ShipPresenter : MonoBehaviour
  {
    private ShipView view;
    private ShipModel model;

    public void Construct(ShipView shipView, ShipModel shipModel)
    {
      view = shipView;
      model = shipModel;
    }

    private void Update()
    {
      model.Tick(Time.deltaTime);
    }
  }
}