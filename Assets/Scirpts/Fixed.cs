using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetRe();
    }
    public void SetRe()
    {
        int setWidth = 640;
        int setHeigth = 1100;
        Screen.SetResolution(setWidth, setHeigth, false);
    }
}
