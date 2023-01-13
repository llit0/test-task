using System;
using UniMob;
using UnityEngine;

[Serializable]
public class PlayerStatsValues : ILifetimeScope
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float damagePerSecond;
    [SerializeField] private int attackRadius;
    private int enemiesKilled;
    
    [Atom] public float MovementSpeed
    {
        get => movementSpeed;
        set => movementSpeed = value;
    }

    [Atom] public float DamagePerSecond
    {
        get => damagePerSecond;
        set => damagePerSecond = value;
    }

    [Atom] public int AttackRadius
    {
        get => attackRadius;
        set => attackRadius = value;
    }
    
    [Atom] public int EnemiesKilled
    {
        get => enemiesKilled;
        set => enemiesKilled = value;
    }
    
    public int maxAttackTargets;

    public Lifetime Lifetime { get; }
}
