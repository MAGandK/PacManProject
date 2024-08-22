using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class GhostEyes : MonoBehaviour
{
    public Sprite Up;
    public Sprite Down;
    public Sprite Left;
    public Sprite Right;

    private SpriteRenderer _spriteRenderer;
    private MovementController _movementController;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _movementController = GetComponent<MovementController>();
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
