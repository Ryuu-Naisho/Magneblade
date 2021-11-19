using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private float transitionTime;
    private wScenes scenes;
    // Start is called before the first frame update
    void Start()
    {
        scenes = new wScenes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MainScene()
    {
        Action loadScene = ()=> SceneManager.LoadScene(scenes.MainScene);
        StartCoroutine(Wait(transitionTime, loadScene));
    }

    private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }

}
