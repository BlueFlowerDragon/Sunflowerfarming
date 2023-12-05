using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Text;

public class CardLevel : MonoBehaviour
{
    public Text NumberCard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NumberUp()
    {
        int i = int.Parse(NumberCard.text) + 1;
        NumberCard.text = i.ToString();
    }
    public void NumberDown()
    {
        int i = int.Parse(NumberCard.text) - 1;
        if(i < 0)
        {
            i = 0;
        }
        NumberCard.text = i.ToString();
    }
}
