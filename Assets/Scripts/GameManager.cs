using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int CollectedTotal;

    [SerializeField]
    private bool isGameRunning = false;

    public AudioPlayer audioPlayer;

    public PlayerController playerController;

    public Animator WinAnimator;
    public Animator LoseAnimator;

    public GameObject Collectible;

    public Text Score;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 0)
        {
            isGameRunning = true;

            CollectedTotal = 0;

            audioPlayer = GetComponent<AudioPlayer>();
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            WinAnimator = GameObject.FindGameObjectWithTag("WinScreen").GetComponent<Animator>();
            LoseAnimator = GameObject.FindGameObjectWithTag("LoseScreen").GetComponent<Animator>();

            Score = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();

            StartCoroutine(InstantiateCollectibles());
        }

        Debug.Log("Scene Loaded");
    }

    public void AddCollectible()
    {
        CollectedTotal++;

        Score.text = CollectedTotal.ToString();

        if (CollectedTotal >= 3)
        {
            StopCoroutine("InstantiateCollectibles");

            StartCoroutine(WinGameCongratulations());
        }
    }

    public IEnumerator WinGameCongratulations()
    {
        isGameRunning = false;

        WinAnimator.SetBool("IsGameWon", true);

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(0);
    }

    public int GetCollectedTotal()
    {
        return CollectedTotal;
    }

    public void EndGame()
    {
        isGameRunning = false;

        StopCoroutine("InstantiateCollectibles");

        StartCoroutine(LostGameScreen());
    }

    public IEnumerator LostGameScreen()
    {
        LoseAnimator.SetBool("IsGameLost", true);

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(0);
    }

    public IEnumerator InstantiateCollectibles()
    {
        while (GetIsGameRunning())
        {
            InstantiateCollectible();

            yield return new WaitForSeconds(.5f);
        }
    }

    public void InstantiateCollectible()
    {
        float spawnY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

        var randomScreenPosition = new Vector2(spawnX, spawnY);

        Instantiate(Collectible, randomScreenPosition, Quaternion.identity);
    }

    public bool GetIsGameRunning()
    {
        return isGameRunning && !(LoseAnimator.GetBool("IsGameLost") || WinAnimator.GetBool("IsGameWon"));
    }
}
