using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    public Transform CanvasRotation;
    public Slider slider;

    public List<Vector2> path;
    public int baseHealth;
    public int baseReward;
    public int damage;
    public float baseSpeed;
    public float baseScale;
    
    protected float currentScale;
    protected int currentHealth;
    protected int currentReward;

    protected float scale;
    protected int health;
    protected float speed;
    protected int reward;

    public float lifeTime { get; private set; }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Spawn();
        slider.maxValue = currentHealth;
        slider.value = currentHealth;
        reward = currentReward;
        foreach(Vector2 p in Controller.instance.path)
        {
            path.Add(p);
        }
    }
    protected virtual void Spawn()
    {
        currentHealth = baseHealth + ((Controller.wave-1)*2);       // Health increases by 2 per wave
        currentReward = baseReward + Controller.wave;               // Reward increases by 1 per wave
        speed = baseSpeed;
    }
    private void FixedUpdate()
    {
        Move();
    }
    protected virtual void Move()
    {
        if (path.Count <= 1)
        {
            ReachEnd();
            return;
        }
        if ((path[1] - rb.position).magnitude > 0.05f)
        {
            Vector2 direction = (path[1] - rb.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            rb.rotation = -angle;
            CanvasRotation.rotation = Quaternion.Euler(Vector3.up);
        }
        else
        {
            path.RemoveAt(0);
        }
    }
    private void Update()
    {
        lifeTime += Time.deltaTime;
    }
    public void TakeDamage(int d)
    {
        health -= d;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void ReachEnd()
    {
        Controller.LoseHP(damage);
        reward = 0;
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        Controller.AdjustMoney(reward);
    }
}
