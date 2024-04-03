using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage;
    public GameObject target;
    public GameObject explosion;
    public float radius = 0;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Fly();
    }
    protected virtual void Fly()
    {
        Vector2 direction = (rb.position - (Vector2)target.transform.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        rb.SetRotation(-angle);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == target)
        {
            collision.gameObject.SendMessage("takeDamage", damage, SendMessageOptions.DontRequireReceiver);
            if (radius > 0 && explosion != null)
            {
                GameObject temp = Instantiate(explosion);
            }
            Destroy(gameObject);
        }
    }

}
