using Features.Ship.Scripts.Base;

namespace Features.Ship.Scripts.Spawn
{
  public readonly struct SpawnedPlayersContainer
  {
    public readonly ShipPresenter FirstPlayer;
    public readonly ShipPresenter SecondPlayer;

    public SpawnedPlayersContainer(ShipPresenter firstPlayer, ShipPresenter secondPlayer)
    {
      FirstPlayer = firstPlayer;
      SecondPlayer = secondPlayer;
    }
  }
}