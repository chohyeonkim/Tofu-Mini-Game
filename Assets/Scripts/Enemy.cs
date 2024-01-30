using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float horizontal_speed = 0.2f;
    private GameObject tri;
    // Start is called before the first frame update
    void Start()
    {
        tri = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-100, 0, 0) * Time.deltaTime;
    }
}
