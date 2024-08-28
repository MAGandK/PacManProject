using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
   public SpriteRenderer SpriteRenderer { get; private set; }
   public Sprite[] Sprites;
   public float AnimTime;
   public int AnimFrame { get; set; }
   public bool Loop = true;
   
   private void Awake()
   {
      SpriteRenderer = GetComponent<SpriteRenderer>();
   }
   
   private void Start()
   {
      InvokeRepeating(nameof(Advence), AnimTime, AnimTime);
   }
   
   private void Advence()
   {
      if (!SpriteRenderer.enabled)
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
        SpriteRenderer.sprite = Sprites[AnimFrame];
      }
   }
   
   public void Restart()
   {
      SpriteRenderer.enabled = true;
      AnimFrame = -1;
      Advence();
   }
}
