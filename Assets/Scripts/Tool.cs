using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite[] tools;

    public void Start()
    {
        holdTool();
    }

    public void holdTool()
    {
        sr.sprite = tools[1];
        sr.size = new Vector2(0.5f, 0.5f);
    }
}
