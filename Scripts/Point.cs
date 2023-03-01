using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
     public ParticleSystem particle;
     public Image image;

     public Image closeImage;

     public void SetSprite(Sprite sprite)
     {
          image.sprite = sprite;
     }

     public void Close()
     {
          closeImage.enabled = false;

     }

     public void PlayParticles()
     {
          particle.Play();
     }
}
