using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Bools")]
    public bool isLevelStart;
    public bool isLevelDone;
    public bool isLevelFail;

    [Header("Canvas Controller")]
    public GameObject levelStartPanel;
    public GameObject completeLevelPanel;
    public GameObject inGamePanel;

    GameController GC;
    PlayerController Player;

    [Header("Panel Settings")]
    public TextMeshProUGUI currentCoinTextStart;
    public TextMeshProUGUI currentCoinTextInGame;
    public TextMeshProUGUI currentCoinTextEnd;
    public TextMeshProUGUI levelTextStart;
    public TextMeshProUGUI levelTextInGame;
    public TextMeshProUGUI levelTextEnd;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI stackText;
    public TextMeshPro stackAmountText;
    public Image fill›mage;

    public static UIController instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    void showTapToStart()
    {
        levelStartPanel.SetActive(true);
    }
    void closeTapToStartPanel()
    {
        levelStartPanel.SetActive(false);
    }
    public void buttonActionTapToStart()
    {
        closeTapToStartPanel();
        GC.tapToStartAction();
    }
    void Start()
    {
        startMethods();
    }

    void startMethods()
    {
        GC = GameController.instance;
        Player = PlayerController.instance;
        showTapToStart();
        currentCoinTextStart.text = PlayerPrefsManager.coins.ToString();
        moneyText.text = PlayerPrefs.GetInt("StackCoin").ToString();
        stackText.text = PlayerPrefs.GetInt("Stack").ToString();
        stackAmountText.text = PlayerPrefs.GetInt("Stack").ToString();
    }
    public void StackUpgradeButtonAction()
    {
        Debug.Log(PlayerPrefsManager.stackCoin);
        if (PlayerPrefsManager.coins >= PlayerPrefs.GetInt("StackCoin"))
        {
            PlayerPrefsManager.coins -= PlayerPrefs.GetInt("StackCoin");
            PlayerPrefsManager.UpdateCoins();
            currentCoinTextStart.text = PlayerPrefsManager.coins.ToString();
            int stack = PlayerPrefs.GetInt("Stack");
            stack++;
            Player.stackAmount++;
            PlayerPrefs.SetInt("Stack", stack);
            int stackCoin = PlayerPrefs.GetInt("StackCoin");
            stackCoin *= 4;
            PlayerPrefs.SetInt("StackCoin", stackCoin);
            moneyText.text = PlayerPrefs.GetInt("StackCoin").ToString();
            stackText.text = PlayerPrefs.GetInt("Stack").ToString();
        }
    }
    void TextUpdates()
    {
        stackText.text = PlayerPrefs.GetInt("Stack").ToString();
        currentCoinTextInGame.text = PlayerPrefsManager.coins.ToString();
        currentCoinTextEnd.text = PlayerPrefsManager.coins.ToString();
    }
    void FillTheBar()
    {
        fill›mage.fillAmount = (float)Player.stackAmount / (float)Player.maximumStackLimit;
    }
    void LevelText()
    { 
       levelTextStart.text = GC.lastLevel.ToString();
       levelTextInGame.text = GC.lastLevel.ToString();
       levelTextEnd.text = GC.lastLevel.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        TextUpdates();
        FillTheBar();
        LevelText();
    }
}
