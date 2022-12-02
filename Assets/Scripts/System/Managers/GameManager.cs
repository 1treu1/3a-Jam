using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : Singleton<GameManager>
{
    [Header("Components")]
    public PuzzelController puzzelController;

    [Header("Setup")]
    public bool isStartGame;

    public int size;


    private void Start()
    {
        StartCoroutine(StartGame());
    }

    public IEnumerator StartGame()
    {
        isStartGame = true;
        
        yield return new WaitForSeconds(.5f);
        puzzelController.StartningSettup(size);
        yield return new WaitForSeconds(.1f);
        //CountDown();
    }

    public IEnumerator Winner()
    {
        isStartGame = false;
        SoundManager.Instance.PauseAllSounds(true);
        SoundManager.Instance.PlayNewSound("Winner");
        yield return new WaitForSeconds(5f);
        ScenesManager.Instance.ui.panelWinner.SetActive(true);
        yield return new WaitUntil(() => !ScenesManager.Instance.ui.panelWinner.activeInHierarchy);
        ScenesManager.Instance.RestartMainMenu();
    }
    public IEnumerator Losser()
    {
        isStartGame = false;
        SoundManager.Instance.PauseAllSounds(true);
        SoundManager.Instance.PlayNewSound("Losser");
        yield return new WaitForSeconds(5f);
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
