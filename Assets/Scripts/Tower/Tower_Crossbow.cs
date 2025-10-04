using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Crossbow : Tower
{
    [Header("Crossbow Details")]
    [SerializeField] private int damage;
    [SerializeField] private Transform gunPoint;

    private Crossbow_Visuals visuals;

    protected override void Awake()
    {
        base.Awake();

        visuals = GetComponent<Crossbow_Visuals>();
    }

    protected override void Attack()
    {
        Vector3 directionToEnemy = DirectionToEnemyFrom(gunPoint);

        if (Physics.Raycast(gunPoint.position, directionToEnemy, out RaycastHit hitInfo, Mathf.Infinity))
        {
            towerHead.forward = directionToEnemy;

            Enemy enemyTarget = null;
            IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                enemyTarget = currentEnemy;
            }

            visuals.PlayAttackVFX(gunPoint.position, hitInfo.point, enemyTarget);
            visuals.PlayReloadVFX(attackCooldown);
        }
    }
}
