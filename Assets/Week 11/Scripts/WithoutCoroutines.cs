using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WithoutCoroutines : MonoBehaviour
{
    public GameObject missile;
    public float speed = 5;
    public float turningSpeedReduction = 0.75f;
    float interpolation = 1;
    float time = 0;
    float legLength = 0;
    float turningAngle = 0;
    Quaternion currentHeading;
    Quaternion newHeading;
    private void Start()
    {
        currentHeading = missile.transform.rotation;
        newHeading = missile.transform.rotation;
    }
    void FixedUpdate()
    {
        Turn(turningAngle);
        RunLeg(legLength);
    }
    public void SetTurnRadius(float radius)
    {
        interpolation = 0;
        turningAngle = radius;
        legLength = 0;
        currentHeading = missile.transform.rotation;
        newHeading = currentHeading * Quaternion.Euler(0, 0, radius);
    }
    public void SetLength(float length)
    {
        time = 0;
        legLength = length;
        turningAngle = 0;
    }
    void Turn(float turn)
    {
        
        if (interpolation < 1)
        {
            interpolation += Time.deltaTime;
            missile.transform.rotation = Quaternion.Lerp(currentHeading, newHeading, interpolation);
            missile.transform.Translate(transform.right * (speed * turningSpeedReduction) * Time.deltaTime);
        }

    }
    void RunLeg(float length)
    {
        if (time < legLength)
        {
            time += Time.deltaTime;
            missile.transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else
        {
            legLength = 0;
        }
    }
}
