using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public float forceMultiplier;
    public GameObject moon;
    public float rotationSpeed;

    private void Update()
    {
        if (moon != null)
        {
            moon.transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rb_obj = collision.GetComponent<Rigidbody2D>();


        rb_obj.AddForce((transform.position - (Vector3)rb_obj.position)*forceMultiplier, ForceMode2D.Force);
    }
}
