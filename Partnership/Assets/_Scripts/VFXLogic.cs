using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXLogic : MonoBehaviour
{
    public float activeTime;

    private void Start()
    {
        StartCoroutine(DestroyAfterSeconds());
    }

    public IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(activeTime);
        Destroy(gameObject);
    }
}
