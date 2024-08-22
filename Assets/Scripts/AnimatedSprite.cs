using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
   public SpriteRenderer SpriteRenderer { get; private set; }
   public Sprite[] Sprites;
   public float AnimTime;
   public int AnimFrame { get; private set; }
   public bool Loop = true;

   private void Awake()
   {
      this.SpriteRenderer = GetComponent<SpriteRenderer>();
   }

   private void Start()
   {
      InvokeRepeating(nameof(Advence), AnimTime, AnimTime);
   }

   private void Advence()
   {
      if (!this.SpriteRenderer.enabled)
      {
         return;
      }

      AnimFrame++;
   
      if (AnimFrame >= Sprites.Length && Loop)
      {
         AnimFrame = 0;
      }

      if (AnimFrame >= 0 && AnimFrame < Sprites.Length)
      {
         this.SpriteRenderer.sprite = Sprites[AnimFrame];
      }
   }

   public void Restart()
   {
      AnimFrame = -1;
      Advence();
   }
}
