 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI bulletText;

    private int coinIndex;

    private void Awake()
    {
        coinIndex = 0;
        SetCoin(0);
    }

    public void InitTextBullet(int bullet)
    {
        bulletText.text = bullet.ToString();
    }

    public void SetCoin(int coin)
    {
        coinIndex += coin;
        coinText.text = coinIndex.ToString();
    }

}

