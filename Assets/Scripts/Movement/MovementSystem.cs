using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MovementSystem))]
public sealed class MovementSystem : FixedUpdateSystem
{
    private Filter filter;
    
    public override void OnAwake()
    {
        
    }

    public override void OnUpdate(float deltaTime)
    {
        updatePlayer(deltaTime);
        updateEnemies(deltaTime);
    }

    private void updatePlayer(float deltaTime)
    {
        filter = World.Filter.With<Player>();
        ref Player player = ref filter.First().GetComponent<Player>();
        ref Position position = ref filter.First().GetComponent<Position>();
        float movementSpeed = player.playerObject.GetComponent<PlayerStats>().Values.MovementSpeed;
        position.coordinates += filter.First().GetComponent<Movement>().movementVector * movementSpeed * deltaTime;
        fitInMapBoundaries(ref position);
    }

    private void updateEnemies(float deltaTime)
    {
        filter = World.Filter.With<Movement>().With<Position>().With<Enemy>();
        foreach (Entity entity in filter)
        {
            ref Position position = ref entity.GetComponent<Position>();
            position.coordinates += entity.GetComponent<Movement>().movementVector * deltaTime;
            fitInMapBoundaries(ref position);
        }
    }

    private void fitInMapBoundaries(ref Position position)
    {
        const int MapWidth = 22;
        const int MapHeight = 12;

        if (position.coordinates.x > MapWidth)
            position.coordinates.x = MapWidth;
        if (position.coordinates.x < -MapWidth)
            position.coordinates.x = -MapWidth;
        if (position.coordinates.z > MapHeight)
            position.coordinates.z = MapHeight;
        if (position.coordinates.z < -MapHeight)
            position.coordinates.z = -MapHeight;
    }
    
}