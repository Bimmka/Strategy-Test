using UnityEngine;

namespace Features.Level.Data
{
  [CreateAssetMenu(fileName = "LevelSettings", menuName = "StaticData/Level/Create Level Settings", order = 52)]
  public class LevelSettings : ScriptableObject
  {
    public Vector3 FirstPlayerSpawnPosition;
    public Vector3 FirstPlayerSpawnRotation;
    public Vector3 SecondPlayerSpawnPosition;
    public Vector3 SecondPlayerSpawnRotation;
  }
}