using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisiual : MonoBehaviour
{
    private const string CUT = "Cut";
    [SerializeField] CuttingCounter cuttingCounter;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, EventArgs e)
    {
        anim.SetTrigger(CUT);
    }
}
