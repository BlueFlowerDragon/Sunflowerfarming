using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Text;

public class DeckData : MonoBehaviour
{
    public int Number = 0;
    public string CardUID;
    public Text CardText;
    public GameObject IMG;
    // Start is called before the first frame update
    void Start()
    {
        CardText.text = Number.ToString();
        IMG.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>(CardUID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
