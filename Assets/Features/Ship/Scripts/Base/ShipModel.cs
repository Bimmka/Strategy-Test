using Features.Ship.Data.InputBindings;
using Features.Ship.Scripts.Input.Scripts;
using Features.Ship.Scripts.Modules.Scripts.Container;
using Features.Ship.Scripts.Move.Scripts;
using Features.Ship.Scripts.Weapons.Container;
using UnityEngine;

namespace Features.Ship.Scripts.Base
{
  public class ShipModel
  {
    private readonly ShipInput input;
    private readonly ShipMove move;
    private readonly ShipWeapons weapons;
    private readonly ShipModules modules;
    
    public PlayerType PlayerType { get; private set; }

    public ShipModel(ShipInput input, ShipMove move, ShipWeapons weapons, ShipModules modules, PlayerType playerType)
    {
      this.input = input;
      this.move = move;
      this.weapons = weapons;
      this.modules = modules;
      PlayerType = playerType;
    }
    
    public void Tick(float deltaTime)
    {
      Vector2Int moveDirection = input.MoveDirection();
      if (moveDirection != Vector2Int.zero)
        move.Move(moveDirection, deltaTime);
      weapons.Tick(deltaTime);
    }
  }
}