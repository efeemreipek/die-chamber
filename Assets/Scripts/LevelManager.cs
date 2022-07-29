using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public int levelOneMovePoints = 8;
    public int levelTwoMovePoints = 18;
    public int levelThreeMovePoints = 36;
    public int levelFourMovePoints = 60;
    public int levelFiveMovePoints = 100;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one LevelManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        UIManager.Instance.UpdateRemainingMoveText(GameManager.Instance.GetRemainingMovePoints());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CableManager.Instance.isEveryCableOn)
            {
                int levelIndex = SceneManager.GetActiveScene().buildIndex;

                switch (levelIndex + 1)
                {
                    case 1:
                        GameManager.Instance.SetRemainingMovePoints(levelTwoMovePoints);
                        LoadSceneIndex(levelIndex + 1);
                        break;
                    case 2:
                        GameManager.Instance.SetRemainingMovePoints(levelThreeMovePoints);
                        LoadSceneIndex(levelIndex + 1);
                        break;
                    case 3:
                        GameManager.Instance.SetRemainingMovePoints(levelFourMovePoints);
                        LoadSceneIndex(levelIndex + 1);
                        break;
                    case 4:
                        GameManager.Instance.SetRemainingMovePoints(levelFiveMovePoints);
                        LoadSceneIndex(levelIndex + 1);
                        break;
                    case 5:
                        UIManager.Instance.gameEndScreen.SetActive(true);
                        GameManager.Instance.SetIsGameOverForGood(true);
                        break;
                }

                UIManager.Instance.UpdateRemainingMoveText(GameManager.Instance.GetRemainingMovePoints());
            }
        }
    }

    public void LoadSceneIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public int GetSceneIndex() => SceneManager.GetActiveScene().buildIndex;

    public void CloseGame()
    {
        Application.Quit();
    }
}
