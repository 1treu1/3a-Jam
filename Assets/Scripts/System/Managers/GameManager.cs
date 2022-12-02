using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : Singleton<GameManager>
{
    [Header("Components")]
    public PuzzelController puzzelController;
    public GameObject mainPanel;
    public Slider timer;
    public TMPro.TMP_Text scoreText;
    public TMPro.TMP_Text levelText;
    public TMPro.TMP_Text scoreTextMainPanel;

    public float timeMax;
    public float currentTime;
    public int score;

    public int level;
    [Header("Setup")]

    public bool isStartGame;

    public int size;
    public float speed;

    private void Start()
    {
        StartGameEvent();
    }

    public void StartGameEvent()
    {
        SetLevel(level);
        levelText.text = "This is the Level #0" + level;
        scoreTextMainPanel.text = "Score : " + score; 
        scoreText.text = "Score : " + score;
        mainPanel.SetActive(true);
        StartCoroutine(StartGame());
    }

    void SetLevel(int level)
    {
        currentTime = 100f;

        switch (level)
        {
            case 0:
                size = 4;
                speed = 130;
                    break;

            case 1:
                size = 8;
                speed = 140;
                break;

            case 2:
                size = 12;
                speed = 150;
                break;

            case 3:
                size = 12;
                speed = 160;
                break;
        }
    }

    public IEnumerator StartGame()
    {
        yield return new WaitUntil(()=> !mainPanel.activeInHierarchy);
        isStartGame = true;
        yield return new WaitForSeconds(.4f);

        puzzelController.StartningSettup(size);
        yield return new WaitForSeconds(.1f);
        StartCoroutine(CountDown());
    }

    public IEnumerator Winner()
    {
        mainPanel.SetActive(false);
        ScenesManager.Instance?.ui.panelWinner.SetActive(true);
        level++;
        isStartGame = false;
        SoundManager.Instance?.PlayNewSound("Winner");
        yield return new WaitForSeconds(5f);
        yield return new WaitUntil(() => !ScenesManager.Instance.ui.panelWinner.activeInHierarchy);
        StartGameEvent();
    }

    public IEnumerator CountDown()
    {
        while (currentTime >= 0 && isStartGame)
        {
            if (timer != null)
            {
                timer.value = currentTime;
            }

            currentTime -= speed * Time.deltaTime;
            yield return new WaitForSecondsRealtime(1f);
        }

        if (isStartGame)
        {
            StartCoroutine(Losser());
        }

    }

    public IEnumerator Losser()
    {
        puzzelController.SkipGame();
        mainPanel.SetActive(false);
        SoundManager.Instance?.PlayNewSound("Losser");
        isStartGame = false;
        ScenesManager.Instance.ui.panelLosser.SetActive(true);
        yield return new WaitUntil(() => !ScenesManager.Instance.ui.panelLosser.activeInHierarchy);
        ScenesManager.Instance.RestartMainMenu();
    }


    void Update()
    {
        if (!isStartGame)
            return;

        if (Input.GetKeyDown(KeyCode.P) && isStartGame || Input.GetKeyDown(KeyCode.Escape) && isStartGame)
        {
            ScenesManager.Instance?.Pause();
        }

    }
}
