using UnityEngine;

public class PowerPellets : Pellets
{
   public float duration = 8f;

   protected override void Eat()
   {
      GameManager.Instance.PelletsEaten(this);
   }
}
