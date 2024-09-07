using UnityEngine;
using Zenject;

[RequireComponent(typeof(Ghost))]
public class GhostBehavior : MonoBehaviour
{
    public Ghost Ghost { get; private set; }
    
    public float Duration;

    [Inject]
    private void Construct(Ghost ghost)
    {
        Ghost = ghost;
    }
    // private void Awake()
    // {
    //     Ghost = GetComponent<Ghost>();
    // }

    public void Enable()
    {
        Enable(Duration);    
    }
    
    public virtual void Enable(float duration)
    {
        enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        enabled = false;
        CancelInvoke();
    }
}
