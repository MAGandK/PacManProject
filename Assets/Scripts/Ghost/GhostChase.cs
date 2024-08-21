using System;
using UnityEngine;

public class GhostChase : GhostBehavior
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Node node = collider.GetComponent<Node>();
        
        if (node != null && enabled && !Ghost.Frightened.enabled)
        {
            Vector2 direction = Vector2.zero;

            float minDistance = float.MaxValue;

            foreach (Vector2 avialable in node.AvialableDirection)
            {
                Vector3 newPosition = transform.position + new Vector3(avialable.x, avialable.y);

                float distance = (Ghost.Target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = avialable;
                    minDistance = distance;
                }
            }
            
            Ghost.MovementController.SetDirection(direction);
        }
    }

    private void OnDisable()
    {
        Ghost.Scatter.Enable();
    }
}
