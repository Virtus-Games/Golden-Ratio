using UnityEngine;

public class Gym : MonoBehaviour
{
     public SkinnedMeshRenderer[] Shirt;
     public SkinnedMeshRenderer[] Pants;
     public SkinnedMeshRenderer[] Hairs;
     public GameObject[] bodys;
     void Start()
     {
          Close();
          int randShirt = Random.Range(0, Shirt.Length);
          int randPants = Random.Range(0, Pants.Length);
          int randHairs = Random.Range(0, Hairs.Length);
          Shirt[randShirt].gameObject.SetActive(true);
          Shirt[randShirt].material.color = Random.ColorHSV();
          Pants[randPants].gameObject.SetActive(true);
          Pants[randPants].material.color = Random.ColorHSV();
          Hairs[randHairs].gameObject.SetActive(true);
          Hairs[randHairs].material.color = Random.ColorHSV();

     }

     void Close()
     {
          for (int i = 0; i < Shirt.Length; i++)
               Shirt[i].gameObject.SetActive(false);
          for (int i = 0; i < Pants.Length; i++)
               Pants[i].gameObject.SetActive(false);
          for (int i = 0; i < Hairs.Length; i++)
               Hairs[i].gameObject.SetActive(false);
     }

     public void CloseAll()
     {
          foreach (var item in Shirt) item.gameObject.SetActive(false);
          foreach (var item in Pants) item.gameObject.SetActive(false);
          Color randomColor = Random.ColorHSV();
          foreach (var item in bodys){
           item.GetComponent<SkinnedMeshRenderer>().material.color = randomColor;
           item.SetActive(true);
          }
     }
}
