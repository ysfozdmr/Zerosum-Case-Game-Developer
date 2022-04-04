using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Movement Setting")]
    [SerializeField] float movementSpeed;
    public int sensitivity;
    float mouseTempX;
    float horizontalTempPos;
    float mouseCurrentX;
    public Animator playerAnim;

    public CinemachineVirtualCamera finishCam;
    public int stackAmount;
    public int maximumStackLimit = 20;
    public GameObject playerCanvas;

    GameController GC;
    UIController UI;
    private void Awake()
    {
        instance = this;
        if (PlayerPrefs.GetString("unity.player_session_count") == "1")
        {
            PlayerPrefs.SetInt("StackCoin", 4);
        }
    }
    void Start()
    {
        StartMethods();
    }
    void StartMethods()
    {
        GC = GameController.instance;
        UI = UIController.instance;
        stackAmount = PlayerPrefs.GetInt("Stack");
    }

    private void HorizontalMovement()
    {
        if (GC.isLevelStart && !GC.isLevelFail && !GC.isLevelDone)
        {
            playerCanvas.SetActive(true);
            transform.position += Vector3.forward * movementSpeed * Time.deltaTime;

            if (Input.GetMouseButtonDown(0)) //Get initial values
            {
                mouseTempX = (Input.mousePosition.x / Screen.width) * sensitivity;

                horizontalTempPos = transform.localPosition.x;
            }

            if (Input.GetMouseButton(0)) //Compare current values to initial values and determine the position
            {
                mouseCurrentX = (Input.mousePosition.x / Screen.width) * sensitivity;

                Vector3 pos = transform.localPosition;
                pos.x = horizontalTempPos + (mouseCurrentX - mouseTempX);
                transform.localPosition = pos;

                pos =transform.localPosition;
                pos.x = Mathf.Clamp(pos.x, -3.62f, 3.62f);

                transform.localPosition = pos;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stack")
        {
            if (stackAmount != maximumStackLimit)
            {
                other.gameObject.SetActive(false);
                stackAmount++;
            }
        }
        if (other.gameObject.tag == "Coin")
        {
            other.gameObject.SetActive(false);
            PlayerPrefsManager.coins++;
            PlayerPrefsManager.UpdateCoins();
        }
        if (other.gameObject.tag == "Obstacle")
        {
            if (stackAmount > 0)
            {
                stackAmount--;
            }
        }
        if (other.gameObject.tag == "Finish")
        {
            StartCoroutine(EndGameAction());
        }
    }
    IEnumerator EndGameAction()
    {
        UI.inGamePanel.SetActive(false);
        finishCam.Priority += 2;
        yield return new WaitForSeconds(1f);
        playerAnim.SetTrigger("LevelDone");
        LevelDone();
        yield return new WaitForSeconds(1.5f);
        GetNextLevel();
    }
    void LevelDone()
    {
       GC.isLevelDone = true;
       playerCanvas.SetActive(false);
    }
    void GetNextLevel()
    {
        GC.completeLevel();
    }
    void AnimControl()
    {
        if (stackAmount == maximumStackLimit)
        {
            playerAnim.SetBool("isStackFull", true);
        }
        else
        {
            playerAnim.SetBool("isStackFull", false);
        }
    }


    private void Update()
    {
        HorizontalMovement();
        UI.stackAmountText.text = PlayerController.instance.stackAmount.ToString();
        AnimControl();
    }
}
