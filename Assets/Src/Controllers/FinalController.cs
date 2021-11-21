using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalController : MonoBehaviour
{
    [SerializeField] private GameObject wGUI;
    private Animator animator;
    private bool animationStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Action promptMessage = ()=>
        {
            wGUI.SetActive(true);
        };
        StartCoroutine(Wait(8f, promptMessage));
    }

    // Update is called once per frame
    void Update()
    {
    }


    private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
}
