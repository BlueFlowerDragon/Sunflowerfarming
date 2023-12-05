using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Textpick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Textpick專案啟動");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseDown()
    {
        Debug.Log("被點了");

    }
    public void OnMouseOver()
    {
        Debug.Log("有東西在上面");

    }
}
