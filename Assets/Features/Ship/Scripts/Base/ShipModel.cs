using Features.Ship.Data.InputBindings;
using Features.Ship.Scripts.Damage;
using Features.Ship.Scripts.Health;
using Features.Ship.Scripts.Input.Scripts;
using Features.Ship.Scripts.Modules.Scripts.Container;
using Features.Ship.Scripts.Move.Scripts;
using Features.Ship.Scripts.Shield;
using Features.Ship.Scripts.Weapons.Container;
using UnityEngine;

namespace Features.Ship.Scripts.Base
{
  public class ShipModel
  {
    private readonly ShipHealth health;
    private readonly ShipShield shield;
    private readonly ShipDamageReceiver damageReceiver;
    private readonly ShipInput input;
    private readonly ShipMove move;
    private readonly ShipWeapons weapons;
    private readonly ShipModules modules;
    
    public PlayerType PlayerType { get; private set; }

    public ShipModel(ShipHealth health, ShipShield shield, ShipDamageReceiver damageReceiver, ShipInput input, 
      ShipMove move, ShipWeapons weapons, ShipModules modules, PlayerType playerType)
    {
      this.health = health;
      this.shield = shield;
      this.health.Dead += OnDead;
      this.damageReceiver = damageReceiver;
      this.input = input;
      this.move = move;
      this.weapons = weapons;
      this.modules = modules;
      PlayerType = playerType;
    }

    public void Cleanup()
    {
      health.Dead -= OnDead;
      shield.Cleanup();
      weapons.Cleanup();
    }

    public void Tick(float deltaTime)
    {
      Vector2Int moveDirection = input.MoveDirection();
      if (moveDirection != Vector2Int.zero)
        move.Move(moveDirection, deltaTime);
      
      shield.Tick(deltaTime);
      weapons.Tick(deltaTime);
    }

    public void TakeDamage(int damage) => 
      damageReceiver.TakeDamage(damage);

    private void OnDead()
    {
      Debug.Log("Dead");  
    }
  }
}