using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Villager
{
    public GameObject arrow;
    public Transform spawnPoint;

    protected override void Attack()
    {
        destination = transform.position;
        Instantiate(arrow, spawnPoint.position, spawnPoint.rotation);
        base.Attack();
    }
    public override ChestType canOpen()
    {
        return ChestType.Archer;
    }
    public override string ToString()
    {
        return "This is an archer";
    }
}
