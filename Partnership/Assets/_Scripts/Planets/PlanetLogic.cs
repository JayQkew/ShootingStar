using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLogic : MonoBehaviour
{
    public GameObject[] moon = new GameObject[0];
    public float[] rotationSpeeds = new float[0];

    private void Awake()
    {
    }

    private void Update()
    {
        RotateMoons();
    }

    private void RotateMoons()
    {
        if (moon.Length > 0)
        {
            for(int i = 0; i < moon.Length; i++)
            {
                moon[i].transform.RotateAround(transform.position, Vector3.forward, rotationSpeeds[i] * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("BOOOMMM");
    }
}
