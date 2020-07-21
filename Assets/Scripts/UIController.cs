using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private int score;
    private int gems;
    private float health = 1.0f;
    private float maxHealth = 5.0f;
    private int popUpsOpen = 0;
    [SerializeField] private Text scoreValue;
    [SerializeField] private Text objective;
    [SerializeField] private Text numJewels;
    [SerializeField] private Image healthBar;
    [SerializeField] private OptionsPopup optionsPopup;
    [SerializeField] private Image crossHair;
    [SerializeField] private GameOverScreen gameOverScreen;

    void Awake()
    {  
        Messenger.AddListener(GameEvent.JEWEL_COLLECTED, OnJewelCollected);
        Messenger<float>.AddListener(GameEvent.HEALTH_CHANGED, OnHealthChanged);
        Messenger.AddListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
        Messenger.AddListener(GameEvent.POPUP_OPENED, OnPopupOpen);
        Messenger.AddListener(GameEvent.PLAYER_DEAD, OnPlayerDead);
        Messenger.AddListener(GameEvent.RESTART_GAME, OnRestartGame);
        Messenger.AddListener(GameEvent.POINT, OnPoint);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.JEWEL_COLLECTED, OnJewelCollected);
        Messenger<float>.RemoveListener(GameEvent.HEALTH_CHANGED, OnHealthChanged);
        Messenger.RemoveListener(GameEvent.POPUP_OPENED, OnPopupOpen);
        Messenger.RemoveListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
        Messenger.RemoveListener(GameEvent.PLAYER_DEAD, OnPlayerDead);
        Messenger.RemoveListener(GameEvent.RESTART_GAME, OnRestartGame);
        Messenger.RemoveListener(GameEvent.POINT, OnPoint);
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        score = 0;
        numJewels.text = "0/3";
        scoreValue.text = score.ToString();
        healthBar.fillAmount = 0.5f;
        healthBar.color = Color.green;   
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Escape) && popUpsOpen == 0)
        {
            optionsPopup.Open();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        crossHair.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        crossHair.gameObject.SetActive(true);
    }

    void OnRestartGame()
    {
        ResumeGame();
        SceneManager.LoadScene(0);
    }

    void OnPopupClosed()
    {
        //Debug.Log("on closed" + popUpsOpen);
        popUpsOpen -= 1;
        if (popUpsOpen == 0)
        {
            ResumeGame();
        }
        Messenger<int>.Broadcast(GameEvent.UI_POPUP_CLOSED, popUpsOpen); 
    }

    void OnPopupOpen()
    {
        if (popUpsOpen == 0)
        {
            PauseGame();
        }
        popUpsOpen += 1;
        Messenger<int>.Broadcast(GameEvent.UI_POPUP_OPENED, popUpsOpen);
    }

    void OnPlayerDead()
    {
        PauseGame();
        gameOverScreen.Open();
    }

    private void OnPoint()
    {
        score += 1;
        scoreValue.text = score.ToString();
    }

    private void OnHealthChanged(float currentHealth)
    {
        health = (currentHealth / maxHealth);
        Debug.Log("current health: " + currentHealth);
        healthBar.color = Color.Lerp(Color.red, Color.green, health);
        Debug.Log(health);
        healthBar.fillAmount = health;
    }

    private void OnJewelCollected()
    {
        Debug.Log("gem collected");
        gems += 1;
        numJewels.text = (gems.ToString() + "/3");

        if (gems == 3)
        {
            objective.text = "All Gems Found!";
        }
    }
}
