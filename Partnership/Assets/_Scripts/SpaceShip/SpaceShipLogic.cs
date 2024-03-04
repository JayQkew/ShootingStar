using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipLogic : MonoBehaviour
{
    public bool active;
    public GameObject collectVFX;
    public float forceMultiplier;
    public float breakTime;
    public Rigidbody2D rb;
    public float speed;
    public AudioSource audioSource;

    [Header("BOIDS Behavoir")]
    public float areaOfInfluence;
    public LayerMask spaceShip;
    public GameObject[] surroundingShips;

    void Update()
    {
        if (active == true)
        {
            speed = Vector3.Magnitude(rb.velocity);

            SurroudingShips();
        }
        else if (active == false) 
        {
            ShipScanner();
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

    public void ShipScanner()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, areaOfInfluence, Vector2.zero, 0, spaceShip);

        if (hit.transform.GetComponent<SpaceShipLogic>().active == true)
        {
            //BoidsManager.Instance.boids.Add(gameObject);
            CameraLogic.Instance.focusedShips.Add(gameObject);
            Instantiate(collectVFX, transform.position, Quaternion.identity);
            audioSource.Play();
            active = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, areaOfInfluence);
    }

    private void OnDestroy()
    {
        BoidsManager.Instance.boids.Remove(gameObject);
        CameraLogic.Instance.focusedShips.Remove(gameObject);
    }
}
