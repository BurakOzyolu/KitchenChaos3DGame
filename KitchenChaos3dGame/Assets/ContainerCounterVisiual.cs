using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisiual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    [SerializeField] ContainerCounter ContainerCounter;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        ContainerCounter.OnPlayerGrabbedObject += ContainerCounter_OplayerGrabbedObject;
    }

    private void ContainerCounter_OplayerGrabbedObject(object sender, EventArgs e)
    {
        anim.SetTrigger(OPEN_CLOSE);
    }

    void Update()
    {
        
    }
}
