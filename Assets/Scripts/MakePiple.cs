using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePiple : MonoBehaviour
{
    public GameObject pipe;
    public float timediff;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timediff)
        {
            GameObject newpipe = Instantiate(pipe);
            newpipe.transform.position = new Vector3(6, Random.Range(0.51f, 4.61f), 0);

            timer = 0;

            Destroy(newpipe, 10.0f);
        }

        
        
    }
}
