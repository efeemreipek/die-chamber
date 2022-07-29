using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int remainingMovePoints = 8;

    [TextArea] public string fallEndGameText;
    [TextArea] public string moveEndGameText;

    private bool isGameOver = false;
    private bool isOnMenu = true;
    private bool isGameOverForGood = false;
    private bool isGameBegun = false;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one GameManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);

    }

    private void Start()
    {
        UIManager.Instance.menuScreen.SetActive(true);
    }

    private void Update()
    {
        if(remainingMovePoints <= 0)
        {
            EndLevel();
        }
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                LevelManager.Instance.LoadSceneIndex(LevelManager.Instance.GetSceneIndex());
                UIManager.Instance.gameScreen.SetActive(true);
                UIManager.Instance.endGameScreen.SetActive(false);
                isGameOver = false;

                switch (LevelManager.Instance.GetSceneIndex())
                {
                    case 0:
                        remainingMovePoints = LevelManager.Instance.levelOneMovePoints;
                        break;
                    case 1:
                        remainingMovePoints = LevelManager.Instance.levelTwoMovePoints;
                        break;
                    case 2:
                        remainingMovePoints = LevelManager.Instance.levelThreeMovePoints;
                        break;
                    case 3:
                        remainingMovePoints = LevelManager.Instance.levelFourMovePoints;
                        break;
                    case 4:
                        remainingMovePoints = LevelManager.Instance.levelFiveMovePoints;
                        break;
                }

                Time.timeScale = 1f;
            }
        }
        if (isOnMenu)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                UIManager.Instance.menuScreen.SetActive(false);
                isOnMenu = false;
                isGameBegun = true;
            }
        }

        if (isGameOverForGood)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                LevelManager.Instance.CloseGame();
            }
        }

        if (isGameBegun)
        {
            UIManager.Instance.menuScreen.SetActive(false);
        }
    }

    public int GetRemainingMovePoints() => remainingMovePoints;

    public void SetRemainingMovePoints(int points)
    {
        remainingMovePoints = points;
    }

    public void EndLevel()
    {
        UIManager.Instance.gameScreen.SetActive(false);

        UIManager.Instance.endGameScreen.SetActive(true);
        UIManager.Instance.endGameScreenText.text = remainingMovePoints <= 0 ? moveEndGameText : fallEndGameText;

        Time.timeScale = 0f;
        isGameOver = true;
    }

    public bool IsOnMenu() => isOnMenu;
    public bool IsGameOverForGood() => isGameOverForGood;
    public void SetIsGameOverForGood(bool cond)
    {
        isGameOverForGood = cond;
    }

}
