using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    Color selfColor;
    Animator animator;
    Collider[] colliders;
    public static List<Color> activeColors = new List<Color>();
    public bool on = false;
    float threshold = 0.4f;

    private void Start()
    {
        selfColor = GetComponent<Renderer>().material.color;
        animator = GetComponent<Animator>();
    }



    //private void OnTriggerStay(Collider other)
    //{
    //    Debug.Log(selfColor + ", " + Color.black + ", " + other.tag);
    //    if (selfColor == Color.black && other.tag == "Player")
    //    {
    //        animator.SetBool("pressed", true);
    //        if (!activeColors.Contains(selfColor))
    //        {
    //            activeColors.Add(selfColor);
    //        }
    //        on = true;
    //    }
    //    else if(other.tag == "Pickupable")
    //    {


    //        Color c1 = other.GetComponent<Renderer>().material.color;
    //        Vector3 color1 = new Vector3(c1.r, c1.g, c1.b);
    //        Vector3 color2 = new Vector3(selfColor.r, selfColor.g, selfColor.b);
    //        Debug.Log(Vector3.Distance(color1, color2));
    //        if (Vector3.Distance(color1, color2) < threshold)
    //        {
    //            animator.SetBool("pressed", true);
    //            on = true;
    //            if(!activeColors.Contains(selfColor))
    //            {
    //                activeColors.Add(selfColor);
    //            }

    //            if (!colliders.Contains(other.gameObject))
    //            {
    //                colliders.Add(other.gameObject);
    //            }

    //            //colors.Add(c1);
    //        }
    //        else
    //        {
    //            colliders.Remove(other.gameObject);
    //            if (colliders.Count == 0)
    //            {
    //                animator.SetBool("pressed", false);
    //                activeColors.Remove(selfColor);
    //                on = false;
    //            }
    //        }
    //    }

    //}
    void OnMouseDown()
    {
        Debug.Log(Application.persistentDataPath);
        ScreenCapture.CaptureScreenshot("SomeLevel");
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(Application.persistentDataPath);
            ScreenCapture.CaptureScreenshot("screenshut");
        }
        //Debug.Log(LayerMask.GetMask("Player") + ", " + LayerMask.GetMask("Pickupable"));
        colliders = Physics.OverlapBox(transform.position + Vector3.up * 0.4415991f * transform.parent.parent.localScale.x, new Vector3(1.537476f * transform.parent.parent.localScale.x, 0.265764f, 1.537476f * transform.parent.parent.localScale.x) /2, Quaternion.identity, 1024 + 512);
        on = false;
        for (int i = 0; i < colliders.Length; i++)
        {
            Transform other = colliders[i].transform;
            if (selfColor == Color.black && other.tag == "Player")
            {

                on = true;
            }
            else if (other.tag == "Pickupable")
            {

                Color c1 = other.GetComponent<Renderer>().material.color;
                Vector3 color1 = new Vector3(c1.r, c1.g, c1.b);
                Vector3 color2 = new Vector3(selfColor.r, selfColor.g, selfColor.b);
                Debug.Log(other.localScale.x + ", " + transform.parent.parent.localScale.x/2);
                //Debug.Log(Vector3.Distance(color1, color2));
                if (Vector3.Distance(color1, color2) < threshold && transform.parent.parent.localScale.x/2 - other.localScale.x < 0.01f)
                {
                    on = true;
                }
            }
        }
        if(on)
        {
            animator.SetBool("pressed", true);
            if (!activeColors.Contains(selfColor))
            {
                activeColors.Add(selfColor);
            }
        }
        else
        {
            animator.SetBool("pressed", false);
            activeColors.Remove(selfColor);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawCube
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (selfColor == Color.black && other.tag == "Player")
    //    {
    //        animator.SetBool("pressed", false);
    //        activeColors.Remove(selfColor);
    //        on = false;
    //    }
    //    else if (other.tag == "Pickupable")
    //    {

    //        Color c1 = other.GetComponent<Renderer>().material.color;
    //        Vector3 color1 = new Vector3(c1.r, c1.g, c1.b);
    //        Vector3 color2 = new Vector3(selfColor.r, selfColor.g, selfColor.b);
    //        Debug.Log(Vector3.Distance(color1, color2));
    //        if (Vector3.Distance(color1, color2) < threshold)
    //        {
    //            colliders.Remove(other.gameObject);
    //            if(colliders.Count == 0)
    //            {
    //                animator.SetBool("pressed", false);
    //                on = false;
    //                activeColors.Remove(selfColor);
    //            }
                
    //        }
    //    }
    //}
}
