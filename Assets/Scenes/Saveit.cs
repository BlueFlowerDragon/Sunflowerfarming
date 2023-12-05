using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saveit : MonoBehaviour
{
    public GameObject DontDestroy_Obj;
    public int Levelstatus = 1;
    public int PlantAmount = 7;
    public int ResetAmount = 2;
    public int PassAmount = 1000;
    public ArrayList Deckbaup = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(DontDestroy_Obj);
        GameObject[] CardNumber = GameObject.FindGameObjectsWithTag("AllData");
        if(CardNumber.Length > 1)
        {
            Destroy(DontDestroy_Obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DeckClean()
    {
        Deckbaup.Clear();
    }
    public void Deckpush(ArrayList Deck)
    {
        Deckbaup = Deck;
    }
}
