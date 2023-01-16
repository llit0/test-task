using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsChangeScriptableObject", menuName = "ScriptableObjects/PlayerStatsChange")]
public class PlayerStatsChange : ScriptableObject
{
    [Header("Value Changes")]
    public float movementSpeedChange = 0.5f;
    public int damagePerSecondChange = 10;
    public int attackRadiusChange = 1;
    [Header("Upgrade Chances")]
    public float movementSpeedUpgradeChance = 0.6f;
    public float damagePerSecondUpgradeChance = 0.3f;
    public float attackRadiusUpgradeChance = 0.1f;
}
