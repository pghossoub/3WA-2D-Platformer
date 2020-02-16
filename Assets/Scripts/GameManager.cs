using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public IntVariable pv;
    public IntVariable score;
    public GameObject[] imagesHeart;
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;

    private bool isGameOver = false;
    void Start()
    {
        pv.value = 3;
        score.value = 0;
    }

    void Update()
    {
        DrawHearts();
        DrawScore();
        if (pv.value <= 0 && !isGameOver)
        {
            isGameOver = true;
            StartCoroutine(GameOver());
        }
    }

    void DrawHearts()
    {
        int i = 1;
        foreach (GameObject image in imagesHeart)
        {
            if (i <= pv.value)
                image.SetActive(true);
            else
                image.SetActive(false);
            i++;
        }
    }

    void DrawScore()
    {
        scoreText.text = string.Format("{0}", score.value);
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);
        gameOverPanel.SetActive(true);
    }
}
