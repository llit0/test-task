using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(HealthSystem))]
public sealed class HealthSystem : UpdateSystem
{
    public override void OnAwake()
    {
        
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in World.Filter.With<Health>().With<IncomingDamage>())
        {
            ref Health healthComponent = ref entity.GetComponent<Health>();
            healthComponent.hp -= entity.GetComponent<IncomingDamage>().damage;
            entity.RemoveComponent<IncomingDamage>();
        }
    }
}