
using System;
using UnityEngine;
using UnityEngine.UI;
public class Player : Singleton<Player>
{
     public SkinnedMeshRenderer meshRenderer;
     public Image Slider;
     public Intractable[] interactables;
     public Charackter charackter;
     public Sprite forwardIcon;
     public Sprite circleIcon;
     public Vector3 forwardScale = new Vector3(0.465623468f, 0.982856214f, 0.465623468f);
     public WhiteMask whiteMask;
     public float forwardIconSpeed = 0.2f;

     public void SetBlendSheep(int index, float value) => meshRenderer.SetBlendShapeWeight(index, value);

     private void Start()
     {
          int blendShapeCount = meshRenderer.sharedMesh.blendShapeCount;
          for (int i = 0; i < blendShapeCount; i++)
               if (meshRenderer.GetBlendShapeWeight(i) > 0)
                    index += 1;
     }

     public int GetBlendShapeCount() => meshRenderer.sharedMesh.blendShapeCount;

     public float GetBlendShapeWeight(int index) => meshRenderer.GetBlendShapeWeight(index);
     float index;
     public float AllValueIsZero()
     {
          float val = 0;
          int blendShapeCount = meshRenderer.sharedMesh.blendShapeCount;
          for (int i = 0; i < blendShapeCount; i++)
               val += meshRenderer.GetBlendShapeWeight(i);
          return val;
     }
     public void SliderController(float val)
     {
          if (val != 0) return;
          float v = (1 / index);
          Slider.fillAmount += v;
          Slider.fillAmount = Mathf.Min(1, Slider.fillAmount);
     }

     public void ResetSlider() => Slider.fillAmount = 0;
     public CharackterType charackterType;

     public void InitializeBlendSheep(Elements[] elements)
     {

          for (int i = 0; i < elements.Length; i++)
          {
               for (int j = 0; j < elements[i].elements.Length; j++)
               {
                    float val = elements[i].elements[j].value;
                    if (val > 0)
                    {
                         Debug.Log("InitializeBlendSheep " + elements[i].elements[j].name);
                         meshRenderer.SetBlendShapeWeight(j, 100);
                         break;
                    }
               }
          }
     }

     public void SetBlendSheep(Element[] elements)
     {

          for (int i = 0; i < elements.Length; i++)
          {
               meshRenderer.SetBlendShapeWeight(i, elements[i].value);
               float val = meshRenderer.GetBlendShapeWeight(i);

               if (val > 0)
               {
                    for (int j = 0; j < interactables.Length; j++)
                    {
                         if (interactables[j].charackterType == elements[i].charackterType)
                         {
                              if ((interactables[j].intractableType == elements[i].intractableType) ||
                                        elements[i].intractableType == IntractableType.UNDEFINED)
                              {
                                   interactables[j].enabled = true;
                                   interactables[j].Open(true);

                                   if (interactables[j].charackterType == CharackterType.Kulak) Player.Instance.whiteMask.Deactive();
                                   else Player.Instance.whiteMask.Active();

                                   charackter.SaveBlendState(elements[i], val, j);

                                   StartCoroutine(
                                        charackter.CameraSetter(interactables[j].SetCameraPosition(),
                                        interactables[j], elements[i].indexMoveType));
                                   break;
                              }
                         }
                    }
                    break;
               }
          }
     }

     public void SettingsBlendSheep(Element elements, float val, int j)
     {
          charackterType = elements.charackterType;
          interactables[j].ValuesController(val);
          interactables[j].Open(true);
          interactables[j].StartPosition();
          interactables[j].ImagePosition(elements.indexMoveType);
     }


}
