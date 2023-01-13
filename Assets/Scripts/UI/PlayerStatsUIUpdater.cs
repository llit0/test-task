using System.Collections;
using Scellecs.Morpeh;
using TMPro;
using UniMob;
using UnityEngine;

public class PlayerStatsUIUpdater : MonoBehaviour, ILifetimeScope
{
    [SerializeField] private TextMeshProUGUI enemiesKilled;
    [SerializeField] private TextMeshProUGUI attackRadius;
    [SerializeField] private TextMeshProUGUI dps;
    [SerializeField] private TextMeshProUGUI movementSpeed;

    private PlayerStats playerStats;
    
    private void Awake()
    {
        StartCoroutine(init());
    }

    private IEnumerator init()
    {
        //bad solution!
        yield return new WaitForEndOfFrame(); //needed to wait for the player gameobject to load
        
        Player data = World.Default!.Filter.With<Player>().First().GetComponent<Player>();
        playerStats = data.playerObject.GetComponent<PlayerStats>();
        Atom.Reaction(Lifetime, updateUI);
        
    }

    private void updateUI()
    {
        enemiesKilled.text = "Enemies killed: " + playerStats.Values.EnemiesKilled;
        attackRadius.text = "AR: " + playerStats.Values.AttackRadius;
        dps.text = "DPS: " + playerStats.Values.DamagePerSecond;
        movementSpeed.text = "MS: " + playerStats.Values.MovementSpeed;
    }
    
    public Lifetime Lifetime { get; } = Lifetime.Eternal;
}
