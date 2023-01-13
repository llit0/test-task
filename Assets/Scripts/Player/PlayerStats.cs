using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerStatsValues values;
    public PlayerStatsValues Values => values;

    public void upgrade()
    {
        PlayerStatsChange statChangeData = GetComponent<PlayerStatsChange>();
        
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
