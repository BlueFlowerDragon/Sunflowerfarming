using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Text;



public class LevelUI : MonoBehaviour
{
    ArrayList Deck = new ArrayList();
    public GameObject Self;
    public GameObject LevelOutput;
    public Text Seeds;
    public Text Plants;
    public Text Decks;
    public Text Card1;
    public Text Card2;
    public Text Card3;
    public Text Card4;
    public Text Card5;
    public Text Card6;
    public Text Card7;
    public Text Card8;
    public Text Card9;
    public Text Card10;
    public Text Card11;
    public Text Card12;
    public Text Card13;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("LevelUI專案啟動了");
        LevelOutput = GameObject.Find("LevelSave");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DeckUp()
    {
        int i = int.Parse(Decks.text) + 1;
        Decks.text = i.ToString();
    }
    public void DeckDown()
    {
        int i = int.Parse(Decks.text) - 1;
        if(i < 1)
        {
            i = 1;
        }
        Decks.text = i.ToString();
    }
    public void PlantUp()
    {
        int i = int.Parse(Plants.text) + 1;
        if(i > 8)
        {
            i = 8;
        }   
        Plants.text = i.ToString();
    }
    public void PlantDown()
    {
        int i = int.Parse(Plants.text) - 1;
        if (i < 6)
        {
            i = 6;
        }
        Plants.text = i.ToString();
    }
    public void Enter()
    {
        Debug.Log("你正在進入自訂的關卡");
        if (Seeds.text == "")
        {
            LevelOutput.GetComponent<Saveit>().PassAmount = 500;
        }
        else
        {
            LevelOutput.GetComponent<Saveit>().PassAmount = int.Parse(Seeds.text);
        }
        
        LevelOutput.GetComponent<Saveit>().PlantAmount = int.Parse(Plants.text);
        LevelOutput.GetComponent<Saveit>().ResetAmount = int.Parse(Decks.text);
        LevelOutput.GetComponent<Saveit>().DeckClean();
        Deck.Clear();
        for (int i = 0; i < int.Parse(Card1.text); i++) { Deck.Add(1); }
        for (int i = 0; i < int.Parse(Card2.text); i++) { Deck.Add(2); }
        for (int i = 0; i < int.Parse(Card3.text); i++) { Deck.Add(3); }
        for (int i = 0; i < int.Parse(Card4.text); i++) { Deck.Add(4); }
        for (int i = 0; i < int.Parse(Card5.text); i++) { Deck.Add(5); }
        for (int i = 0; i < int.Parse(Card6.text); i++) { Deck.Add(6); }
        for (int i = 0; i < int.Parse(Card7.text); i++) { Deck.Add(7); }
        for (int i = 0; i < int.Parse(Card8.text); i++) { Deck.Add(8); }
        for (int i = 0; i < int.Parse(Card9.text); i++) { Deck.Add(9); }
        for (int i = 0; i < int.Parse(Card10.text); i++) { Deck.Add(10); }
        for (int i = 0; i < int.Parse(Card11.text); i++) { Deck.Add(11); }
        for (int i = 0; i < int.Parse(Card12.text); i++) { Deck.Add(12); }
        for (int i = 0; i < int.Parse(Card13.text); i++) { Deck.Add(13); }

        /*Deck.Add(1); Deck.Add(1); Deck.Add(1);
        Deck.Add(2);
        Deck.Add(3); Deck.Add(3); Deck.Add(3);
        Deck.Add(4);
        Deck.Add(5);
        Deck.Add(6);
        Deck.Add(7);
        Deck.Add(8);
        Deck.Add(9); Deck.Add(9);
        Deck.Add(10);
        Deck.Add(11);
        Deck.Add(12);
        Deck.Add(13);*/
        LevelOutput.GetComponent<Saveit>().Deckpush(Deck);
        SceneManager.LoadScene(3);
    }
    public void Exit()
    {
        Self.SetActive(false);
    }
}
