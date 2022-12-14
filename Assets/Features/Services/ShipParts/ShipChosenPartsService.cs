namespace Features.Services.ShipParts
{
  public class ShipChosenPartsService : IShipChosenPartsService
  {
    public ShipChosenParts FirstPlayerParts { get; private set; }
    public ShipChosenParts SecondPlayerParts { get; private set; }

    public void SetFirstPlayerParts(ShipChosenParts chosenParts) => 
      FirstPlayerParts = chosenParts;

    public void SetSecondPlayerParts(ShipChosenParts chosenParts) => 
      SecondPlayerParts = chosenParts;
  }
}