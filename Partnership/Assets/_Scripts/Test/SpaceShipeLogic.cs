using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipeLogic : MonoBehaviour
{
    public float forceMultiplier;
    public float breakTime;
    public Rigidbody2D rb;
    void Update()
    {
        // make the ship look in the direction of the mouse
        Vector3 forceDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rb.rotation = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90f;

        Vector3 clampedThrust = Vector3.ClampMagnitude(forceDir, 5);
        // 
        if (Input.GetMouseButton(0))
        {
            rb.AddForce(clampedThrust * forceMultiplier * Time.fixedDeltaTime, ForceMode2D.Force);
        }
        if (Input.GetMouseButton(1))
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, breakTime * Time.fixedDeltaTime);
        }
    }
}
