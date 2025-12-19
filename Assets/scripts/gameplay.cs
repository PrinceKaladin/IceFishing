using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameplay : MonoBehaviour
{
    public GameObject boot;
    public GameObject fish;
    public Sprite ul;
    public GameObject udochka;
    public void Ulov() {
        transform.Translate(new Vector2(10f,10f));
        int i = Random.Range(0, 3);
        udochka.GetComponent<SpriteRenderer>().sprite = ul;
        if (i == 0) {
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 10);

            StartCoroutine(x());  
            
        }
        else
        {
            
            StartCoroutine (y());
        }

    }
    IEnumerator x() { 
        fish.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(4);
    }
    IEnumerator y()
    {
        boot.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(5);
    }
}
