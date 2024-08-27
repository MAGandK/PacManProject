using UnityEngine;
using Random = UnityEngine.Random;
public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        Ghost.Chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled && !Ghost.Frightened.enabled)
        {
            int indexDirection = Random.Range(0, node.AvialableDirection.Count);

            if (node.AvialableDirection.Count > 1 && node.AvialableDirection[indexDirection] == -Ghost.MovementController.Direction)
            {
                indexDirection++;
                
                if (indexDirection >= node.AvialableDirection.Count)
                {
                    indexDirection = 0;
                }
            }
            
            Ghost.MovementController.SetDirection(node.AvialableDirection[indexDirection]);
        }
    }
}
