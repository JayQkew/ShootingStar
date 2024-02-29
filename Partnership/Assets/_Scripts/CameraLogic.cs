using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public GameObject mainShip;

    public float zoomSpeed;
    public float minZoomSpeed;

    public float moveDelta;
    public float speedThreshold;

    public float targetOrtho;

    public GameObject[] focusedShips = new GameObject[0];
    public LayerMask shipLayer;
    public Vector2 colliderArea;


    private void Update()
    {
        FleetCollider();
        FollowFleet();
        CameraZoom();

        virtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(virtualCamera.m_Lens.OrthographicSize, 30, 100);

        if (AverageSpeed() >= minZoomSpeed)
        {
            targetOrtho = AverageSpeed() * 1.75f;
        }
    }

    public void FollowFleet()
    {

        if (focusedShips.Length <= 0)
        {
            transform.position = new Vector3(mainShip.transform.position.x, mainShip.transform.position.y, transform.position.z);
        }
        else
        {
            Vector2 totalPos = Vector2.zero;


            foreach (GameObject go in focusedShips)
            {
                totalPos += (Vector2)go.transform.position;
            }

            Vector2 center = totalPos / focusedShips.Length;


            if (targetOrtho < speedThreshold)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(center.x, center.y, transform.position.z), moveDelta * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(center.x, center.y, transform.position.z), moveDelta * 2 * Time.deltaTime);
            }
        }

    }

    public float AverageSpeed()
    {
        float totalSpeed = 0;

        foreach (GameObject go in focusedShips)
        {
            totalSpeed += go.GetComponent<SpaceShipLogic>().speed;
        }

        return totalSpeed / focusedShips.Length;
    }

    public void FleetCollider()
    {
        RaycastHit2D[] hit = Physics2D.BoxCastAll(transform.position, colliderArea, 0, Vector3.zero, 0, shipLayer);
        List<GameObject> sur_ships = new List<GameObject>();

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject != gameObject)
            {
                sur_ships.Add(hit[i].transform.gameObject);
            }
        }

        focusedShips = sur_ships.ToArray();
    }

    public void CameraZoom()
    {
        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, targetOrtho, zoomSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, colliderArea);
    }

}
