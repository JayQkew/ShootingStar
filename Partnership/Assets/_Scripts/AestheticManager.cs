using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AestheticManager : MonoBehaviour
{
    public static AestheticManager Instance { get; private set; }

    public Shape currentShape;
    public MainColour currentColour;

    private void Awake()
    {
        Instance = this;

        //currentShape = (Shape)Random.Range(0, 3);
        //currentColour = (MainColour)Random.Range(0, 4);
    }

}

public enum Shape
{
    Circle,
    Square,
    Triangle
}

public enum MainColour
{
    Blue,
    Green,
    Yellow,
    Red
}
