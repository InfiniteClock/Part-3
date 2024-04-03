using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tower : MonoBehaviour
{
    public GameObject projectile;
    CircleCollider2D detectionCollider;
    [SerializeField] protected List<GameObject> enemiesInRange;
    public float fireSpeed;
    public int damage;
    public float radius;
    public int cost;

    protected GameObject target;
    public int level = 1;
    private float timer;
    private void Start()
    {
        detectionCollider = GetComponent<CircleCollider2D>();
        detectionCollider.radius = radius;
        cost += (int)(cost / 2f);
    }

    private void Update()
    {
        if (timer < fireSpeed) { timer += Time.deltaTime; }
        if (enemiesInRange != null)
        {
            if (timer >= fireSpeed)
            {
                timer = 0;
                Attack();
            }
        }
    }

    protected virtual void Attack()
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            enemiesInRange.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            enemiesInRange.Remove(collision.gameObject);
        }
    }
    public void Upgrade()
    {
        if (Controller.AdjustMoney(-cost))
        {
            UpgradeTower();
        }
    }
    protected virtual void UpgradeTower()
    {
        cost += (int)(cost / 2f);
        fireSpeed -= (int)(fireSpeed/10f);
        if (level % 2 == 1) 
        { 
            damage += 1; 
            radius += 0.5f; 
        }
        level += 1;
    }
}
