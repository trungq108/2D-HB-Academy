 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIController : Singleton<UIController>
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI bulletText;
    [SerializeField] Button nextStageButton;

    private int coinIndex;

    private void Awake()
    {
        coinIndex = 0;
        SetCoin(0);
        nextStageButton.onClick.AddListener(NextLevel);
    }

    public void EndLevel()
    {
        nextStageButton.gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level 2", LoadSceneMode.Additive);
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

