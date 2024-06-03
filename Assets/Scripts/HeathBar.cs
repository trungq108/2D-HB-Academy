using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    [SerializeField] Image imageFill;
    [SerializeField] Vector3 offset;

    private float hp;
    private float maxHp;
    private Transform target;

    private void Update()
    {
        transform.position = target.position + offset;
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp / maxHp, Time.deltaTime * 5);
    }

    public void OnInit(float maxHp, Transform target)
    {
        this.target = target;
        this.maxHp = maxHp;
        hp = maxHp;
        imageFill.fillAmount = hp/maxHp;
    }

    public void SetHp(float hp)
    {
        this.hp = hp;
    }


}
