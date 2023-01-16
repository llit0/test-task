using System;
using UniMob;
using UnityEngine;

[Serializable]
public class PlayerStatsValues : ILifetimeScope
{
    [Header("Current Values")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float damagePerSecond;
    [SerializeField] private int attackRadius;
    private int enemiesKilled;
    
    [Header("Start Values")]
    [SerializeField] private float startMovementSpeed;
    [SerializeField] private float startDamagePerSecond;
    [SerializeField] private int startAttackRadius;

    public float StartMovementSpeed => startMovementSpeed;
    public float StartDamagePerSecond => startDamagePerSecond;
    public int StartAttackRadius => startAttackRadius;
    
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
