using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellets : Pellets
{
   public float duration = 8f; //время действия клубники

   protected override void Eat()
   {
      GameManager.Instance.PowerPelletsEaten(this);
   }
}
