using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemy : Enemy
{
    public AnimationCurve curve;
    float currentSpeed;
    float timer;
    protected override void Spawn()
    {
        currentHealth = baseHealth * (Controller.wave);                                 // Health increases 100% per wave
        currentReward = baseReward + (int)(baseReward * ((Controller.wave - 1) / 2f));  // Reward increases by 50% per wave
        currentScale = baseScale + (int)(baseScale * ((Controller.wave - 1) / 10f));    // Size increases by 10% per wave
        currentSpeed = baseSpeed;                                                       // Slow enemies do not get faster
    }
    protected override void Move()
    {
        if (path.Count > 1)
        {
            if (timer < 1f) { timer += Time.deltaTime; }
            else { timer = 0f; }
            speed = Mathf.Lerp(currentSpeed/10f, currentSpeed, curve.Evaluate(timer));
        }
        base.Move();
    }
}
