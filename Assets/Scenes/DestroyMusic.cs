using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMusic : MonoBehaviour
{
    public GameObject self;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(self, 10f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
