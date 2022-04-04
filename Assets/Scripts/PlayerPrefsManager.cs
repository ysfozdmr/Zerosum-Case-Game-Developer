using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public const string Coins = "Coins";
    public static int coins = 0;
    public const string Stack = "Stack";
    public static int stack = 0;
    public const string StackCoin = "StackCoin";
    public static int stackCoin = 10;
    void Start()
    {
        coins = PlayerPrefs.GetInt("Coins");
    }

    public static void UpdateCoins()
    {
        PlayerPrefs.SetInt("Coins", coins);
        coins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.Save();
    }
    public static void UpdateStack()
    {
        PlayerPrefs.SetInt("Stack", stack);
        stack = PlayerPrefs.GetInt("Stack");
        PlayerPrefs.Save();
    }
    public static void UpdateStackCoins()
    {
        PlayerPrefs.SetInt("StackCoin", stackCoin);
        stackCoin = PlayerPrefs.GetInt("StackCoin");
        PlayerPrefs.Save();
    }
    void Update()
    {

    }
}
