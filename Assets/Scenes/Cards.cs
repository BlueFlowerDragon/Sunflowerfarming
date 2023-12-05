using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Globalization;

using System.Text;

public class Cards : MonoBehaviour
{
    public enum Cardststus { Waiting, Using, Picked};
    public Cardststus ststusChange;
    public string CardUID;
    public string CardNO;
    public int Canplay = 0;
    public int Targetd = 0;
    public int CardMode = 0;
    public string system;
    public Text UIText;
    public GameObject Title;
    public GameObject Content;
    public Text TitleText;
    public Text ContentText;
    public GameObject IMG;
    public GameObject Light;
    public GameObject self;
    public GameObject ShadowsCard;
    float Y;
    float WA;

    public GameObject MusicCantPicked;
    //1撥種完成 2澆水 3擴增 4集中 5夏季隻峰完成 6移植 7稻草人 8夥伴 9竊賊 10乾旱 11歉收 12暴風 13疾病
    // Start is called before the first frame update
    void Start()
    {
        ststusChange = Cardststus.Waiting;
        Debug.Log("顯示卡片");
        switch (CardUID)
        {
            case "1":
                TitleText.text = "撥種";
                ContentText.text = "賦予全數空白區域種子狀態";
                break;
            case "2":
                TitleText.text = "澆水";
                ContentText.text = "賦予一區域成長階段+2";
                break;
            case "3":
                TitleText.text = "擴增";
                ContentText.text = "賦予一區域數量階段+1";
                break;
            case "4":
                TitleText.text = "集中";
                ContentText.text = "指定兩區域數量階段-1。賦予一區域數量階段+1，三區域相同，則將+1變為+2";
                break;
            case "5":
                TitleText.text = "夏季之風";
                ContentText.text = "所有區域數量階段+1";
                break;
            case "6":
                TitleText.text = "移植";
                ContentText.text = "指定一區域複製至空白區域";
                break;
            case "7":
                TitleText.text = "稻草人";
                ContentText.text = "消除一張手牌";
                break;
            case "8":
                TitleText.text = "夥伴";
                ContentText.text = "複製並且使用一張手牌";
                break;
            case "9":
                TitleText.text = "竊賊";
                ContentText.text = "指定空白區域以外一區域清空";
                break;
            case "10":
                TitleText.text = "乾旱";
                ContentText.text = "所有區域成長階段-1";
                break;
            case "11":
                TitleText.text = "歉收";
                ContentText.text = "數量階段最高區域數量階段-1";
                break;
            case "12":
                TitleText.text = "暴風";
                ContentText.text = "指定除空白區域以外三區域，數量階段-1";
                break;
            case "13":
                TitleText.text = "疾病";
                ContentText.text = "指定除空白區域以外三區域，數量階段降為1";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        WA = Screen.width;
        UIText.text = system;
        //GameObject.Find("GameRule").GetComponent<GameRules>().ActionType == PlayCard
        /*enum ActionType { Waiting, PlayCard, InMenu, Pass };
        ActionType ActionChange;*/
        if ((Targetd == 1 && GameObject.Find("GameRule").GetComponent<GameRules>().ActionChange == GameRules.ActionType.PlayCard) || (Canplay == 1 && GameObject.Find("GameRule").GetComponent<GameRules>().ActionChange == GameRules.ActionType.Waiting))
        {
            Light.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("777");

        }
        else
        {
            Light.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("empty");
        }
        if (CardMode == 0)
        {
            IMG.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>(CardUID);
            //Title.SetActive(false);
            Content.SetActive(false);
        }
        else
        {
            IMG.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("CARD_0"+ CardUID + "_info");
            //Title.SetActive(true);
            //Content.SetActive(true);
        }
        

    }
    public void OnMouseDown()
    {
        Debug.Log("卡片被點了");
        if (Targetd == 1)
        {
            ststusChange = Cardststus.Picked;
            Targetd = 0;
            GameObject.Find("GameRule").GetComponent<GameRules>().Selectit(self);
        }
        else
        {
            Instantiate(MusicCantPicked);
        }
    }
    public void Hold()
    {
        Y = Input.mousePosition.y;
        Debug.Log("我被Hold了，他在那邊" + Input.mousePosition.y + "抓我");
        if (Canplay == 1)
        {
            Image hid = IMG.GetComponent<Image>();
            Color Rgb = hid.color;
            Rgb.a = 0.6f;
            hid.color = Rgb;
            GameObject.Find("GameRule").GetComponent<GameRules>().Useme(self);
        }
        else
        {
            Instantiate(MusicCantPicked);
        }
    }
    public void HoldDown()
    {
        Debug.Log("我被丟在" + Input.mousePosition.y + "了" + WA);
        Image hid = IMG.GetComponent<Image>();
        ShadowsCard = GameObject.Find("ShadowsCard");
        ShadowsCard.SetActive(false);
        Color Rgb = hid.color;
        Rgb.a = 1;
        hid.color = Rgb;
        if ((Y- Input.mousePosition.y)*1920 / WA < -250)
        {
            Debug.Log("我這張卡被使用了");
            PlayCard();
        }
    }
    void PlayCard()
    {
        if((CardUID == "1" || CardUID == "5" || CardUID == "11") && Canplay == 1)
        {
            GameObject.Find("GameRule").GetComponent<GameRules>().RemoveCard(CardUID);
            Destroy(this.gameObject);
        }else if(Canplay == 1)
        {
            ststusChange = Cardststus.Using;
            GameObject.Find("GameRule").GetComponent<GameRules>().PickingList(self); 
        }
    }
    public void OnMouseOver()
    {
        Debug.Log("卡片有東西在上面");

    }
}
