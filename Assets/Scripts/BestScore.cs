using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BestScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Best Score: " + Score.bestScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
