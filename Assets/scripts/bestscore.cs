using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bestscore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("score")) {
            PlayerPrefs.SetInt("bestscore", 0);

        }
        if (PlayerPrefs.GetInt("bestscore") < PlayerPrefs.GetInt("score")) {
            PlayerPrefs.SetInt("bestscore", PlayerPrefs.GetInt("score"));
        }
        this.GetComponent<Text>().text = "Best score: " + PlayerPrefs.GetInt("bestscore").ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
