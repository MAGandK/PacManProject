
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
   public LayerMask ObstacleLayer;
   public readonly List<Vector2> AvialableDirection = new List<Vector2>();

   private void Start()
   {
      AvialableDirection.Clear();
      
      CheckAvialableDirection(Vector2.up);
      CheckAvialableDirection(Vector2.down);
      CheckAvialableDirection(Vector2.left);
      CheckAvialableDirection(Vector2.right);
   }

   private void CheckAvialableDirection(Vector2 direction)
   {
      RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, direction, 1f, ObstacleLayer);
      if (hit.collider == null)
      {
         AvialableDirection.Add(direction);
      }
   }
}
