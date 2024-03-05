using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLogic : MonoBehaviour
{
    public GameObject[] moon = new GameObject[0];
    public float[] rotationSpeeds = new float[0];

    public GameObject plant;
    public GameObject plantParticles;

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
            for (int i = 0; i < moon.Length; i++)
            {
                moon[i].transform.RotateAround(transform.position, Vector3.forward, rotationSpeeds[i] * Time.deltaTime);
            }
        }
    }

    public void SpawnPlant(Vector3 pos, float normalZ)
    {

        GameObject dust = Instantiate(plantParticles, transform, true);
        dust.transform.position = pos;
        dust.transform.Rotate(new Vector3(0, 0, normalZ));

        GameObject _plant = Instantiate(plant, transform, true);
        _plant.transform.position = pos;
        _plant.transform.Rotate(new Vector3(0, 0, normalZ));
        _plant.GetComponentInChildren<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color;
        _plant.transform.localScale *= Random.Range(1, 3.5f);

        // decided with GUI script for plant
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "ship" && collision.collider.name != "SpaceShip_main")
        {
            //BoidsManager.Instance.boids.Remove(gameObject);
            Vector3 collisionPoint = collision.contacts[0].point;

            Vector3 normalPoint = collisionPoint - transform.position;
            float collisionNormal = Mathf.Atan2(normalPoint.y, normalPoint.x) * Mathf.Rad2Deg - 90f;
            Debug.Log(collisionNormal);
            //float collisionNormal = collision.collider.gameObject.transform.rotation.z + 180;

            SpawnPlant(collisionPoint, collisionNormal);

            CameraLogic.Instance.focusedShips.Remove(gameObject);
            Destroy(collision.gameObject);
        }

    }
}
