using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] CuttingCounter cuttingCounter;
    [SerializeField] Image bar;
    private void Start()
    {
        cuttingCounter.OnProgressBarChange += CuttingCounter_OnProgressBarChange;
        bar.fillAmount = 0;
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void CuttingCounter_OnProgressBarChange(object sender, CuttingCounter.OnProgressBarChangeEventArgs e)
    {
        bar.fillAmount = e.progressNormalize;
        if (e.progressNormalize == 0f || e.progressNormalize ==1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
}
