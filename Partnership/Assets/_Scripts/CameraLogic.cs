using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public static CameraLogic Instance { get; private set; }

    public CinemachineVirtualCamera virtualCamera;
    public GameObject mainShip;

    public float minOrtho = 30;

    public float zoomSpeed;
    public float minZoomSpeed;

    public float moveDelta;
    public float speedThreshold;

    public float targetOrtho;

    public List<GameObject> focusedShips = new List<GameObject>();
    public LayerMask shipLayer;
    public Vector2 colliderArea;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        focusedShips = BoidsManager.Instance.boids;
    }
    private void Update()
    {
        FollowShip();
        CameraZoom();

        virtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(virtualCamera.m_Lens.OrthographicSize, minOrtho, 100);
        targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, 100);

        if (AverageSpeed() >= minZoomSpeed)
        {
            targetOrtho = AverageSpeed() * 1.75f;
        }
        if (AverageSpeed() <= minZoomSpeed)
        {
            targetOrtho = minOrtho;
        }
    }

    public void FollowShip() => transform.position = new Vector3(mainShip.transform.position.x, mainShip.transform.position.y, transform.position.z);

    public float AverageSpeed()
    {
        float totalSpeed = 0;

        foreach (GameObject go in focusedShips)
        {
            totalSpeed += go.GetComponent<SpaceShipLogic>().speed;
        }

        return totalSpeed / focusedShips.Count;
    }

    public void CameraZoom()
    {
        if (AverageSpeed() >= minZoomSpeed)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, targetOrtho, zoomSpeed * Time.deltaTime);
        }
        if (AverageSpeed() <= minZoomSpeed)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, targetOrtho, Mathf.Pow(zoomSpeed * Time.deltaTime, 3));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, colliderArea);
    }

}
