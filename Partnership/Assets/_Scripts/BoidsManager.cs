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

    private void Awake()
    {
        Instance = this;
    }


    private void Update()
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

        if (centerOfMassForce.magnitude > 0) boid.GetComponent<Rigidbody2D>().AddForce(noramilzedCenterOfMass * coherenceForce, ForceMode2D.Force);
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


        if (center.magnitude > 0) boid.GetComponent<Rigidbody2D>().AddForce(normailzedCenter * separationForce, ForceMode2D.Force);
        else return;

    }

}
