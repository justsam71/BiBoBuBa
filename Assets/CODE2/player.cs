using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class player : MonoBehaviour
{
    private SpriteRenderer _sr;
    [SerializeField] private GameManager gm;

    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private TMP_Text livesCounter;

    [SerializeField] private GameObject[] hearts;
    private float currentHearts;
    [SerializeField] private float maxHearts;
    private int currentLives;
    [SerializeField] private int maxLives;

    [SerializeField] private PlayerAnimation playerAnimation;

    [SerializeField] private Transform start;
    [SerializeField] private int minHeight; //for falling

    public int faliantsCollected = 0;
    public bool inProccessOfCarrying = false;

    public bool shieldOn = false;

    private GameObject[] faliants;


    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
                print(_sr);
        currentHearts = maxHearts;
        currentLives = maxLives;
        UpdateHP();
    }

    private void Update()
    {

        if (this.transform.position.y < minHeight)
            Die();
    }

    public void TakeDamage(float amount)
    {
        print(_sr);
        if (!shieldOn)
        {
            _sr.color = Color.red;
            Invoke("ColorBack", 0.2f);

            currentHearts -= amount;
            currentHearts = Mathf.Clamp(currentHearts, 0, maxHearts);
            UpdateHP();
            
            if (currentHearts == 0)
            {
                Die();
            }
        }
        else
        {
            _sr.color = Color.green;
            Invoke("ColorBack", 0.2f);
        }
    }

    // Call this method to heal
    public void Heal(float amount)
    {
        currentHearts += amount;
        currentHearts = Mathf.Clamp(currentHearts, 0, maxHearts);
        UpdateHP();
    }

    public void Die()
    {
        faliants = GameObject.FindGameObjectsWithTag("faliants");
        for (int i = 0; i < faliants.Length; i++)
        {
            faliants[i].GetComponent<Falliant>().ReturnToTheStart();
        }

        if (currentLives > 1)
        {
            //playerAnimation.SetAnimation("player_die");
            Respawn();
        }
        else
        {
            GameOver();
        }

        currentLives--;
        if (currentLives >= 0)
            livesCounter.text = $"x{currentLives}";
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        this.gameObject.SetActive(false);
        gm.gameOver = true;
    }

    public void Respawn()
    {
        currentHearts = maxHearts+1;
        UpdateHP();
        this.transform.position = start.transform.position;
    }

    void ColorBack()
    {
        _sr.color = Color.white;
    }

    void UpdateHP()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i + 1 <= currentHearts)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }
}
