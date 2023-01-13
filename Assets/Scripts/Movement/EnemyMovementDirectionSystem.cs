using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EnemyMovementDirectionSystem))]
public sealed class EnemyMovementDirectionSystem : FixedUpdateSystem
{
    public override void OnAwake()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
        Entity playerEntity = World.Filter.With<Player>().First();
        Position playerPos = playerEntity.GetComponent<Position>();

        Filter enemyFilter = World.Filter.With<Enemy>();
        foreach (Entity enemy in enemyFilter)
        {
            ref Movement enemyMovement = ref enemy.GetComponent<Movement>();
            ref Position enemyPos = ref enemy.GetComponent<Position>();
            enemyMovement.movementVector = (playerPos.coordinates - enemyPos.coordinates).normalized;
        }
    }
}