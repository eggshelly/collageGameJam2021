using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEndLocation : MonoBehaviour
{
    [SerializeField] List<DelayAndDuration> locations;
    [SerializeField] bool ShouldStopAnimation = false;

    Animator anim;

    bool overlapped = false;
    int currentIndex = 0;

    private void Awake()
    {
        this.gameObject.layer = LayerMask.NameToLayer("EndLocation");
        anim = this.GetComponent<Animator>();
        foreach(DelayAndDuration d in locations)
        {
            d.GetCollider().enabled = false;
        }

    }

    private void Start()
    {
        StartCoroutine(StartLocations());
    }

    IEnumerator StartLocations()
    {
        foreach(DelayAndDuration d in locations)
        {
            yield return new WaitForSeconds(d.GetDelay());
            d.GetCollider().enabled = true;
            yield return new WaitForSeconds(d.GetDuration());
            d.GetCollider().enabled = false;
            if(!overlapped && ShouldStopAnimation && anim != null)
            {
                anim.SetBool("Stop", true);
            }
            overlapped = false;
            currentIndex += 1;
        }
    }

    public void Overlapped()
    {
        overlapped = true;
        locations[currentIndex].GetCollider().enabled = false;
    }
}
