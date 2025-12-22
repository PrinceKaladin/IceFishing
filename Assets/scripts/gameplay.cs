using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Gameplay : MonoBehaviour
{
    public Slider timingSlider;      // Ссылка на UI Slider
    public Image fillImage;          // Ссылка на Fill Image для цветовой индикации
    public Color successColor = Color.green;
    public Color failColor = Color.red;

    public GameObject fish;
    public GameObject boot;
    public GameObject udochka;
    public Sprite ul;

    public float sliderSpeed = 2f;  // Скорость движения ползунка
    public float successMin = 0.45f; // Начало "зоны удачи"
    public float successMax = 0.55f; // Конец "зоны удачи"

    private bool movingRight = true;

    void Start()
    {
        timingSlider.value = 0.5f;
    }

    void Update()
    {
        // Двигаем ползунок туда-сюда
        if (movingRight)
        {
            timingSlider.value += sliderSpeed * Time.deltaTime;
            if (timingSlider.value >= 1f) movingRight = false;
        }
        else
        {
            timingSlider.value -= sliderSpeed * Time.deltaTime;
            if (timingSlider.value <= 0f) movingRight = true;
        }

        // Изменяем цвет фона в зависимости от положения
        if (timingSlider.value >= successMin && timingSlider.value <= successMax)
            fillImage.color = successColor;
        else
            fillImage.color = failColor;

        // Ловим тайминг по нажатию
       
    }

    public void TryCatchFish()
    {
        udochka.GetComponent<SpriteRenderer>().sprite = ul;

        if (timingSlider.value >= successMin && timingSlider.value <= successMax)
        {
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 10);
            StartCoroutine(SuccessCoroutine());
        }
        else
        {
            StartCoroutine(FailCoroutine());
        }
    }

    IEnumerator SuccessCoroutine()
    {
        fish.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(4); // Победа
    }

    IEnumerator FailCoroutine()
    {
        boot.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(5); // Проигрыш
    }
}
