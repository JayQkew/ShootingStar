using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpaceShipeLogic : MonoBehaviour
{
    public float forceMultiplier;
    public float breakTime;
    public Rigidbody2D rb;
    public float speed;

    [Header("BOIDS Behavoir")]
    public float areaOfInfluence;
    public LayerMask spaceShip;
    public GameObject[] surroundingShips;

    void Update()
    {
        speed = Vector3.Magnitude(rb.velocity);

        MovementControl();
        SurroudingShips();
    }

    public void MovementControl()
    {
        // make the ship look in the direction of the mouse
        Vector3 forceDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rb.rotation = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90f;

        Vector3 clampedThrust = Vector3.ClampMagnitude(forceDir, 5);
        if (Input.GetMouseButton(0))
        {
            rb.AddForce(clampedThrust * forceMultiplier * Time.fixedDeltaTime, ForceMode2D.Force);
        }
        if (Input.GetMouseButton(1))
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, breakTime * Time.fixedDeltaTime);
        }
    }

    public void SurroudingShips()
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, areaOfInfluence, Vector2.zero, 0, spaceShip);
        List<GameObject> sur_ships = new List<GameObject>();

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject != gameObject)
            {
                sur_ships.Add(hit[i].transform.gameObject);
            }
        }

        surroundingShips = sur_ships.ToArray();

    }

}
