using UnityEngine;

public class GhostFrightened : GhostBehavior
{
   public SpriteRenderer Body;
   public SpriteRenderer Eyes;
   public SpriteRenderer Blue;
   public SpriteRenderer White;

   private bool _isEaten;

   public override void Enable(float duration)
   {
      base.Enable(duration);
      Body.enabled = false;
      Eyes.enabled = false;
      Blue.enabled = true;
      White.enabled = false;

      Invoke(nameof(Flash), duration / 2f);
   }

   public override void Disable()
   {
      base.Disable();
      Body.enabled = true;
      Eyes.enabled = true;
      Blue.enabled = false;
      White.enabled = false;
   }

   private void Eaten()
   {
      _isEaten = true;
      Ghost.SetPosition(Ghost.Home.Inside.position);
      Ghost.Home.Enable(Duration);
      
      Body.enabled = false;
      Eyes.enabled = true;
      Blue.enabled = false;
      White.enabled = false;
   }

   private void Flash()
   {
      if (!_isEaten)
      {
         Blue.enabled = false;
         White.enabled = true;
         
         White.GetComponent<AnimatedSprite>().Restart();
      }
   }

   private void OnEnable()
   {
      Blue.GetComponent<AnimatedSprite>().Restart();
      Ghost.MovementController.SpeedObject = 0.5f;
      _isEaten = false;
   }

   private void OnDisable()
   {
      Ghost.MovementController.SpeedObject = 1f;
      _isEaten = false;
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      Node node = GetComponent<Node>();

      if (node != null && enabled)
      {
         Vector2 direction = Vector2.zero;
         float maxDistance = float.MinValue;

         foreach (Vector2 availableDirection in node.AvialableDirection)
         {
            Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
            float distanse = (Ghost.Target.position - newPosition).sqrMagnitude;

            if (distanse > maxDistance)
            {
               direction = availableDirection;
               maxDistance = distanse;
            }
         }
         Ghost.MovementController.SetDirection(direction);
      }
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.layer == LayerMask.NameToLayer("PacMan"))
      {
         if (enabled)
         {
            Eaten();
         }
      }
   }
}
