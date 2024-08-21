using UnityEngine;

public class Pacman : MonoBehaviour
{
   [SerializeField] private AnimatedSprite _deathSequence;
   private SpriteRenderer _spriteRender;
   private CircleCollider2D _circleCollider2D;
   private MovementController _movementController;

   private void Awake()
   {
      _spriteRender = GetComponent<SpriteRenderer>();
      _circleCollider2D = GetComponent<CircleCollider2D>();
      _movementController = GetComponent<MovementController>();
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
      {
         _movementController.SetDirection(Vector2.up);
      }
      else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
      {
         _movementController.SetDirection(Vector2.down);
      }
      else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
      {
         _movementController.SetDirection(Vector2.left);
      }
      else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
      {
         _movementController.SetDirection(Vector2.right);
      }

      float angle = Mathf.Atan2(_movementController.Direction.y, _movementController.Direction.x);
      transform.rotation = Quaternion.AngleAxis(angle* Mathf.Rad2Deg,Vector3.forward);
   }

   public void ResetState()
   {
      enabled = true;
      _spriteRender.enabled = true;
      _circleCollider2D.enabled = true;
      _deathSequence.enabled = false;
      _movementController.ResetState();
      gameObject.SetActive(true);
   }
   public void DeathSequence()
   {
      enabled = false; 
      _spriteRender.enabled = false;
      _circleCollider2D.enabled = false;
      _movementController.enabled = false; 
      _deathSequence.enabled = true;
      _deathSequence.Restart();
   }
}
