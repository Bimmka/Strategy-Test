using Features.Bullet.Scripts.Element;
using UnityEngine;

namespace Features.Bullet.Data
{
  [CreateAssetMenu(fileName = "BulletSettings", menuName = "StaticData/Bullet/Create Bullets Settings", order = 52)]
  public class BulletSettings : ScriptableObject
  {
    public BulletType Type;
    public float MoveSpeed;
    public BulletView BulletView;
  }
}