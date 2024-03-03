using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ChunkLogic : MonoBehaviour
{

    public void CheckSelf()
    {
        for (int i = 0; i < SpaceManager.Instance.surroundingChunks.Length; i++)
        {
            if (SpaceManager.Instance.surroundingChunks[i] == gameObject) return;
            else Destroy(gameObject);
        }
    }
}
