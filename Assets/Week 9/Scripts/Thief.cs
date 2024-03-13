using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : Villager
{
    public GameObject dagger;
    public Transform spawnPoint;
    public Transform secondSpawnPoint;
    public float dashDistance = 1f;

    protected override void Attack()
    {
        // This makes the Thief move towards the mouse by the amount set in dashDistance
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0;
        target = (transform.position - target).normalized * dashDistance;
        destination = transform.position - target;

        Instantiate(dagger, spawnPoint);
        Instantiate(dagger, secondSpawnPoint);
        base.Attack();
    }

    public override ChestType canOpen()
    {
        return ChestType.Thief;
    }
}
