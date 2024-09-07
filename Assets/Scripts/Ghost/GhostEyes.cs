using UnityEngine;
using Zenject;

[RequireComponent(typeof(SpriteRenderer))]
public class GhostEyes : MonoBehaviour
{
    public Sprite Up;
    public Sprite Down;
    public Sprite Left;
    public Sprite Right;

    private SpriteRenderer _spriteRenderer;
    private MovementController _movementController;
    
    [Inject]
    public void Construct(MovementController movement)
    {
        _movementController = movement;
    }
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_movementController.Direction == Vector2.up)
        {
            _spriteRenderer.sprite = Up;
        }
        if (_movementController.Direction == Vector2.down)
        {
            _spriteRenderer.sprite = Down;
        }
        if (_movementController.Direction == Vector2.left)
        {
            _spriteRenderer.sprite = Left;
        }
        if (_movementController.Direction == Vector2.right)
        {
            _spriteRenderer.sprite = Right;
        }
    }
}
