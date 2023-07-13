using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] BaseCounter baseCounter;
    [SerializeField] GameObject[] visualGameObjectArray;
    void Start()
    {
        Player.Instance.OnSelectedCounterChangeEvent += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangeEventArg e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        foreach (GameObject virtualGameObject in visualGameObjectArray)
        {
            virtualGameObject.SetActive(false);
        }
        
    }

    private void Show()
    {
        foreach (GameObject virtualGameObject in visualGameObjectArray)
        {
            virtualGameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
