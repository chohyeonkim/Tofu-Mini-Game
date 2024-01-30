using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float horizontal_speed = -10f;
    public float vertical_speed = 0.2f;
    private Renderer re;
    private Vector2 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        re  = GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        offset += new Vector2(Time.deltaTime * horizontal_speed, Time.deltaTime * vertical_speed);
        re.material.mainTextureOffset = offset;
    }
}
