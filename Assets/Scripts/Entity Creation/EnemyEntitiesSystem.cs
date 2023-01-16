using System;
using System.Linq;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Random = UnityEngine.Random;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EnemyEntitiesSystem))]
public sealed class EnemyEntitiesSystem : UpdateSystem
{
    public override void OnAwake()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
        cleanUpDeadEnemies();
        createEnemies();
    }

    private void createEnemies()
    {
        Filter filter = World.Filter.With<EnemyCreationConstants>();
        ref EnemyCreationConstants constants = ref filter.First().GetComponent<EnemyCreationConstants>();

        int numOfEnemies = World.Filter.With<Enemy>().Count();
        for (int i = 0; i < constants.maxNumberOfEnemies - numOfEnemies; i++)
        {
            if (Random.value < constants.strongEnemySpawnChance)
                createEnemyEntity(Enemy.Type.Strong, constants);
            else if (Random.value < constants.mediumEnemySpawnChance)
                createEnemyEntity(Enemy.Type.Medium, constants);
            else
                createEnemyEntity(Enemy.Type.Weak, constants);
        }
    }

    private void cleanUpDeadEnemies()
    {
        Filter filter = World.Filter.With<Enemy>();
        foreach (Entity enemy in filter)
            if (enemy.GetComponent<Health>().hp <= 0f)
            {
                enemy.Dispose();
                incrementPlayerKillCount();
            }
    }

    private void incrementPlayerKillCount()
    {
        World.Filter.With<Player>().First().GetComponent<Player>().stats.addKill();
    }

    private void createEnemyEntity(Enemy.Type type, EnemyCreationConstants constants)
    {
        Entity enemy = World.CreateEntity();
        enemy.AddComponent<Movement>();

        ref Position enemyPosition = ref enemy.AddComponent<Position>();
        enemyPosition.coordinates = new Vector3(Random.Range(-20, 20), 0f, Random.Range(-20, 20));

        ref Health enemyHp = ref enemy.AddComponent<Health>();
        enemyHp.hp = type switch
        {
            Enemy.Type.Weak => constants.weakEnemyHealth,
            Enemy.Type.Medium => constants.mediumEnemyHealth,
            Enemy.Type.Strong => constants.strongEnemyHealth,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        ref Enemy enemyComponent = ref enemy.AddComponent<Enemy>();
        enemyComponent.type = type;
    }
}