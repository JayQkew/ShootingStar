using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public GameObject player;

    public float zoomSpeed;

    private void Update()
    {
        FollowPlayer();
        //CameraZoom();
    }

    public void FollowPlayer()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    //public void CameraZoom()
    //{
    //    if (SpaceShipeLogic.Instance.speed >= 10)
    //    {

    //        float targetSize = SpaceShipeLogic.Instance.speed * 2;

    //        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, targetSize, zoomSpeed * Time.deltaTime);
    //    }
    //    else
    //    {
    //        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 20, zoomSpeed* 2 * Time.deltaTime);
    //    }
    //}
}
