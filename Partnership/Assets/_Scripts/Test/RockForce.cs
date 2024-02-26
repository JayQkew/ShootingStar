using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockForce : MonoBehaviour
{
    public float forceMultiplier;
    void Update()
    {
        Vector3 lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector3)GetComponent<Rigidbody2D>().position;
        GetComponent<Rigidbody2D>().rotation = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        if (Input.GetMouseButton(0))
        {
            GetComponent<Rigidbody2D>().AddForce((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position) * forceMultiplier, ForceMode2D.Force);
        }
    }
}
