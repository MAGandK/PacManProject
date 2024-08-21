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
            int index = Random.Range(0, node.AvialableDirection.Count);

            if (node.AvialableDirection.Count > 1 && node.AvialableDirection[index] == -Ghost.MovementController.Direction )
            {
                index++;

                if (index >= node.AvialableDirection.Count)
                {
                    index = 0;
                }
            }
            
            Ghost.MovementController.SetDirection(node.AvialableDirection[index]);
        }
    }
}
