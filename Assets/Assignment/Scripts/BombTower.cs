using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTower : Tower
{
    public float explosionRad;
    protected override void Attack()
    {
        if (enemiesInRange.Count <= 0) { return; }
        if (target == null) { target = enemiesInRange[0]; }
        foreach (GameObject enemy in enemiesInRange)
        {
            if (enemy.GetComponent<Enemy>().lifeTime > target.GetComponent<Enemy>().lifeTime)
            {
                target = enemy;
            }
        }
        GameObject temp = Instantiate(projectile, transform.position, Quaternion.identity);
        temp.GetComponent<Projectile>().target = target;
        temp.GetComponent<Projectile>().damage = damage;
        temp.GetComponent<Projectile>().radius = explosionRad;
    }
    protected override void UpgradeTower()
    {
        cost += (int)(cost / 2f);
        if (level % 2 == 1)
        {
            fireSpeed -= (int)(fireSpeed / 20f);
        }
        damage += 1;
        radius += 0.5f;
        explosionRad += 0.5f;
        level += 1;
    }
}
