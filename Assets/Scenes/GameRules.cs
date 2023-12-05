using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Text;
using System;

public class GameRules : MonoBehaviour
{
    public enum ActionType { Waiting, PlayCard, InMenu, Pass, DeckMode};
    public ActionType ActionChange;
    public int[] Requirement = new int[]{ 9999999, 1000, 3000, 10000, 50000, 9999999 };
    static public int Level;
    public int[] SeedsAmount = new int[]{ 0, 1, 5, 10, 50, 100, 500, 1000 };
    public int[] DeckBackup = new int[] {1,1,1,2,3,3,3,4,5,6,7,8,9,9,10,11,12,13};
    public ArrayList DeckBackup2 = new ArrayList();
    public ArrayList Hand = new ArrayList();
    public ArrayList Deck = new ArrayList();
    public List<GameObject> TargetList = new List<GameObject>();
    public int Totalnumber = 0;
    public int Score = 0;
    public int Reset = 0;
    public int PlaythisRound = 0;
    int Copy = 0;
    int Pass = 0;
    int CardMode = 0;
    public Text ScoreString;
    public Text LevelText;
    public Text Debug1;
    public Text DeckAmount;
    public Text ResetTime;
    public Text DebugText1;
    public Text DebugText2;
    public Text DebugText3;
    public Text DebugText4;
    public GameObject PickText;
    public GameObject UsingCard;
    public GameObject CCC;
    public GameObject Menu;

    public GameObject Zone;
    public GameObject Farm;
    public GameObject Fail;
    public GameObject NextButton;
    public GameObject NextLevel;
    public GameObject LevelData;
    public GameObject CardGO;
    public GameObject CardUI;
    public GameObject ShadowsCard;
    public GameObject DeckIMG;
    public GameObject DeckStuff;
    public GameObject DeckCard;
    public GameObject DeckUI;

    public GameObject MusicOnClick;
    public GameObject MusicPicked;
    public GameObject MusicBuff;
    public GameObject MusicDebuff;
    public GameObject MusicPass;
    public GameObject MusicFail;
    public GameObject MusicCantPicked;

