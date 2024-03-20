using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject[] parts;
    public float maxTime = 1f;
    void Start()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            StartCoroutine(Build(i));
        }
    }
    IEnumerator Build(int order)
    {
        float timer = 0f;
        parts[order].transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(order*maxTime);
        while (parts[order].transform.localScale.z < 1) 
        {
            timer += Time.deltaTime;
            parts[order].transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timer/maxTime);
            yield return null;
        }
        
    }
}
