using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGUI : MonoBehaviour
{
    public Animator anim;

    public float randomScaleMax;

    private void OnEnable()
    {
        
        transform.localScale *= Random.Range(1, randomScaleMax);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();


        switch (AestheticManager.Instance.currentColour)
        {
            case MainColour.Blue:
                anim.SetInteger("AnimType", 0);
                break;
            case MainColour.Green:
                anim.SetInteger("AnimType", 1);
                break;
            case MainColour.Yellow:
                anim.SetInteger("AnimType", 2);
                break;
            case MainColour.Red:
                anim.SetInteger("AnimType", 3);
                break;
        }
    }
}
