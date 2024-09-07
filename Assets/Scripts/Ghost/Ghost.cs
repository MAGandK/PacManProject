using UnityEngine;
using Zenject;
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
    private bool _isActiv = true;
    
    [Inject]
    public void Construct(MovementController movementController, GhostHome home, GhostFrightened frightened, GhostChase chase, GhostScatter scatter)
    {
        MovementController = movementController;
        Home = home;
        Frightened = frightened;
        Chase = chase;
        Scatter = scatter;
    }
    private void OnEnable()
    {
        if (!_isActiv)
        {
            ResetState();
        }
    }
    private void Start()
    {
        ResetState();
    }
    public void ResetState()
    {
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
    private void OnDisable()
    {
        _isActiv = false;
    }
}
