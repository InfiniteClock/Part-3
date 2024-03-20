using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : Villager
{
    public GameObject dagger;
    public Transform spawnPoint;
    public Transform secondSpawnPoint;
    public float dashDistance = 1f;
    public float dashSpeed = 7f;
    Coroutine dashing;
    protected override void Attack()
    {
        if (dashing != null)
        {
            StopCoroutine(dashing);
        }
        dashing = StartCoroutine(Dash());

    }
    private IEnumerator Dash()
    {
        // This makes the Thief move towards the mouse by the amount set in dashDistance
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0;
        target = (transform.position - target).normalized * dashDistance;
        destination = transform.position - target;

        speed = dashSpeed;

        while (speed > 3f)
        {
            yield return null;
        }

        base.Attack();
        yield return new WaitForSeconds(0.1f);
        Instantiate(dagger, spawnPoint.position, spawnPoint.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(dagger, secondSpawnPoint.position, spawnPoint.rotation);
    }

    public override ChestType canOpen()
    {
        return ChestType.Thief;
    }
    public override string ToString()
    {
        return "This is a thief";
    }
    
}
