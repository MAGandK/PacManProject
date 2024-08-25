using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
   public float Speed; 
   public float SpeedObject;
   public Vector2 InitDirection;
   public LayerMask LayerMask;

   private float _size = 0.75f;
   private float _angle = 0.0f;
   private float _distance = 1.5f;
   
   public Rigidbody2D Rb { get; private set; }
   public Vector2 Direction { get; private set; }
   public Vector2 NextDirection { get; private set; }
   public Vector3 StartingPosition { get; private set; }

   private void Awake()
   {
      Rb = GetComponent<Rigidbody2D>();
      StartingPosition = transform.position;
      InitDirection = Vector2.zero;
   }

   private void Start()
   {
      ResetState();
   }
   private void FixedUpdate()
   {
      Vector2 position = Rb.position;
      Vector2 translation = Direction * (Speed * SpeedObject * Time.fixedDeltaTime);
      
      Rb.MovePosition(position + translation);
   }

   public void ResetState()
   {
      SpeedObject = 1;
      Direction = InitDirection;
      transform.position = StartingPosition;
      Rb.isKinematic = false;
      enabled = true;
   }


   public void SetDirection(Vector2 direction, bool forced = false)
   {
      if (forced ||!Occuped(direction))
      {
         Direction = direction;
         NextDirection = Vector2.zero;
      }
      else
      {
         NextDirection = direction;
      }
   }

   public bool Occuped(Vector2 direction)
   {
      RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * _size, _angle, direction, _distance, LayerMask);
      return hit.collider;
   }
}
