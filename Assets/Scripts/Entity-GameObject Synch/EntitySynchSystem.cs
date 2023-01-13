using System.Collections.Generic;
using System.Linq;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EntitySynchSystem))]

//synchronizes gameobjects with their corresponding entities
public sealed class EntitySynchSystem : UpdateSystem
{
    public override void OnAwake()
    {
        initExistingObjectsComponent();
    }

    public override void OnUpdate(float deltaTime)
    {
        updatePlayerGameObject();
        updateEnemyGameObjects();
    }

    private void updatePlayerGameObject()
    {
        ref ExistingObjectsData existingObjects = ref getExistingObjectsData();

        if (!existingObjects.playerObject)
        {
            ref ObjectsData objectsData = ref getObjectsData();
            existingObjects.playerObject = Instantiate(objectsData.playerPrefab);
        }
        else
        {
            Filter filter = World.Filter.With<Player>();
            existingObjects.playerObject.transform.position = filter.First().GetComponent<Position>().coordinates;
        }
    }

    private void updateEnemyGameObjects()
    {
        cleanUpDeadEnemyObjects();
        updateExistingEnemyObjects();
    }

    private void updateExistingEnemyObjects()
    {
        Filter filter = World.Filter.With<Enemy>();
        ref ExistingObjectsData existingObjects = ref getExistingObjectsData();
        foreach (Entity enemy in filter)
        {
            if (!existingObjects.enemyObjects.TryGetValue(enemy, out GameObject enemyObject))
            {
                ref ObjectsData objectsData = ref getObjectsData();
                enemyObject = Instantiate(objectsData.enemyPrefabs[(int)enemy.GetComponent<Enemy>().type]);
                existingObjects.enemyObjects.Add(enemy, enemyObject);
            }

            enemyObject.transform.position = enemy.GetComponent<Position>().coordinates;
        }
    }

    private void cleanUpDeadEnemyObjects()
    {
        ref ExistingObjectsData existingObjects = ref getExistingObjectsData();

        //finding all pairs with disposed entities
        List<KeyValuePair<Entity, GameObject>> toRemove = existingObjects.enemyObjects
            .Where(pair => pair.Key.IsDisposed()).ToList();
        
        //destroying gameobjects that correspond to disposed entities
        toRemove.Select(pair => pair.Value).ToList().ForEach(Destroy);
        
        foreach (KeyValuePair<Entity, GameObject> pair in toRemove)
            existingObjects.enemyObjects.Remove(pair.Key);
    }

    private ref ExistingObjectsData getExistingObjectsData()
    {
        return ref World.Filter.With<ExistingObjectsData>().First().GetComponent<ExistingObjectsData>();
    }

    private ref ObjectsData getObjectsData()
    {
        return ref World.Filter.With<ObjectsData>().First().GetComponent<ObjectsData>();
    }

    private void initExistingObjectsComponent()
    {
        getExistingObjectsData().enemyObjects = new Dictionary<Entity, GameObject>();
    }
}