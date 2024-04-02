using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    public float speedMultiplier = 2f;
    float currentSpeed;
    public AnimationCurve curve;
    protected override void Spawn()
    {
        currentHealth = baseHealth + ((Controller.wave - 1));                   // Health increases by 1 per wave
        currentReward = baseReward * (Controller.wave);                         // Reward multiplies each wave
        currentSpeed = baseSpeed + (baseSpeed * ((Controller.wave - 1)/10f));   // Speed increases by 10% each wave
        currentScale = baseScale;                                               // Fast enemies do not grow larger
    }
    protected override void Move()
    {
        if (path.Count > 1)
        {
            float temp = Vector2.Distance(path[0], rb.position) / Vector2.Distance(path[0], path[1]);
            speed = Mathf.Lerp(currentSpeed, currentSpeed * speedMultiplier, curve.Evaluate(temp));
        }
        base.Move();
    }
}