    // Start is called before the first frame update
    void Start()
    {
        LevelData = GameObject.Find("LevelSave");
        for (int i = 0; i < LevelData.GetComponent<Saveit>().PlantAmount; i++)
        {
            Instantiate(Zone, Farm.transform);
        }
        ActionChange = ActionType.Waiting;
        Debug.Log("GameRule專案啟動了");
        NextLevel.SetActive(false);
        RoundStart();

        if(LevelData.GetComponent<AudioSource>().clip != Resources.Load<AudioClip>("game")){
            LevelData.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("game");
            LevelData.GetComponent<AudioSource>().Play();
        }
        

        Invoke("Yay",0.03f);
        Action();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            switch (ActionChange)
            {
                case ActionType.Waiting:
                    Debug.Log("因為按下ESC，進入選單中");
                    ActionChange = ActionType.InMenu;
                    Action();
                    Instantiate(MusicOnClick);
                    break;
                case ActionType.PlayCard:
                    Debug.Log("取消選擇，進行返回作業");
                    ActionChange = ActionType.Waiting;
                    Canel();
                    Action();
                    break;
                case ActionType.InMenu:
                    Debug.Log("關閉選單，回到遊戲");
                    ActionChange = ActionType.Waiting;
                    Action();
                    break;
                case ActionType.DeckMode:
                    Debug.Log("停止查看牌庫內容，回到遊戲");
                    ActionChange = ActionType.Waiting;
                    Action();
                    break;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (ActionChange== ActionType.PlayCard)
            {
                Debug.Log("按下右鍵取消選擇，返回中");
                Canel();
                ActionChange = ActionType.Waiting;
                Action();
            }
        }

    }
    public void Action()
    {
        switch (ActionChange)
        {
            case ActionType.Waiting:
                Debug.Log("等待狀態");
                PickText.SetActive(false);
                CCC.SetActive(false);
                Menu.SetActive(false);
                DeckStuff.SetActive(false);
                break;
            case ActionType.PlayCard:
                Debug.Log("選擇目標中");
                CCC.SetActive(true);
                Menu.SetActive(false);
                break;
            case ActionType.InMenu:
                Debug.Log("在選單中");
                CCC.SetActive(false);
                Menu.SetActive(true);
                break;
            case ActionType.DeckMode:
                Debug.Log("查看牌庫");
                Menu.SetActive(false);
                DeckStuff.SetActive(true);
                break;
        }
    }
    public void collection(int SeedsAmountLevel)
    {
        Score += SeedsAmount[SeedsAmountLevel];
        Yay();
    }
    void Yay()
    {
        Level = LevelData.GetComponent<Saveit>().Levelstatus;
        string UItext = "種子:" + Score + "/" + LevelData.GetComponent<Saveit>().PassAmount;
        ScoreString.text = UItext.ToString();
        if (Deck.ToArray().Length==0)
        {
            DeckIMG.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("empty");
        }
        else
        {
            DeckIMG.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("CARD_BACK");
        }
        if(LevelData.GetComponent<Saveit>().Levelstatus == 1)
        {
            LevelText.text = "教學";
        }else if(LevelData.GetComponent<Saveit>().Levelstatus == 2)
        {
            LevelText.text = "正常";
        }
        else
        {
            LevelText.text = "自訂";
        }
        
        Debug.Log("目前分數:" + Score + "目標:" + LevelData.GetComponent<Saveit>().PassAmount);
        if (Score >= LevelData.GetComponent<Saveit>().PassAmount && Pass==0)
        {
            Pass = 1;
            ActionChange = ActionType.Pass;
            if (PlayerPrefs.GetInt("Level") <= LevelData.GetComponent<Saveit>().Levelstatus)
            {
                PlayerPrefs.SetInt("Level", LevelData.GetComponent<Saveit>().Levelstatus + 1);
                Debug.Log("你可以玩更難關卡");
            }
            Debug.Log("過關");
            Instantiate(MusicPass);  
            NextLevel.SetActive(true);
        }
        DeckAmount.text = "剩餘:" + Deck.ToArray().Length;

        int Sunflower = 0;
        int EmptyZone = 0;
        GameObject[] Graw = GameObject.FindGameObjectsWithTag("Plant");
        for (int i = 0; i < Graw.Length; i++)////
        {
            if (Graw[i].GetComponent<Plants>().status[0] != 0)
            {
                Sunflower++;
            }
            else
            {
                EmptyZone++;
            }
        }
        Debug.Log(Sunflower + "!" + EmptyZone);
        if (EmptyZone > 0)
        {
            DebugText1.text = "空白區1+:1";
        }
        else
        {
            DebugText1.text = "空白區1+:0";
        }
        if (Sunflower > 0)
        {
            DebugText2.text = "向日葵1+:1";
        }
        else
        {
            DebugText2.text = "向日葵1+:0";
        }
        if (Sunflower > 2)
        {
            DebugText3.text = "向日葵3+:1";
        }
        else
        {
            DebugText3.text = "向日葵3+:0";
        }
        if (Sunflower > 0 && EmptyZone > 0)
        {
            DebugText4.text = "向1+&白1+:1";
        }
        else
        {
            DebugText4.text = "向1+&白1+:0";
        }
        Copy = 0;
        GameObject[] CardNumber = GameObject.FindGameObjectsWithTag("Card");
        for (int i = 0; i < CardNumber.Length; i++)////
        {
            switch (CardNumber[i].GetComponent<Cards>().CardUID)
            {
                case "1":
                    if (EmptyZone > 0)
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 1;
                        Copy = 1;
                    }
                    else
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 0;
                    }
                    break;
                case "2":
                    if (Sunflower > 0)
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 1;
                        Copy = 1;
                    }
                    else
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 0;
                    }
                    break;
                case "3":
                    if (Sunflower > 0)
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 1;
                        Copy = 1;
                    }
                    else
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 0;
                    }
                    break;
                case "4":
                    if (Sunflower > 2)
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 1;
                        Copy = 1;
                    }
                    else
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 0;
                    }
                    break;
                case "5":
                    if (Sunflower > 0)
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 1;
                        Copy = 1;
                    }
                    else
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 0;
                    }
                    break;
                case "6":
                    if (Sunflower > 0 && EmptyZone > 0)
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 1;
                        Copy = 1;
                    }
                    else
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 0;
                    }
                    break;
                case "7":
                    if (Hand.ToArray().Length > 1)
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 1;
                        Copy = 1;
                    }
                    else
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 0;
                    }
                    break;
                case "9":
                    if (Sunflower > 0)
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 1;
                        Copy = 1;
                    }
                    else
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 0;
                    }
                    break;
                case "10":
                    if (Sunflower > 0)
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 1;
                        Copy = 1;
                    }
                    else
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 0;
                    }
                    break;
                case "11":
                    if (Sunflower > 0)
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 1;
                        Copy = 1;
                    }
                    else
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 0;
                    }
                    break;
                case "12":
                    if (Sunflower > 2)
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 1;
                        Copy = 1;
                    }
                    else
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 0;
                    }
                    break;
                case "13":
                    if (Sunflower > 2)
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 1;
                        Copy = 1;
                    }
                    else
                    {
                        CardNumber[i].GetComponent<Cards>().Canplay = 0;
                    }
                    break;
            }
            
        }
        if (Copy == 1)
        {
            for (int i = 0; i < CardNumber.Length; i++)////
            {
                if (CardNumber[i].GetComponent<Cards>().CardUID == "8")
                {
                    CardNumber[i].GetComponent<Cards>().Canplay = 1;
                }
            }
        }
        int Stop = 1;
        for (int i = 0; i < CardNumber.Length; i++)////
        {
            if(CardNumber[i].GetComponent<Cards>().Canplay == 1)
            {
                Stop = 0;
            }
        }
        if(Stop==1 && PlaythisRound == 0 && Pass ==0)
        {
            Fail.SetActive(true);
            Instantiate(MusicFail); 
        }
        else
        {
            Fail.SetActive(false);
        }
        if(PlaythisRound == 0)
        {
            NextButton.SetActive(false);
        }
        else
        {
            NextButton.SetActive(true);
        }
    }
    public void RoundStart()
    {
        Instantiate(MusicOnClick);
        Debug.Log("進行回合開始作業" + Hand.ToArray().Length);
        PlaythisRound = 0;
        if (Hand.ToArray().Length == 0 && Deck.ToArray().Length == 0 && Reset < LevelData.GetComponent<Saveit>().ResetAmount)
        {
            DeckReset();
            Draw(); Draw(); Draw(); Draw(); Draw();
        }
        while(Hand.ToArray().Length < 5 && Deck.ToArray().Length > 0)
        {
            Draw();
        }
        GameObject[] Graw = GameObject.FindGameObjectsWithTag("Plant");
        for (int i = 0; i < Graw.Length; i++)////
        {
            Graw[i].GetComponent<Plants>().GrowChange(1);
        }
        if (CardMode == 1)
        {
            
            GameObject[] CardNumber = GameObject.FindGameObjectsWithTag("Card");
            for (int i = 0; i < CardNumber.Length; i++)////
            {
                CardNumber[i].GetComponent<Cards>().CardMode = 1;
            }
        }
        else
        {
            
            GameObject[] CardNumber = GameObject.FindGameObjectsWithTag("Card");
            for (int i = 0; i < CardNumber.Length; i++)////
            {
                CardNumber[i].GetComponent<Cards>().CardMode = 0;
            }
        }

        DeckStuff.SetActive(true);
        GameObject[] DeckArray = GameObject.FindGameObjectsWithTag("Decks");
        for (int i = 0; i < DeckArray.Length; i++)////
        {
            Destroy(DeckArray[i]);
            Debug.Log("我刪了");
        }
        DeckStuff.SetActive(false);
        ArrayList FindDeck = new ArrayList(Deck);
        int Foundnumber = 0;
        for (int i = 1; i < 14; i++)
        {
            Foundnumber = 0;
            while (FindDeck.IndexOf(i) > -1)
            {
                Foundnumber++;
                FindDeck.RemoveAt(FindDeck.IndexOf(i));
            }
            if (Foundnumber > 0)
            {
                DeckCard.GetComponent<DeckData>().CardUID = i.ToString();
                DeckCard.GetComponent<DeckData>().Number = Foundnumber;
                Instantiate(DeckCard, DeckUI.transform);
            }
            Debug.Log(i + "共有" + Foundnumber);
        }
        //GameObject.Find("Zones(Clone)").GetComponent<Plants>().GrowChange(1);
        Yay();
    }
    void DeckReset()
    {
        Debug.Log("進行重製牌庫作業");
        if (Reset < LevelData.GetComponent<Saveit>().ResetAmount)
        {
            Reset++;
            ResetTime.text = "重製牌庫次數:" + Reset + "/" + LevelData.GetComponent<Saveit>().ResetAmount;
            
            DeckBackup2 = LevelData.GetComponent<Saveit>().Deckbaup;

            ArrayList RandonDeck = new ArrayList();

            int pickup = 0;
            /*for (int i = 0; i < DeckBackup.Length; i++)////
            {
                RandonDeck[i] = -1;
            }*/
            for (int i = 0; i < DeckBackup2.ToArray().Length; i++)
            {
                var result = UnityEngine.Random.Range(0, DeckBackup2.ToArray().Length);
                pickup = result;

                String message = "";
                foreach (object S in RandonDeck)
                {
                    message += S.ToString() + ",";
                }

                /*Debug.Log("RandonDeck.contains(pickup)的送出結果是" + RandonDeck.Contains(pickup) + "，pickup:" + pickup+"，RandonDeck:"+ message);
                if (RandonDeck.Contains(pickup) == true) {
                    Debug.Log("發現相同的卡，第" + i + "張效果是" + DeckBackup[pickup] + "喔！");
                }*/

                while (RandonDeck.Contains(pickup) == true)
                {
                    result = UnityEngine.Random.Range(0, DeckBackup2.ToArray().Length);
                    pickup = result;
                }
                RandonDeck.Insert(i, pickup);
                Debug.Log("第" + i + "張將牌放入牌庫，隨機抽到" + pickup + "，將NO" + DeckBackup2[pickup] + "放入牌庫了");
                Deck.Insert(Deck.ToArray().Length, DeckBackup2[pickup]);
            }
            /*foreach (int n in DeckBackup) {
                Debug.Log("第"+(n-1)+"張放入牌庫");
                Deck.Insert(Deck.ToArray().Length, DeckBackup[n-1]);
            }*/

            //Random(0, DeckBackup.Length)
        }
        else
        {
            Debug.Log("重製次數用盡");
            Fail.SetActive(true);
            Instantiate(MusicFail);
        }
        
    }
    void Draw()
    {
        Hand.Insert(Hand.ToArray().Length, Deck[0]);
        CardGO.GetComponent<Cards>().CardUID = Deck[0].ToString();
        Instantiate(CardGO, CardUI.transform);
        Deck.RemoveAt(0);
    }
        public void ResetGame()
    {

        SceneManager.LoadScene(3);
    }
    public void BackToLevel()
    {
        SceneManager.LoadScene(2);
    }
    public void Exit()
    {
        if (LevelData.GetComponent<AudioSource>().clip != Resources.Load<AudioClip>("title"))
        {
            LevelData.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("title");
            LevelData.GetComponent<AudioSource>().Play();
        }
        SceneManager.LoadScene(0);
    }
    public void BacktoGame()
    {
        ActionChange = ActionType.Waiting;
        Instantiate(MusicOnClick);
        Action();
    }
    public void EntetMenu()
    {
        ActionChange = ActionType.InMenu;
        Instantiate(MusicOnClick);
        Action();
    }
    public void LookDeck()
    {
        ActionChange = ActionType.DeckMode;
        Instantiate(MusicOnClick);
        Action();
    }
    public void ShowMode()
    {
        Instantiate(MusicOnClick);
        if (CardMode == 0)
        {
            CardMode = 1;
            GameObject[] CardNumber = GameObject.FindGameObjectsWithTag("Card");
            for (int i = 0; i < CardNumber.Length; i++)////
            {
                CardNumber[i].GetComponent<Cards>().CardMode = 1;
            }
        }
        else
        {
            CardMode = 0;
            GameObject[] CardNumber = GameObject.FindGameObjectsWithTag("Card");
            for (int i = 0; i < CardNumber.Length; i++)////
            {
                CardNumber[i].GetComponent<Cards>().CardMode = 0;
            }
        }
        
    }
    public void RemoveCard(string CardType) {
        Debug.Log("收到移除指令");
        Hand.RemoveRange(0, 1);
        PlaythisRound = 1;
        GameObject[] Graw = GameObject.FindGameObjectsWithTag("Plant");
        switch (CardType)
        {
            case "1":
                Instantiate(MusicBuff);
                for (int i = 0; i < Graw.Length; i++)////
                {
                    if (Graw[i].GetComponent<Plants>().status[0] == 0)
                    {
                        Graw[i].GetComponent<Plants>().status[0] = 1;
                        Graw[i].GetComponent<Plants>().status[1] = 1;
                    }
                }
                break;
            case "5":
                Instantiate(MusicBuff);
                for (int i = 0; i < Graw.Length; i++)////
                {
                    if (Graw[i].GetComponent<Plants>().status[0] != 0)
                    {
                        Graw[i].GetComponent<Plants>().AmountChange(1);
                    }
                }
                break;
            case "11":
                Instantiate(MusicDebuff);
                int highest = 0;

                for (int i = 0; i < Graw.Length; i++)////
                {
                    if(Graw[i].GetComponent<Plants>().status[1] > Graw[highest].GetComponent<Plants>().status[1] || (Graw[i].GetComponent<Plants>().status[1] == Graw[highest].GetComponent<Plants>().status[1] && Graw[i].GetComponent<Plants>().status[0] >= Graw[highest].GetComponent<Plants>().status[0]))
                    {
                        highest = i;
                    }
                }
                Graw[highest].GetComponent<Plants>().AmountChange(-1);
                break;
        }
        Yay();
    }
    public void PickingList(GameObject CardType)
    {
        ActionChange = ActionType.PlayCard;
        //TargetList;

        //public Text PickText;
        PickText.SetActive(true);
        CardType.GetComponent<Cards>().system = "使用中";

        UsingCard = CardType;
        GameObject[] Graw = GameObject.FindGameObjectsWithTag("Plant");
        GameObject[] CardNumber = GameObject.FindGameObjectsWithTag("Card");
        CCC.SetActive(true);
        switch (CardType.GetComponent<Cards>().CardUID)
        {
            case "2":
                Totalnumber = 1;
                PickText.GetComponent<Text>().text = "選擇澆水目標";
                for (int i = 0; i < Graw.Length; i++)////
                {
                    if (Graw[i].GetComponent<Plants>().status[0] != 0)
                    {
                        Graw[i].GetComponent<Plants>().Targetd =1;
                    }
                }
                break;
            case "3":
                Totalnumber = 1;
                PickText.GetComponent<Text>().text = "選擇擴增目標";
                for (int i = 0; i < Graw.Length; i++)////
                {
                    if (Graw[i].GetComponent<Plants>().status[0] != 0)
                    {
                        Graw[i].GetComponent<Plants>().Targetd = 1;
                    }
                }
                break;
            case "4":
                Totalnumber = 3;
                PickText.GetComponent<Text>().text = "將數量-1";
                for (int i = 0; i < Graw.Length; i++)////
                {
                    if (Graw[i].GetComponent<Plants>().status[0] != 0)
                    {
                        Graw[i].GetComponent<Plants>().Targetd = 1;
                    }
                }
                break;
            case "6":
                PickText.GetComponent<Text>().text = "選擇要複製的向日葵區";
                Totalnumber = 2;
                for (int i = 0; i < Graw.Length; i++)////
                {
                    if (Graw[i].GetComponent<Plants>().status[0] != 0)
                    {
                        Graw[i].GetComponent<Plants>().Targetd = 1;
                    }
                }
                break;
            case "7":
                Totalnumber = 1;
                PickText.GetComponent<Text>().text = "選擇要丟掉的手牌";
                for (int i = 0; i < CardNumber.Length; i++)////
                {
                    if (CardNumber[i].GetComponent<Cards>().ststusChange == Cards.Cardststus.Waiting)
                    {
                        CardNumber[i].GetComponent<Cards>().Targetd = 1;
                    }
                }
                break;
            case "8":
                Totalnumber = 2;
                PickText.GetComponent<Text>().text = "選擇要複製的手牌";
                for (int i = 0; i < CardNumber.Length; i++)////
                {
                    if (CardNumber[i].GetComponent<Cards>().ststusChange == Cards.Cardststus.Waiting && CardNumber[i].GetComponent<Cards>().Canplay ==1)
                    {
                        CardNumber[i].GetComponent<Cards>().Targetd = 1;
                    }
                }
                break;
            case "9":
                Totalnumber = 1;
                PickText.GetComponent<Text>().text = "選擇要被清除的向日葵區";
                for (int i = 0; i < Graw.Length; i++)////
                {
                    if (Graw[i].GetComponent<Plants>().status[0] != 0)
                    {
                        Graw[i].GetComponent<Plants>().Targetd = 1;
                    }
                }
                break;
            case "10":
                Totalnumber = 1;
                PickText.GetComponent<Text>().text = "選擇成長-1的向日葵區";
                for (int i = 0; i < Graw.Length; i++)////
                {
                    if (Graw[i].GetComponent<Plants>().status[0] != 0)
                    {
                        Graw[i].GetComponent<Plants>().Targetd = 1;
                    }
                }
                break;
            case "12":
                Totalnumber = 3;
                PickText.GetComponent<Text>().text = "選擇3個數量-1的向日葵區";
                for (int i = 0; i < Graw.Length; i++)////
                {
                    if (Graw[i].GetComponent<Plants>().status[0] != 0)
                    {
                        Graw[i].GetComponent<Plants>().Targetd = 1;
                    }
                }
                break;
            case "13":
                Totalnumber = 3;
                PickText.GetComponent<Text>().text = "選擇3個數量設定1的向日葵區";
                for (int i = 0; i < Graw.Length; i++)////
                {
                    if (Graw[i].GetComponent<Plants>().status[0] != 0)
                    {
                        Graw[i].GetComponent<Plants>().Targetd = 1;
                    }
                }
                break;
        }
    }
    public void Selectit(GameObject Target)
    {
        Debug.Log("收到點選目標");
        TargetList.Insert(TargetList.ToArray().Length, Target);


        PickText.SetActive(true);
        switch (UsingCard.GetComponent<Cards>().CardUID)
        {
            case "4":
                Target.GetComponent<Plants>().system = "數量-1";
                break;
            case "6":
                Target.GetComponent<Plants>().system = "被複製";
                break;
            case "8":
                if(TargetList.ToArray().Length == 1)
                {
                    Target.GetComponent<Cards>().system = "複製它的效果";
                }
                break;
            case "12":
                Target.GetComponent<Plants>().system = "數量-1";
                break;
            case "13":
                Target.GetComponent<Plants>().system = "數量設成1";
                break;
        }
        System(Target);
    }
    public void Useme(GameObject Target)
    {
        ShadowsCard.SetActive(true);
        if (CardMode == 0)
        {
            ShadowsCard.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>(Target.GetComponent<Cards>().CardUID);
        }
        else
        {
            ShadowsCard.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("CARD_0" + Target.GetComponent<Cards>().CardUID + "_info");
        }
        
    }
    public void Canel()
    {
        PickText.SetActive(false);
        TargetList.Clear();
        ActionChange = ActionType.Waiting;
        GameObject[] Graw = GameObject.FindGameObjectsWithTag("Plant");

        for(int i = 0; i < Graw.Length; i++)////
        {
            Graw[i].GetComponent<Plants>().Targetd = 0;
            Graw[i].GetComponent<Plants>().ststusChange = Plants.Plantststus.Waiting;
            Graw[i].GetComponent<Plants>().system = "";
        }
        GameObject[] CardNumber = GameObject.FindGameObjectsWithTag("Card");
        for (int i = 0; i < CardNumber.Length; i++)////
        {
            CardNumber[i].GetComponent<Cards>().Targetd = 0;
            CardNumber[i].GetComponent<Cards>().Canplay = 0;
            CardNumber[i].GetComponent<Cards>().ststusChange = Cards.Cardststus.Waiting;
            CardNumber[i].GetComponent<Cards>().system = "";
        }
        Action();
        Yay();
    }
    public void System(GameObject Target)
    {
        if(TargetList.ToArray().Length == Totalnumber)
        {
            CastCard();
        }
        else
        {
            Instantiate(MusicPicked);
            GameObject[] Graw = GameObject.FindGameObjectsWithTag("Plant");
            GameObject[] CardNumber = GameObject.FindGameObjectsWithTag("Card");
            switch (UsingCard.GetComponent<Cards>().CardUID)
            {
                case "6":
                    if (TargetList.ToArray().Length == 1)
                    {
                        PickText.GetComponent<Text>().text = "選擇要複製的空白區";
                        for (int i = 0; i < Graw.Length; i++)////
                        {
                            if (Graw[i].GetComponent<Plants>().status[0] == 0)
                            {
                                Graw[i].GetComponent<Plants>().Targetd = 1;
                            }
                            else
                            {
                                Graw[i].GetComponent<Plants>().Targetd = 0;
                            }
                        }
                    }
                    break;
                case "8":
                    if (TargetList.ToArray().Length == 1)
                    {
                        switch (Target.GetComponent<Cards>().CardUID)
                        {
                            case "1":
                                for (int i = 0; i < Graw.Length; i++)////
                                {
                                    if (Graw[i].GetComponent<Plants>().status[0] == 0)
                                    {
                                        Graw[i].GetComponent<Plants>().status[0] = 1;
                                        Graw[i].GetComponent<Plants>().status[1] = 1;
                                    }
                                }
                                Destroy(UsingCard);
                                Hand.RemoveRange(0, 1);
                                PlaythisRound = 1;
                                ActionChange = ActionType.Waiting;
                                Canel();
                                Action();
                                Yay();
                                break;
                            case "5":
                                for (int i = 0; i < Graw.Length; i++)////
                                {
                                    if (Graw[i].GetComponent<Plants>().status[0] != 0)
                                    {
                                        Graw[i].GetComponent<Plants>().AmountChange(1);
                                    }
                                }
                                Destroy(UsingCard);
                                Hand.RemoveRange(0, 1);
                                PlaythisRound = 1;
                                ActionChange = ActionType.Waiting;
                                Canel();
                                Action();
                                Yay();
                                break;
                            case "11":
                                int highest = 0;

                                for (int i = 0; i < Graw.Length; i++)////
                                {
                                    if (Graw[i].GetComponent<Plants>().status[1] >= Graw[highest].GetComponent<Plants>().status[1])
                                    {
                                        highest = i;
                                    }
                                }
                                Destroy(UsingCard);
                                Hand.RemoveRange(0, 1);
                                PlaythisRound = 1;
                                Graw[highest].GetComponent<Plants>().AmountChange(-1);
                                ActionChange = ActionType.Waiting;
                                Canel();
                                Action();
                                Yay();
                                break;
                            case "2":
                                PickText.GetComponent<Text>().text = "選擇澆水目標";
                                for (int i = 0; i < CardNumber.Length; i++)////
                                {
                                    CardNumber[i].GetComponent<Cards>().Targetd = 0;
                                }
                                for (int i = 0; i < Graw.Length; i++)////
                                {
                                    if (Graw[i].GetComponent<Plants>().status[0] != 0)
                                    {
                                        Graw[i].GetComponent<Plants>().Targetd = 1;
                                    }
                                }
                                break;
                            case "3":
                                PickText.GetComponent<Text>().text = "選擇擴增目標";
                                for (int i = 0; i < CardNumber.Length; i++)////
                                {
                                    CardNumber[i].GetComponent<Cards>().Targetd = 0;
                                }
                                for (int i = 0; i < Graw.Length; i++)////
                                {
                                    if (Graw[i].GetComponent<Plants>().status[0] != 0)
                                    {
                                        Graw[i].GetComponent<Plants>().Targetd = 1;
                                    }
                                }
                                break;
                            case "4":
                                PickText.GetComponent<Text>().text = "選擇數量-1的向日葵區";
                                Totalnumber += 2;
                                for (int i = 0; i < CardNumber.Length; i++)////
                                {
                                    CardNumber[i].GetComponent<Cards>().Targetd = 0;
                                }
                                for (int i = 0; i < Graw.Length; i++)////
                                {
                                    if (Graw[i].GetComponent<Plants>().status[0] != 0)
                                    {
                                        Graw[i].GetComponent<Plants>().Targetd = 1;
                                    }
                                }
                                break;
                            case "6":
                                PickText.GetComponent<Text>().text = "選擇要複製的向日葵區";
                                Totalnumber += 1;
                                for (int i = 0; i < CardNumber.Length; i++)////
                                {
                                    CardNumber[i].GetComponent<Cards>().Targetd = 0;
                                }
                                for (int i = 0; i < Graw.Length; i++)////
                                {
                                    if (Graw[i].GetComponent<Plants>().status[0] != 0)
                                    {
                                        Graw[i].GetComponent<Plants>().Targetd = 1;
                                    }
                                }
                                break;
                            case "7":
                                PickText.GetComponent<Text>().text = "選擇移除的手牌";
                                break;
                            case "9":
                                PickText.GetComponent<Text>().text = "選擇要清除的向日葵區";
                                for (int i = 0; i < CardNumber.Length; i++)////
                                {
                                    CardNumber[i].GetComponent<Cards>().Targetd = 0;
                                }
                                for (int i = 0; i < Graw.Length; i++)////
                                {
                                    if (Graw[i].GetComponent<Plants>().status[0] != 0)
                                    {
                                        Graw[i].GetComponent<Plants>().Targetd = 1;
                                    }
                                }
                                break;
                            case "10":
                                PickText.GetComponent<Text>().text = "選擇要成長-1的向日葵區";
                                for (int i = 0; i < CardNumber.Length; i++)////
                                {
                                    CardNumber[i].GetComponent<Cards>().Targetd = 0;
                                }
                                for (int i = 0; i < Graw.Length; i++)////
                                {
                                    if (Graw[i].GetComponent<Plants>().status[0] != 0)
                                    {
                                        Graw[i].GetComponent<Plants>().Targetd = 1;
                                    }
                                }
                                break;
                            case "12":
                                PickText.GetComponent<Text>().text = "選擇3個數量-1";
                                Totalnumber += 2;
                                for (int i = 0; i < CardNumber.Length; i++)////
                                {
                                    CardNumber[i].GetComponent<Cards>().Targetd = 0;
                                }
                                for (int i = 0; i < Graw.Length; i++)////
                                {
                                    if (Graw[i].GetComponent<Plants>().status[0] != 0)
                                    {
                                        Graw[i].GetComponent<Plants>().Targetd = 1;
                                    }
                                }
                                break;
                            case "13":
                                PickText.GetComponent<Text>().text = "選擇3個數量設定1區";
                                Totalnumber += 2;
                                for (int i = 0; i < CardNumber.Length; i++)////
                                {
                                    CardNumber[i].GetComponent<Cards>().Targetd = 0;
                                }
                                for (int i = 0; i < Graw.Length; i++)////
                                {
                                    if (Graw[i].GetComponent<Plants>().status[0] != 0)
                                    {
                                        Graw[i].GetComponent<Plants>().Targetd = 1;
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (TargetList[0].GetComponent<Cards>().CardUID)
                        {
                            case "6":
                                if (TargetList.ToArray().Length == 2) 
                                {
                                    PickText.GetComponent<Text>().text = "選擇要複製的空白區";
                                    for (int i = 0; i < Graw.Length; i++)////
                                    {
                                        if (Graw[i].GetComponent<Plants>().status[0] == 0)
                                        {
                                            Graw[i].GetComponent<Plants>().Targetd = 1;
                                        }
                                        else
                                        {
                                            Graw[i].GetComponent<Plants>().Targetd = 0;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    break;
            }
        }
    }
    public void CastCard()
    {
        Debug.Log("進行使用卡片");
        string WorkNO ="";
        GameObject[] CardNumber = GameObject.FindGameObjectsWithTag("Card");
        for (int i = 0; i < CardNumber.Length; i++)////
        {
            if (CardNumber[i].GetComponent<Cards>().ststusChange == Cards.Cardststus.Using)
            {
                WorkNO= CardNumber[i].GetComponent<Cards>().CardUID;

            }
        }
        switch (WorkNO)
        {
            case "2":
                Debug.Log("2號卡片出動");
                Instantiate(MusicBuff);
                TargetList[0].GetComponent<Plants>().GrowChange(2);
                Destroy(UsingCard);
                Hand.RemoveRange(0, 1);
                Canel();
                break;
            case "3":
                Debug.Log("3號卡片出動");
                Instantiate(MusicBuff);
                TargetList[0].GetComponent<Plants>().AmountChange(1);
                Destroy(UsingCard);
                Hand.RemoveRange(0, 1);
                Canel();
                break;
            case "4":
                Debug.Log("4號卡片出動");
                Instantiate(MusicBuff);
                if (TargetList[0].GetComponent<Plants>().status[0] == TargetList[1].GetComponent<Plants>().status[0] && TargetList[1].GetComponent<Plants>().status[0] == TargetList[2].GetComponent<Plants>().status[0])
                {
                    TargetList[2].GetComponent<Plants>().AmountChange(2);
                }
                else
                {
                    TargetList[2].GetComponent<Plants>().AmountChange(1);
                }
                TargetList[0].GetComponent<Plants>().AmountChange(-1);
                TargetList[1].GetComponent<Plants>().AmountChange(-1);
                Destroy(UsingCard);
                Hand.RemoveRange(0, 1);
                Canel();
                break;
            case "6":
                Debug.Log("6號卡片出動");
                Instantiate(MusicBuff);
                TargetList[1].GetComponent<Plants>().status[0] = TargetList[0].GetComponent<Plants>().status[0];
                TargetList[1].GetComponent<Plants>().status[1] = TargetList[0].GetComponent<Plants>().status[1];
                Destroy(UsingCard);
                Hand.RemoveRange(0, 1);
                Canel();
                break;
            case "7":
                Debug.Log("7號卡片出動");
                Instantiate(MusicBuff);
                Destroy(UsingCard);
                Destroy(TargetList[0]);
                Hand.RemoveRange(0, 2);
                Canel();
                break;
            case "8":
                Debug.Log("8號卡片出動");
                Instantiate(MusicBuff);
                switch (TargetList[0].GetComponent<Cards>().CardUID)
                {
                    case "2":
                        Debug.Log("複製了2號卡片，進行中");
                        TargetList[1].GetComponent<Plants>().GrowChange(2);
                        break;
                    case "3":
                        Debug.Log("複製了3號卡片，進行中");
                        TargetList[1].GetComponent<Plants>().AmountChange(1);
                        break;
                    case "4":
                        Debug.Log("複製了4號卡片，進行中");
                        if (TargetList[1].GetComponent<Plants>().status[0] == TargetList[2].GetComponent<Plants>().status[0] && TargetList[2].GetComponent<Plants>().status[0] == TargetList[3].GetComponent<Plants>().status[0])
                        {
                            TargetList[3].GetComponent<Plants>().AmountChange(2);
                        }
                        else
                        {
                            TargetList[3].GetComponent<Plants>().AmountChange(1);
                        }
                        TargetList[1].GetComponent<Plants>().AmountChange(-1);
                        TargetList[2].GetComponent<Plants>().AmountChange(-1);
                        break;
                    case "6":
                        Debug.Log("複製了6號卡片，進行中");
                        TargetList[2].GetComponent<Plants>().status[0] = TargetList[1].GetComponent<Plants>().status[0];
                        TargetList[2].GetComponent<Plants>().status[1] = TargetList[1].GetComponent<Plants>().status[1];
                        break;
                    case "7":
                        Debug.Log("複製了7號卡片，進行中");
                        Destroy(TargetList[1]);
                        Hand.RemoveRange(0, 1);
                        break;
                    case "9":
                        Debug.Log("複製了9號卡片，進行中");
                        TargetList[1].GetComponent<Plants>().status[0] = 0;
                        TargetList[1].GetComponent<Plants>().status[1] = 0;
                        break;
                    case "10":
                        Debug.Log("複製了10號卡片，進行中");
                        TargetList[1].GetComponent<Plants>().GrowChange(-1);
                        break;
                    case "12":
                        Debug.Log("複製了2號卡片，進行中");
                        TargetList[1].GetComponent<Plants>().AmountChange(-1);
                        TargetList[1].GetComponent<Plants>().AmountChange(-1);
                        TargetList[3].GetComponent<Plants>().AmountChange(-1);
                        break;
                }
                Destroy(UsingCard);
                Hand.RemoveRange(0, 1);
                Canel();
                break;
            case "9":
                Debug.Log("9號卡片出動");
                Instantiate(MusicDebuff);
                TargetList[0].GetComponent<Plants>().status[0] = 0;
                TargetList[0].GetComponent<Plants>().status[1] = 0;
                Destroy(UsingCard);
                Hand.RemoveRange(0, 1);
                Canel();
                break;
            case "10":
                Debug.Log("10號卡片出動");
                Instantiate(MusicDebuff);
                TargetList[0].GetComponent<Plants>().GrowChange(-1);
                Destroy(UsingCard);
                Hand.RemoveRange(0, 1);
                Canel();
                break;
            case "12":
                Debug.Log("12號卡片出動");
                Instantiate(MusicDebuff);
                TargetList[0].GetComponent<Plants>().AmountChange(-1);
                TargetList[1].GetComponent<Plants>().AmountChange(-1);
                TargetList[2].GetComponent<Plants>().AmountChange(-1);
                Destroy(UsingCard);
                Hand.RemoveRange(0, 1);
                Canel();
                break;
            case "13":
                Debug.Log("13號卡片出動");
                Instantiate(MusicDebuff);
                TargetList[0].GetComponent<Plants>().status[1]=1;
                TargetList[1].GetComponent<Plants>().status[1]=1;
                TargetList[2].GetComponent<Plants>().status[1]=1;
                Destroy(UsingCard);
                Hand.RemoveRange(0, 1);
                Canel();
                break;
        }
        PlaythisRound = 1;
        Yay();
    }
 }