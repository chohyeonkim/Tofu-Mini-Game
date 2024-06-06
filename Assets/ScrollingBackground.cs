using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float hspeed;
    public float vspeed;
    public Renderer re;


    // Start is called before the first frame update
    void Start()
    {
        re = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(Time.time * hspeed, Time.time * vspeed);
        re.material.mainTextureOffset = offset;
    }
}
