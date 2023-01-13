using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(InputSystem))]
public sealed class InputSystem : UpdateSystem
{
    public override void OnAwake()
    {
        
    }

    public override void OnUpdate(float deltaTime)
    {
        updatePlayerMovementVector();
    }

    private void updatePlayerMovementVector()
    {
        Filter playerFilter = World.Filter.With<Player>();
        if (playerFilter.IsEmpty())
            return;
        
        ref Movement playerMovement = ref playerFilter.First().GetComponent<Movement>();
        playerMovement.movementVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.DownArrow))
            playerMovement.movementVector += new Vector3(0, 0, -1);
        if (Input.GetKey(KeyCode.UpArrow))
            playerMovement.movementVector += new Vector3(0, 0, 1);
        if (Input.GetKey(KeyCode.RightArrow))
            playerMovement.movementVector += new Vector3(1, 0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
            playerMovement.movementVector += new Vector3(-1, 0, 0);
        
        playerMovement.movementVector.Normalize();
    }
}