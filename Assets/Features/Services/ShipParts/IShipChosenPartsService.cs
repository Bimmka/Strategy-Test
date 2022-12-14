namespace Features.Services.ShipParts
{
  public interface IShipChosenPartsService
  {
    ShipChosenParts FirstPlayerParts { get; }
    ShipChosenParts SecondPlayerParts { get; }
    void SetFirstPlayerParts(ShipChosenParts chosenParts);
    void SetSecondPlayerParts(ShipChosenParts chosenParts);
  }
}