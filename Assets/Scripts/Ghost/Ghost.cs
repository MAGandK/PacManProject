using UnityEngine;

public class Ghost : MonoBehaviour
{
    public MovementController MovementController { get; private set; }
    public GhostHome Home { get; private set; }
    public GhostFrightened Frightened { get; private set; }
    public GhostChase Chase { get; private set; }
    public GhostScatter Scatter { get; private set; }
    
    public GhostBehavior InitialBehavior;
    public Transform Target;
    public int ScorePoint = 200;

    private void Awake()
    {
        MovementController = GetComponent<MovementController>();
        Home = GetComponent<GhostHome>();
        Frightened = GetComponent<GhostFrightened>();
        Chase = GetComponent<GhostChase>();
        Scatter = GetComponent<GhostScatter>();
    }

    private void Start()
    {
        ResetState();
    }

    private void ResetState()
    {
        gameObject.SetActive(true);
        MovementController.ResetState();
        
        Frightened.Disable();
        Chase.Disable();
        Scatter.Enable();

        if (Home != InitialBehavior)
        {
            Home.Disable();
        }

        if (InitialBehavior != null)
        {
            InitialBehavior.Enable();
        }
    }
    public void SetPosition(Vector3 position)
    {
        position.z = transform.position.z;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PacMan"))
        {
            if (Frightened.enabled)
            {
                GameManager.Instance.GhostEaten(this);
            }
            else
            {
                GameManager.Instance.PacmanEaten();
                ResetState();
            }
        }
    }
}
