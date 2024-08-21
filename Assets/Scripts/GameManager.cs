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

    private void Awake()
    {
        if (Instance !=null)
        {
           DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
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
        SetScore(Score);
        SetLives(3);
        NewRound();
    }

    public void PacmanEaten()
    {
       // PacmanObject.DeathSequence();
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
        if (ghost != null)
        {
            SetScore(Score + ghost.ScorePoint);
        }
    }
    
    private void GameOver()
    {
        _gameOverText.enabled = true;
        
        for (int i = 0; i < GhostObject.Length; i++)
        {
            GhostObject[i].gameObject.SetActive(false); 
        }
        
        PacmanObject.gameObject.SetActive(false);
    }
    private void ResetStart()
    {
        Debug.Log("вызов ResetStart ");

        for (int i = 0; i < GhostObject.Length; i++)
        {
            GhostObject[i].gameObject.SetActive(true); 
        }

        if (!PacmanObject.gameObject.activeSelf)
        {
            Debug.Log("Pacman не активе активация");
            PacmanObject.gameObject.SetActive(true);
        }
    }
    private void NewRound()
    {
        _gameOverText.enabled = false;
        
        foreach (Transform pellet in PelletsTransform)
        {
            pellet.gameObject.SetActive(true);
        }
        
        ResetStart();
    }
    private void SetScore(int score)
    {
       Score = score;
    }

    private void SetLives(int lives)
    {
        Lives = lives;
    }

    public void PelletsEaten(Pellets pellets)
    {
        pellets.gameObject.SetActive(false);
        SetScore(Score + pellets.Points);

        if (!HasRamainingPellets())
        {
            PacmanObject.gameObject.SetActive(false);
            Invoke(nameof(NewRound),3f);
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

    private bool HasRamainingPellets()
    {
        foreach (Transform pellet in PelletsTransform)
        {
            if (pellet.gameObject.activeSelf)
            {
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
