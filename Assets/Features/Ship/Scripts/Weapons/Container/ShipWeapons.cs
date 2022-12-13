using Features.Ship.Scripts.Weapons.Elements;

namespace Features.Ship.Scripts.Weapons.Container
{
  public class ShipWeapons
  {
    private readonly Weapon[] weapons;

    public ShipWeapons(Weapon[] weapons)
    {
      this.weapons = weapons;
    }
    
    public void Tick(float deltaTime)
    {
      for (int i = 0; i < weapons.Length; i++)
      {
        weapons[i].Tick(deltaTime);
      }  
    }

    public void Cleanup()
    {
      for (int i = 0; i < weapons.Length; i++)
      {
        weapons[i].Cleanup();
      }
    }
  }
}