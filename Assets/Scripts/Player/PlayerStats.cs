using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "PlayerStatsScriptableObject", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private PlayerStatsValues values;
    public PlayerStatsValues Values => values;
    
    [SerializeField] private PlayerStatsChange statChangeData;
    
    public void upgrade()
    {
        if (Random.value < statChangeData.attackRadiusUpgradeChance)
            values.AttackRadius += statChangeData.attackRadiusChange;
        else if (Random.value < statChangeData.damagePerSecondUpgradeChance)
            values.DamagePerSecond += statChangeData.damagePerSecondChange;
        else if (Random.value < statChangeData.movementSpeedUpgradeChance)
            values.MovementSpeed += statChangeData.movementSpeedChange;
    }

    public void addKill()
    {
        values.EnemiesKilled++;
    }
}
