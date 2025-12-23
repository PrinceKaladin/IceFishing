using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Gameplay : MonoBehaviour
{
    public Slider timingSlider;
    public Image fillImage;
    public Color successColor = Color.green;
    public Color failColor = Color.red;
    public Text score;
    public GameObject fish;
    public GameObject boot;
    public GameObject udochka;

    public Sprite ul;
    public Sprite defaultRodSprite;

    public Text timerText;

    public float sliderSpeed = 2f;
    public float successMin = 0.45f;
    public float successMax = 0.55f;

    private bool movingRight = true;
    private int timeLeft = 30;
    private bool timerRunning = true;
    
    void Start()
    {
        PlayerPrefs.SetInt("score", 0);
        timingSlider.value = 0.5f;
        fish.SetActive(false);
        boot.SetActive(false);
        UpdateTimerText();
        StartCoroutine(TimerCoroutine());
    }

    void Update()
    {
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

        fillImage.color =
            (timingSlider.value >= successMin && timingSlider.value <= successMax)
            ? successColor
            : failColor;
    }

    public void TryCatchFish()
    {
        udochka.GetComponent<SpriteRenderer>().sprite = ul;

        if (timingSlider.value >= successMin && timingSlider.value <= successMax)
        {
            timeLeft += 5;
            StartCoroutine(SuccessCoroutine());
        }
        else
        {
            timeLeft -= 3;
            StartCoroutine(FailCoroutine());
        }
        score.text =  PlayerPrefs.GetInt("score").ToString();    
        UpdateTimerText();
    
    }

    IEnumerator TimerCoroutine()
    {
        while (timerRunning)
        {
            yield return new WaitForSeconds(1f);
            timeLeft--;
            UpdateTimerText();

            if (timeLeft <= 0)
            {
                SceneManager.LoadScene(4);
                yield break;
            }
        }
    }

    IEnumerator SuccessCoroutine()
    {
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score")+10);
        sliderSpeed = sliderSpeed * 1.1f;
        Color color = timerText.color;
        timerText.color = Color.green;
        fish.SetActive(true);
        yield return new WaitForSeconds(1f);
        timerText.color = color;
        ResetState();

    }

    IEnumerator FailCoroutine()
    {
        Color color = timerText.color;
        timerText.color = Color.red;
        boot.SetActive(true);

        yield return new WaitForSeconds(1f);
        timerText.color = color;
        ResetState();
;
    }

    void ResetState()
    {
        fish.SetActive(false);
        boot.SetActive(false);
        udochka.GetComponent<SpriteRenderer>().sprite = defaultRodSprite;
    }

    void UpdateTimerText()
    {
        if (timeLeft < 0) timeLeft = 0;
        timerText.text = "Time left: " +  timeLeft.ToString();
    }
}
