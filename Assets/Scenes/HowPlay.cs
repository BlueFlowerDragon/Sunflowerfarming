using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowPlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("HowtoPlay專案啟動了");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("抓到右鍵");
            
        }
    }
    public void Back()
    {
        Debug.Log("我要跑去主畫面");
        SceneManager.LoadScene(0);
    }

    public void LeftSSS()
    {
        Debug.Log("左鍵點及");
    }
}
