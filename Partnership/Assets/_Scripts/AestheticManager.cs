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

        //foreach (var boi in BoidsManager.Instance.boids)
        //{
        //    boi.GetComponentInChildren<SpaceShipGUI>().ChangeColours();
        //}

        currentColour = (MainColour)Random.Range(0, 4);
    }

    private void Start()
    {

        //foreach (var boi in BoidsManager.Instance.boids)
        //{
        //    boi.GetComponentInChildren<SpaceShipGUI>().ChangeColours();
        //}

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentColour = (MainColour)Random.Range(0, 4);

            foreach(var boi in BoidsManager.Instance.boids)
            {
                boi.GetComponentInChildren<SpaceShipGUI>().ChangeColours();
            }
        }
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
