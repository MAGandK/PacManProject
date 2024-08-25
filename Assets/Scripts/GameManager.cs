using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Ghost[] GhostObject;
    public Pacman PacmanObject;
    public Transform PelletsTransform;

    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _lives;
    [SerializeField] private TMP_Text _gameOverText;

    private int _ghostMultiplayer = 1;
    public int Score { get; private set; }
    public int Lives { get; private set; }
    
    public float DelayTime = 2f;
    private bool isGameStarted = false;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (!isGameStarted)
        {
            NewGame();
            isGameStarted = true;
        }
    }

    private void Update()
    {
        if (Lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0); // Обнулить счет при новой игре
        SetLives(3);
        _gameOverText.enabled = false;

        NewRound();
    }

    public void PacmanEaten()
    {
        PacmanObject.DeathSequence();

        SetLives(Lives - 1);

        if (Lives > 0)
        {
            Invoke(nameof(ResetStart), DelayTime);
        }
        else
        {
            GameOver();
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void GhostEaten(Ghost ghost)
    {
        SetScore(Score + ghost.ScorePoint);
    }

    private void GameOver()
    {
        _gameOverText.enabled = true;

        foreach (var ghost in GhostObject)
        {
            ghost.gameObject.SetActive(false);
        }

        PacmanObject.gameObject.SetActive(false);
    }

    private void ResetStart()
    {
        foreach (var ghost in GhostObject)
        {
            ghost.gameObject.SetActive(true);
        }

        PacmanObject.ResetState(); // Перезагружаем состояние Pacman
    }

    private void NewRound()
    {
        if (!HasRemainingPellets())
        {
            foreach (Transform pellet in PelletsTransform)
            {
                pellet.gameObject.SetActive(true);
            }
            Invoke(nameof(ResetStart), 1f); 
        }
        else
        {
            ResetStart();
        }
    }

    private void SetScore(int score)
    {
        Score = score;
        _score.SetText(Score.ToString());
    }

    private void SetLives(int lives)
    {
        Lives = lives;
        _lives.SetText(Lives.ToString());
    }

    public void PelletsEaten(Pellets pellet)
    {
        pellet.gameObject.SetActive(false);
            
        SetScore(Score + pellet.Points);

        if (!HasRemainingPellets())
        {
            PacmanObject.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }

    public void PowerPelletsEaten(PowerPellets pellets)
    {
        for (int i = 0; i < GhostObject.Length; i++)
        {
            GhostObject[i].Frightened.Enable(pellets.duration);
        }
        PelletsEaten(pellets);
        // CancelInvoke(nameof(ResetGhost));
        // Invoke(nameof(ResetGhost), pellets.duration);
    }

    private bool HasRemainingPellets()
    {
        for (int i = 0; i < PelletsTransform.childCount; i++)
        {
            if (PelletsTransform.GetChild(i).gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
    // private void ResetGhost()
    // {
    //     _ghostMultiplayer = 1;
    // }
}
