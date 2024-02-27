using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public float gravityMultiplier;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rb_obj = collision.GetComponent<Rigidbody2D>();

        Vector3 planetCentre = transform.position - (Vector3)rb_obj.position;
        Vector3 clampedGravity = Vector3.ClampMagnitude(planetCentre, 1);

        rb_obj.AddForce(clampedGravity * gravityMultiplier, ForceMode2D.Force);
    }
}
