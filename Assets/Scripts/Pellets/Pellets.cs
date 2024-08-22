using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class Pellets : MonoBehaviour
{
    public int Points = 10;

    protected virtual void Eat()
    {
        GameManager.Instance.PelletsEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PacMan"))
        {
            Eat();
        }
    }
}
