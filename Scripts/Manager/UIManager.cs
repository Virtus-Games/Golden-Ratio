using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using System;

public class UIManager : Singleton<UIManager>
{
     #region Data
     private const string Music = "Music";
     private const string Vibrate = "Vibrate";
     public GameObject StartPanel, MarketPanel, PlayPanel;
     public GameObject SettingsPanel, VictoryPanel, DefeatPanel;

     [Space(5)]
     [Header("Level and Coins Panel")]
     public TextMeshProUGUI levelName;
     public TextMeshProUGUI CoinText;

     [Header("Coins Panel")]
     [Space(5)]
     public TextMeshProUGUI TotalMoneyText;

     internal bool SettingsDataController(SettingsIcon iconData)
     {
          return iconData.isStatus;
     }

     [Tooltip("Victory Panel Coin Movement")]
     public GameObject CoinPrefab;
     public GameObject MoneyBox;
     public Transform CoinParent;
     public Transform MoneyImage;
     private int _money;
     private int _totalMoney;

     [Header("UserNameInputPanel")]
     [SerializeField] GameObject nameInputObject;
     [SerializeField] TMP_InputField nameInputField;
     [Space(5)]

     public WinSO winSO;

     #endregion

     private void Start()
     {
          UpdateGameState(GAMESTATE.START);
          UpdateCoin();


     }

     private void OnEnable()
     {
          GameManager.OnGameStateChanged += UpdateGameState;
          LevelManager.OnLevelLoaded += UpdateLevel;
     }
     private void OnDisable()
     {
          GameManager.OnGameStateChanged -= UpdateGameState;
          LevelManager.OnLevelLoaded -= UpdateLevel;
     }

     public GameObject[] moneys;
     public Transform normalMoneyParent;


     public void MoneyToMoneyParent()
     {
          StartCoroutine(MoveMoneyToMoneyParent());
     }


     IEnumerator MoveMoneyToMoneyParent()
     {
          yield return new WaitForSeconds(0.1f);

          foreach (var item in moneys)
          {
               item.SetActive(true);
               item.transform.localScale = Vector3.one;
               while (Vector2.Distance(item.transform.position, MoneyImage.position) > 0.1f)
               {
                    item.transform.DOLocalMove(MoneyImage.localPosition, 1f).onComplete += () =>
                    {
                         item.transform.SetParent(MoneyImage);
                         item.transform.localPosition = Vector3.zero;
                         item.gameObject.SetActive(false);
                    };
                    yield return null;
               }
          }

          GameManager.Instance.UpdateGameState(GAMESTATE.START);
          LevelManager.Instance.NextLevel();
     }

     #region  UI MANAGER

     private void UpdateLevel(bool arg0)
     {
          if (arg0)
          {

          }
     }

     private void UpdateGameState(GAMESTATE switchState)
     {
          switch (switchState)
          {
               case GAMESTATE.START:
                    StartMethod();
                    break;
               case GAMESTATE.PLAY:
                    PlayMode();
                    break;
               case GAMESTATE.VICTORY:
                    VictoryMode();
                    break;
               case GAMESTATE.MARKET:
                    MarketMode();
                    break;
               case GAMESTATE.DEFEAT:
                    DefautMode();
                    break;
               default:
                    UpdateUI(null);
                    break;

          }
     }



     private void MarketMode()
     {
          UpdateUI(MarketPanel);
          BannerController(false);
     }

     private void StartMethod()
     {
          if (!PlayerPrefs.HasKey("Username"))
          {
               nameInputObject.SetActive(true);
               return;
          }

          foreach (var item in moneys)
          {
               item.transform.SetParent(normalMoneyParent);
               item.transform.localPosition = Vector3.zero;
               item.gameObject.SetActive(false);
          }

          _money = 0;
          _totalMoney = 0;
          UpdateUI(StartPanel);
     }

     private void VictoryMode()
     {
          _totalMoney = _money;
          //CoinsGoToMoneyBox();
          BannerController(true);
     }

     private void DefautMode()
     {
          //AdmonController.Instance.DefeatIntersitial();
          UpdateUI(DefeatPanel);
          BannerController(true);
     }

     private void BannerController(bool v)
     {

     }

     private void PlayMode()
     {
          levelName.SetText("Level " + PlayerData.playerData.currentLevel.ToString());
          UpdateUI(PlayPanel);
          BannerController(false);
     }

     public void UpdateUI(GameObject obj)
     {
          StartPanel.SetActive(false);
          MarketPanel.SetActive(false);
          PlayPanel.SetActive(false);
          VictoryPanel.SetActive(false);
          DefeatPanel.SetActive(false);
          SettingsPanel.SetActive(false);

          if (obj != null)
               obj.SetActive(true);

     }

     public void UsernameInputOKButton()
     {
          PlayerPrefs.SetString("Username", nameInputField.text);
          nameInputObject.SetActive(false);
          Start();
     }

     #endregion

     #region Coins
     public void UpdateCoin()
     {


         
          CoinText.text = PlayerData.playerData.totalMoney.ToString();
     }

     public int GetCoin() => PlayerData.playerData.totalMoney;
     public void SetMoneyCalculate(int getMoney) => _money += getMoney;

     private void SetCoin(int v)
     {
          _totalMoney += v;
          CoinText.SetText((GetCoin() + v).ToString());
     }


}
#endregion

