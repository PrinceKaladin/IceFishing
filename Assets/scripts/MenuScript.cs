using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuScript : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }
 

    public void SelectLevel(int Level) { 
    SceneManager.LoadScene(Level);
    }
}
