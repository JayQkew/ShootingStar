using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpaceShipGUI : MonoBehaviour
{
    [Header("Ship Colour")]
    public Animator anim;
    public AnimationClip clip;
    public AnimationCurve r_curve;
    public AnimationCurve g_curve;
    public AnimationCurve b_curve;

    public Color displayColor;

    public Color red;
    public Color blue;
    public Color yellow;
    public Color green;

    [Header("Trail Renderer")]
    public TrailRenderer trailRenderer;

    public Gradient gradient_red;
    public Gradient gradient_blue;
    public Gradient gradient_yellow;
    public Gradient gradient_green;

    [Header("Shapes")]
    public SpriteRenderer spriteRenderer;
    public Sprite triangle;
    public Sprite circle;
    public Sprite square;

    private void Start()
    {
        anim = GetComponent<Animator>();
        ChangeColours();
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            ChangeColours();
        }
    }

    public void ChangeColours()
    {
        switch (AestheticManager.Instance.currentColour)
        {
            case MainColour.Yellow:
                trailRenderer.colorGradient = gradient_yellow;
                anim.SetInteger("AnimInt", 2);
                break;
            case MainColour.Green:
                trailRenderer.colorGradient = gradient_green;
                anim.SetInteger("AnimInt", 3);
                break;
            case MainColour.Red:
                trailRenderer.colorGradient = gradient_red;
                anim.SetInteger("AnimInt", 0);
                break;
            case MainColour.Blue:
                trailRenderer.colorGradient = gradient_blue;
                anim.SetInteger("AnimInt", 1);
                break;
        }

        switch (AestheticManager.Instance.currentShape)
        {
            case Shape.Circle:
                spriteRenderer.sprite = circle;
                transform.localScale = new Vector3(1, 1, 0);
                break;
            case Shape.Square:
                spriteRenderer.sprite = square;
                break;
            case Shape.Triangle:
                spriteRenderer.sprite = triangle;
                break;
        }
    }
    public void ColourKeys(Color colour)
    {
        float r_col = colour.r;
        float g_col = colour.g;
        float b_col = colour.b;

        r_curve = new AnimationCurve();
        g_curve = new AnimationCurve();
        b_curve = new AnimationCurve();

        Keyframe r_start = new Keyframe(0, Color.white.r);
        Keyframe g_start = new Keyframe(0, Color.white.g);
        Keyframe b_start = new Keyframe(0, Color.white.b);

        Keyframe r_mid = new Keyframe(0.5f, r_col);
        Keyframe g_mid = new Keyframe(0.5f, g_col);
        Keyframe b_mid = new Keyframe(0.5f, b_col);

        Keyframe r_end = new Keyframe(1f, Color.white.r);
        Keyframe g_end = new Keyframe(1f, Color.white.g);
        Keyframe b_end = new Keyframe(1f, Color.white.b);

        Keyframe[] r_keys =
        {
            r_start,
            r_mid,
            r_end
        };

        Keyframe[] g_keys =
        {
            g_start,
            g_mid,
            g_end
        };

        Keyframe[] b_keys =
        {
            b_start,
            b_mid,
            b_end
        };

        r_curve.keys = r_keys;
        g_curve.keys = g_keys;
        b_curve.keys = b_keys;

        clip.SetCurve("", typeof(SpriteRenderer), "m_Color.r", r_curve);
        clip.SetCurve("", typeof(SpriteRenderer), "m_Color.g", g_curve);
        clip.SetCurve("", typeof(SpriteRenderer), "m_Color.b", b_curve);
    }

}