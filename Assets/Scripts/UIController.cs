 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public static UIController Instance => instance;

    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI bulletText;

    private int coinIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(this.gameObject);

        SetCoin(0);
    }

    public void InitTextBullet(int bullet)
    {
        bulletText.text = bullet.ToString();
    }

    public void SetCoin(int coin)
    {
        coinIndex += coin;
        coinText.text = coin.ToString();
    }

}

