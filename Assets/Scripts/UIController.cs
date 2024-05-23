 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public static UIController Instance { get; private set; }

    [SerializeField] TextMeshProUGUI coinText;
    int coinIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(this.gameObject);

        SetCoin(0);
    }

    public void SetCoin(int coin)
    {
        coinIndex += coin;
        coinText.text = coin.ToString();
    }

}

