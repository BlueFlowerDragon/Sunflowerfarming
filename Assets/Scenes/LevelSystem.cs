using System.Collections;
using System.Collections.Generic;
//using System.Media;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSystem : MonoBehaviour
{
    public GameObject LevelData;
    public GameObject Customized;
    public GameObject LockStuff1;
    public GameObject LockStuff2;
    ArrayList Deck = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("LevelSystem專案啟動了");
        
        LevelData = GameObject.Find("LevelSave");
        if (PlayerPrefs.GetInt("Level") > 1)
        {
            LockStuff1.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Level") > 2)
        {
            LockStuff2.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Level (int choose)
    {
        if(choose ==1)
        {
            Debug.Log("你正在進入教學的關卡");
            LevelData.GetComponent<Saveit>().Levelstatus = 1;
            LevelData.GetComponent<Saveit>().PassAmount = 500;
            LevelData.GetComponent<Saveit>().PlantAmount = 7;
            LevelData.GetComponent<Saveit>().ResetAmount = 2;
            LevelData.GetComponent<Saveit>().DeckClean();
            Deck.Clear();
            Deck.Add(1);Deck.Add(1);Deck.Add(1);
            Deck.Add(2);
            Deck.Add(3);Deck.Add(3);Deck.Add(3);
            Deck.Add(4);
            Deck.Add(5);
            Deck.Add(6);
            Deck.Add(7);
            Deck.Add(8);
            Deck.Add(9);Deck.Add(9);
            Deck.Add(10);
            Deck.Add(11);
            Deck.Add(12);
            Deck.Add(13);
            LevelData.GetComponent<Saveit>().Deckpush(Deck);
            SceneManager.LoadScene(3);
        }
        else if(choose == 2)
        {
            Debug.Log("你正在進入正常的關卡");
            LevelData.GetComponent<Saveit>().Levelstatus = 2;
            LevelData.GetComponent<Saveit>().PassAmount = 4000;
            LevelData.GetComponent<Saveit>().PlantAmount = 7;
            LevelData.GetComponent<Saveit>().ResetAmount = 3;
            LevelData.GetComponent<Saveit>().DeckClean();
            Deck.Clear();
            Deck.Add(1); Deck.Add(1); Deck.Add(1);
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
            Deck.Add(13);
            LevelData.GetComponent<Saveit>().Deckpush(Deck);
            SceneManager.LoadScene(3);
        }
        
        
    }
    public void Open()
    {
        Customized.SetActive(true);
    }
    public void Back()
    {
        if (LevelData.GetComponent<AudioSource>().clip != Resources.Load<AudioClip>("title"))
        {
            LevelData.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("title");
            LevelData.GetComponent<AudioSource>().Play();
        }
        SceneManager.LoadScene(0);
    }
}
