public class PowerPellets : Pellets
{
   public float duration = 8f; 

   protected override void Eat()
   {
      GameManager.Instance.PowerPelletsEaten(this);
   }
}
