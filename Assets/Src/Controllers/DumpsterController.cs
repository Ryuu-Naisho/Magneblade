using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpsterController : MonoBehaviour
{
    [SerializeField] private Transform shitantPrefab;
    [SerializeField] private float transitionTime;
    private wTags tags;
    private Animator animator;
    private bool animationExecuted = false;
    private bool swapPrefabs = true;
    private wNames names;
    // Start is called before the first frame update
    void Start()
    {
        tags = new wTags();
        names = new wNames();
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (animationExecuted && !AnimatorIsPlaying() && swapPrefabs)
        {
            swapPrefabs = false;
            Action doSwap = ()=> DoSwapPrefabs();
            StartCoroutine(Wait(transitionTime, doSwap));
        }
    }


    private void DoSwapPrefabs()
    {
        GameObject currentPrefab = transform.Find(names.ShitAntMain).gameObject;
        Vector3 current_position = currentPrefab.transform.position;
        var current_rotation = currentPrefab.transform.rotation;
        Destroy(currentPrefab);
        Instantiate(shitantPrefab, current_position, current_rotation);
        
    }



    private   bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
        animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

        private void OnTriggerEnter(Collider other)
    {
        string wtag = other.tag;
        if (wtag == tags.Player && !animationExecuted)
        {
            animator.enabled = true;
            animationExecuted = true;
        }
    }


    private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
}
