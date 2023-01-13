using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct EnemyCreationConstants : IComponent
{
    public int maxNumberOfEnemies;
    public float strongEnemySpawnChance;
    public float mediumEnemySpawnChance;

    public float weakEnemyHealth;
    public float mediumEnemyHealth;
    public float strongEnemyHealth;
}