using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public IntVariable pv;
    public GameObject gameOverPanel;

    private bool isGameOver = false;
    void Start()
    {
        pv.value = 3;
    }

    void Update()
    {
        if (pv.value <= 0 && !isGameOver)
        {
            isGameOver = true;
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.0f);
        gameOverPanel.SetActive(true);
    }
}
