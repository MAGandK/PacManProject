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
        NewGame();
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
        NewRound();
    }
    private void NewRound()
    {
        _gameOverText.enabled = false;

        foreach (Transform pellet in PelletsTransform) {
            pellet.gameObject.SetActive(true);
        }

        ResetStart();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
  
    

    private void ResetStart()
    {
        for (int i = 0; i < GhostObject.Length; i++) {
            GhostObject[i].ResetState();
        }

        PacmanObject.ResetState(); // Перезагружаем состояние Pacman
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
    private void SetScore(int score)
    {
        Score = score;
        _score.SetText(Score.ToString());
    }

    private void SetLives(int lives)
    {
        Lives = lives;
        _lives.SetText("x" + Lives.ToString());
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
    public void GhostEaten(Ghost ghost)
    {
        
        SetScore(Score + ghost.ScorePoint);

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
        CancelInvoke(nameof(ResetGhost));
        Invoke(nameof(ResetGhost), pellets.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in PelletsTransform)
        {
            if (pellet.gameObject.activeSelf) {
                return true;
            }
        }

        return false;
    }
    private void ResetGhost()
    {
        _ghostMultiplayer = 1;
    }
}
