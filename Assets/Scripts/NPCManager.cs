using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{

    public GameObject dialog;
    public GameObject tipImage;
    public float showTime=4;
    private float showTimer;
    // Start is called before the first frame update
    void Start()
    {
        dialog.SetActive(false);
        tipImage.SetActive(true);
        showTimer = -1;
    }

    // Update is called once per frame
    void Update()
    {
        showTimer -= Time.deltaTime;
        if(showTimer<0)
        {
            dialog.SetActive(false);
            tipImage.SetActive(true);
        }
    }

    public void ShowDialog()
    {
        showTimer = showTime;
        dialog.SetActive(true);
        tipImage.SetActive(false);

    }
}
