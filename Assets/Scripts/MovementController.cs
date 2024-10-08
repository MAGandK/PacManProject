using UnityEngine;

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
   }

   private void Start()
   {
      ResetState();
   }
   
   private void Update()
   {
      if (NextDirection != Vector2.zero) 
      {
         SetDirection(NextDirection);
      }
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
      NextDirection = Vector2.zero;
      transform.position = StartingPosition;
      Rb.isKinematic = false;
      enabled = true;
   }


   public void SetDirection(Vector2 direction, bool forced = false)
   {
      if (forced || !Occupied(direction))
      {
        this.Direction = direction;
         NextDirection = Vector2.zero;
      }
      else
      {
         NextDirection = direction;
      }
   }
   public bool Occupied(Vector2 direction)
   {
      RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * _size, _angle, direction, _distance, LayerMask);
      return hit.collider != null;
   }
}
