using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI remainingMoveAmountText;
    public TextMeshProUGUI endGameScreenText;
    public GameObject gameScreen;
    public GameObject endGameScreen;
    public GameObject menuScreen;
    public GameObject gameEndScreen;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one UIManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void UpdateRemainingMoveText(int amount)
    {
        remainingMoveAmountText.text = amount.ToString();
    }
}
