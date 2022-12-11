using Features.Ship.Scripts.Factory;

namespace Features.Ship.Scripts.Spawn
{
  public class ShipSpawner
  {
    private readonly ShipFactory factory;

    public ShipSpawner(ShipFactory factory)
    {
      this.factory = factory;
    }
  }
}