using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPointController : MonoBehaviour
{
    public int hp = 10;
    public Slider hpSlider;

    private void Start()
    {
        hpSlider.maxValue = hp;
        hpSlider.value = hp;
    }

    public void ChangeHp()
    {
        hpSlider.value = --hp;
        if (hp <= 0)
        {
            GameObject.Find("GameManager").SendMessage("GameFailed");
        }
    }
}