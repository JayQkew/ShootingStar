using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
    public static BoidsManager Instance { get; private set; }

    public List<GameObject> boids = new List<GameObject>();
    public float coherenceForce;
    public float separationForce;
    public float areaOfInfluence;

    private bool move;
    private bool stop;

    public float forceMultiplier;
    public float breakTime;
    private void Awake()
    {
        Instance = this;
    }


    private void Update()
    {
        MovementInput();
    }

    private void FixedUpdate()
    {
        BoidsLogic();
        
    }

    private void BoidsLogic()
    {

        foreach (var boid in boids)
        {
            boid.GetComponent<SpaceShipLogic>().areaOfInfluence = areaOfInfluence;

            Coherence(boid);
            Separation(boid);

            MovementCalc(boid);
        }
    }

    private void MovementCalc(GameObject boid)
    {
        Rigidbody2D rb = boid.GetComponent<Rigidbody2D>();

        Vector3 forceDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - boid.transform.position;
        rb.rotation = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90f;

        Vector3 clampedThrust = Vector3.ClampMagnitude(forceDir, 5);

        if (boid.GetComponent<SpaceShipLogic>().move == true)
        {
            rb.AddForce(clampedThrust * forceMultiplier, ForceMode2D.Force);
            boid.GetComponent<SpaceShipLogic>().move = false;
        }
        if (boid.GetComponent<SpaceShipLogic>().stop == true)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, breakTime* 10 * Time.deltaTime);
            boid.GetComponent<SpaceShipLogic>().stop = false;
        }
    }

    private void MovementInput()
    {
        if ( Input.GetMouseButton(0))
        {
            foreach(var boid in boids)
            {
                boid.GetComponent<SpaceShipLogic>().move = true;
            }
        }
        if (Input.GetMouseButton(1))
        {
            foreach (var boid in boids)
            {
                boid.GetComponent<SpaceShipLogic>().stop = true;
            }
        }
    }


    private void Coherence(GameObject boid)
    {

        // calculate the center of mass
        Vector3 totalMass = Vector3.zero;

        if (boid.GetComponent<SpaceShipLogic>().surroundingShips.Length > 0)
        {
            foreach (GameObject b in boid.GetComponent<SpaceShipLogic>().surroundingShips)
            {
                totalMass += b.transform.position;
            }
        }
        else return;

        Vector2 centerOfMass = totalMass / (boid.GetComponent<SpaceShipLogic>().surroundingShips.Length - 1);

        Vector2 centerOfMassForce = centerOfMass - (Vector2)boid.transform.position;

        Vector2 noramilzedCenterOfMass = Vector2.ClampMagnitude(centerOfMassForce, 1);

        if (centerOfMassForce.magnitude > 0 && (noramilzedCenterOfMass * coherenceForce).GetType() == typeof(Vector2))
        {
            Vector2 force = noramilzedCenterOfMass * coherenceForce;
            if (!float.IsNaN(force.x) && !float.IsNaN(force.y))
            {
                boid.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Force);
            }
        }
        else return;

        Debug.DrawLine(boid.transform.position, centerOfMassForce);
    }

    public void Separation(GameObject boid)
    {
        Vector2 center = Vector2.zero;

        if (boid.GetComponent<SpaceShipLogic>().surroundingShips.Length > 0)
        {
            foreach (GameObject b in boid.GetComponent<SpaceShipLogic>().surroundingShips)
            {
                if (Vector3.Magnitude(b.transform.position - boid.transform.position) < 10)
                {
                    center -= (Vector2)b.transform.position - (Vector2)boid.transform.position;
                }

            }
        }
        else return;

        Vector2 normailzedCenter = Vector2.ClampMagnitude(center, 1);


        if (center.magnitude > 0 && (normailzedCenter * separationForce).GetType() == typeof(Vector2))
        {
            Vector2 force = normailzedCenter * separationForce;
            if (!float.IsNaN(force.x) && !float.IsNaN(force.y))
            {
                boid.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Force);
            }
        }
        else return;

    }

}
