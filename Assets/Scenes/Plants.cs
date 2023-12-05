using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Text;

public class Plants : MonoBehaviour
{
    public int[] SeedsAmount = new int[] { 0, 1, 5, 10, 50, 100, 500, 1000 };
    public int[] status = { 0, 0 };
    public enum Plantststus { Waiting, Picked };
    public Plantststus ststusChange;
    public int GrowingLevel;
    public int AmountLevel;
    public int Targetd = 0;
    public string system;
    public Text UIText;
    public Text Picked;
    public GameObject self;
    public GameObject IMG;
    public GameObject Light;
    public GameObject LevelData;

    public GameObject MusicCantPicked;
    // Start is called before the first frame update
    /*void Awake()
    {
        GrowingLevel = 1;
        AmountLevel = 1;
        status[0] = 1;
        status[1] = 1;
        Debug.Log("以新增種植點");
       
    }*/
    void Start()
    {

        GrowingLevel = 1;
        AmountLevel = 1;
        status[0] = 1;
        status[1] = 1;
        Debug.Log("以新增種植點");
        LevelData = GameObject.Find("LevelSave");
        if (LevelData.GetComponent<Saveit>().PlantAmount == 6)
        {
            self.GetComponent<RectTransform>().sizeDelta = new Vector2(252, 300);
            IMG.GetComponent<RectTransform>().sizeDelta = new Vector2(252, 300);
        }
        else if (LevelData.GetComponent<Saveit>().PlantAmount == 7)
        {
            self.GetComponent<RectTransform>().sizeDelta = new Vector2(216, 300);
            IMG.GetComponent<RectTransform>().sizeDelta = new Vector2(216, 300);
        }
        else if (LevelData.GetComponent<Saveit>().PlantAmount == 8)
        {
            self.GetComponent<RectTransform>().sizeDelta = new Vector2(189, 300);
            IMG.GetComponent<RectTransform>().sizeDelta = new Vector2(189, 300);
        }

    }
    // Update is called once per frame
    void Update()
    {
        Picked.text = system;
        if (status[1] > 0)
        {
            UIText.text =SeedsAmount[status[1]].ToString();
        }
        else
        {
            UIText.text = "";
        }
        
        if (Targetd==1)
        {
            Light.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("777");
        }
        else
        {
            Light.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("empty");
        }
        switch (status[0])
        {
            case 1:
                IMG.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("field_02");
                break;
            case 2:
                IMG.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("field_03");
                break;
            case 3:
                IMG.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("field_04");
                break;
            case 4:
                IMG.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("field_05");
                break;
            case 0:
                IMG.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("field_01");
                break;
        }
    }
    public void OnMouseDown()
    {
        Debug.Log("盆栽被點了");
        if (Targetd == 1)
        {
            ststusChange = Plantststus.Picked;
            Targetd = 0;
            GameObject.Find("GameRule").GetComponent<GameRules>().Selectit(self);
        }
        else
        {
            Instantiate(MusicCantPicked);
        }
    }
    public void AmountChange(int Level)
    {
        status[1] += Level;
        if (status[1] > 8)
        {
            status[1] = 7;
        }
        StatusCheck();
    }
    public void GrowChange(int Level)
    {
        status[0] += Level;
        
        StatusCheck();
    }
    void StatusCheck()
    {
        if (status[0] > 4)
        {
            GameObject.Find("GameRule").GetComponent<GameRules>().collection(status[1]);
            status[0] = 0;
            status[1] = 0;
        }
        if (status[0] < 1 || status[1] < 1)
        {
            status[0] = 0;
            status[1] = 0;
        }
    }
        public void OnMouseOver()
    {
        Debug.Log("盆栽有東西在上面");

    }
}