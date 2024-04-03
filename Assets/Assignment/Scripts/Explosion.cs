using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius;
    public float damage;
    public float lifetime = 0.3f;
    float timer = 0f;
    CircleCollider2D circleCollider;
    SpriteRenderer spr;
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        StartCoroutine(Explode());
    }
    IEnumerator Explode()
    {
        while (timer < lifetime)
        {
            Color temp = spr.color;
            temp.a -= 5;
            spr.color = temp;
            transform.localScale = new Vector3(transform.localScale.x+0.1f, transform.localScale.y + 0.1f, transform.localScale.z);
            circleCollider.radius = Mathf.Lerp(radius / 2f, radius, (timer / lifetime));
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
