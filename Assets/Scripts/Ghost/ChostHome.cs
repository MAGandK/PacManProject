using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
public class GhostHome : GhostBehavior
{
   public Transform Inside;
   public Transform Outside;

   private void OnEnable()
   {
      StopAllCoroutines();
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
      if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
      {
          Ghost.MovementController.SetDirection(-Ghost.MovementController.Direction);
      }
   }

   private IEnumerator ExitTransition()
   {
      Ghost.MovementController.SetDirection(Vector2.up,true);
      Ghost.MovementController.Rb.isKinematic = true;
      Ghost.MovementController.enabled = false;

       Vector3 position = transform.position;

       float duration = 0.5f;
       float elapsed = 0f;

       while (elapsed < duration)
       {
           Ghost.SetPosition(Vector3.Lerp(position, Inside.position, elapsed / duration));
           elapsed += Time.deltaTime;
           yield return null;
       }
       
       elapsed = 0f;
       
       while (elapsed < duration)
       {
           Ghost.SetPosition(Vector3.Lerp(Inside.position, Outside.position, elapsed / duration));
           elapsed += Time.deltaTime;
           yield return null;
       }
       Ghost.MovementController.Rb.isKinematic = false;
       Ghost.MovementController.enabled = true;
       Ghost.MovementController.SetDirection(new Vector2(Random.value < 0.5f ? -1f: 1f, 0f ), true);
   }

   private void OnDisable()
   {
       if (gameObject.activeInHierarchy)
       {
           StartCoroutine(ExitTransition());
       }
   }
}
