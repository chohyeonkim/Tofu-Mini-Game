using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Score: " + Score.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
