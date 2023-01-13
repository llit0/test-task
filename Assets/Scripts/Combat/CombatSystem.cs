using System.Collections.Generic;
using System.Linq;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(CombatSystem))]
public sealed class CombatSystem : FixedUpdateSystem
{
    public override void OnAwake()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
        playerAttack(deltaTime);
    }

    private void playerAttack(float deltaTime)
    {
        ExistingObjectsData data = World.Filter.With<ExistingObjectsData>().First().GetComponent<ExistingObjectsData>();
        PlayerStats playerStats = data.playerObject.GetComponent<PlayerStats>();

        Position playerPos = World.Filter.With<Player>().First().GetComponent<Position>();
        List<Entity> enemies = World.Filter.With<Enemy>().ToList();

        //to find the closest enemies and check whether they are inside the attack range or not
        float SqrMagnitude(Entity entity) => 
            (entity.GetComponent<Position>().coordinates - playerPos.coordinates).sqrMagnitude;

        enemies.Sort((enemy1, enemy2) => SqrMagnitude(enemy1).CompareTo(SqrMagnitude(enemy2)));
        for (int i = 0; i < playerStats.Values.maxAttackTargets; i++)
        {
            if(SqrMagnitude(enemies[i]) <= playerStats.Values.AttackRadius * playerStats.Values.AttackRadius)
                sendAttack(enemies[i], playerStats.Values.DamagePerSecond * deltaTime);
        }
    }

    private void sendAttack(Entity entity, float damage)
    {
        ref IncomingDamage dmg = ref entity.AddComponent<IncomingDamage>();
        dmg.damage = damage;
    }
}