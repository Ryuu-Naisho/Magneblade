using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(AudioUtil))]
public class FootController : MonoBehaviour
{


    [SerializeField] private AudioClip[] clips;
    private AudioUtil audioUtil;
    private wTags tags;
    private float distance = 0.001f;
    private float yOffset = 0.0005f;
    private float footRadius = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        audioUtil = GetComponent<AudioUtil>();
        tags = new wTags();
    }


    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        int size = clips.Length;
        int random_index = UnityEngine.Random.Range(0,size);
        AudioClip clip = clips[random_index];
        return clip;
    }

    // Update is called once per frame
void Update()
{
    string hitTag = DetectGround(Vector3.zero);
    if (hitTag != null)
    {
        OnFound(hitTag);
        return;
    }

    const int rays = 10;
    for (int i = 0; i < rays; ++i)
    {
        float angle = (360.0f / rays) * i;
        Vector3 posOffset = Quaternion.AngleAxis(angle, Vector3.up) * (Vector3.forward * footRadius);

        hitTag = DetectGround(posOffset);
        if (hitTag != null)
        {
            OnFound(hitTag);
            return;
        }
    }
}

void OnFound(string tag)
{
    if(tag == tags.Floor)
    {
        audioUtil.PlayOneShot(clips[0]);
    }
}

string DetectGround(Vector3 posOffset)
{
    RaycastHit hit;
    Ray footstepRay = new Ray(transform.position + posOffset + (Vector3.up * yOffset), Vector3.down);

    if(Physics.Raycast(footstepRay, out hit, distance + yOffset, LayerMask.GetMask("Ground", "Platform")))
    {
        return hit.collider.tag;
    }
    return null;
}
}
