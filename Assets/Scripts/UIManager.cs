using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image healthBar;
    public Text bulletCountText;
    public static UIManager instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public void UpdateHealthBar(int curAmount,int maxAmount)
    {
        healthBar.fillAmount = (float)curAmount / maxAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBulletCount(int curAmount,int maxAmount)
    {
        bulletCountText.text = curAmount.ToString() + "/" + maxAmount;
    }
}
