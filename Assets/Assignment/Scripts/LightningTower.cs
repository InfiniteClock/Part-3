using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTower : Tower
{
    protected override void Attack()
    {
        if (enemiesInRange.Count <= 0) { return; }
        if (target == null) { target = enemiesInRange[0]; }
        foreach (GameObject enemy in enemiesInRange)
        {
            GameObject temp = Instantiate(projectile, transform.position, Quaternion.identity);
            temp.GetComponent<Projectile>().target = target;
            temp.GetComponent<Projectile>().damage = damage;
        }
    }
    protected override void UpgradeTower()
    {
        cost += (int)(cost / 2f);
        if (level % 2 == 1)
        {
            damage += 1;
        }
        fireSpeed -= (int)(fireSpeed / 20f);
        radius += 1f;
        level += 1;
    }
}
