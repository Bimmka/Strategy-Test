using Features.Ship.Scripts.Base;

namespace Features.Ship.Scripts.Disable
{
  public class ShipDestroyer
  {
    private readonly ShipPresenter shipPresenter;

    public ShipDestroyer(ShipPresenter shipPresenter)
    {
      this.shipPresenter = shipPresenter;
    }

    public void Destroy()
    {
      shipPresenter.Disable();
    }
  }
}