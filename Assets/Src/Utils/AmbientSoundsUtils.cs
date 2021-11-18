using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(AudioUtil))]
public class AmbientSoundsUtils : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    private AudioUtil audioUtil;
    // Start is called before the first frame update
    void Start()
    {
        audioUtil = GetComponent<AudioUtil>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioUtil.isPlaying)
            PlayAmbientSounds();
    }


    private AudioClip PickRandomClip()
    {
        int size = clips.Length;
        int random_index = UnityEngine.Random.Range(0,size);
        AudioClip clip = clips[random_index];
        return clip;
    }


    private void PlayAmbientSounds()
    {
        AudioClip clip = PickRandomClip();
        audioUtil.Play(clip);
    }
}
