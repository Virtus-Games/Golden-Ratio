using UnityEngine.Events;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[System.Serializable]
public class PlayerDataContainer
{
     public int currentLevel = 1;
     public int totalMoney = 0;
     // public MarketDataContainer marketData;
     public int currentWinGiftItem = 0;

}

[System.Serializable]
public class MarketDataContainer
{
     public List<Item> items = new List<Item>();

     public void addItem(Item item)
     {
          // FIXME: NOT WORKING
          if (!items.Contains(item))
               items.Add(item);

     }
}
public class PlayerData : Singleton<PlayerData>
{
     private const string Filename = "playerData.dat";
     public static PlayerDataContainer playerData;
     public static UnityAction<PlayerDataContainer> onDataChanged;

     [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
     static void BeforeAwake()
     {
          PlayerDataContainer player = BinarySerializer.Load<PlayerDataContainer>(Filename);
          playerData = player;
          onDataChanged?.Invoke(playerData);
     }

     public void EmptyPlayerData()
     {
          playerData = new PlayerDataContainer();
          Save();
     }

     public void AddMoney(int value){
          playerData.totalMoney += value;
          Save();
          UIManager.Instance.UpdateCoin();
     }


     public void Save()
     {
          BinarySerializer.Save(playerData, Filename);
          onDataChanged?.Invoke(playerData);
     }

     public void Load()
     {
          PlayerDataContainer player = BinarySerializer.Load<PlayerDataContainer>(Filename);
          playerData = player;
          onDataChanged?.Invoke(playerData);
     }

}

#if UNITY_EDITOR


[CustomEditor(typeof(PlayerData))]
public class PlayerDataEditor : Editor
{
     public override void OnInspectorGUI()
     {
          base.OnInspectorGUI();
          PlayerData customer = (PlayerData)target;

          GUILayout.BeginHorizontal();
          if (GUILayout.Button("Save"))
               customer.Save();

          if (GUILayout.Button("Load"))
               customer.Load();

          GUILayout.EndHorizontal();

          if (GUILayout.Button("Delete Data"))
               customer.EmptyPlayerData();
     }
}
#endif