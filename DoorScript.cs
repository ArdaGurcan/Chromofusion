using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Color selfColor;
    Animator animator;
    bool trigger = false;
    public ButtonScript[] buttons;
    static List<Vector3> pickupPositions;
    static List<Quaternion> pickupRotations;
    static List<Vector3> pickupScales;
    static List<Color> pickupColors;
    bool activated = false;
    GameObject player;
    static GameObject lastActivated;
    public GameObject prefab;

    void Start()
    {
        selfColor = GetComponent<Renderer>().material.color;
        animator = GetComponent<Animator>();
        trigger = GetComponent<BoxCollider>().isTrigger;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!trigger)
        {
            bool open = true;
            for (int i = 0; i < buttons.Length; i++)
            {
                //Debug.Log(i + ", "+buttons[i].on);
                    if (!buttons[i].on)
                {

                    open = false;
                }
            }
            animator.SetBool("open", open);
        }
        else if((Input.GetKeyDown(KeyCode.R)) && lastActivated == gameObject)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Pickupable");
            for (int i = 0; i < objects.Length; i++)
            {
                //if(objects[i].GetComponent<Renderer>())
                {
                    Destroy(objects[i]);
                }

                //if(i < pickupPositions.Count)
                //{
                //    objects[i].transform.position = pickupPositions[i];
                //    objects[i].transform.rotation = pickupRotations[i];
                //    objects[i].transform.localScale = pickupScales[i];
                //    if(objects[i].GetComponent<Renderer>())
                //        objects[i].GetComponent<Renderer>().material.color = pickupColors[i];
                //}
                //else
                //{
                //    Destroy(objects[i]);
                //}
            }
            for (int i = 0; i < pickupPositions.Count; i++)
            {
                GameObject newObject = Instantiate(prefab, pickupPositions[i], pickupRotations[i]);
                newObject.transform.localScale = pickupScales[i];
                newObject.GetComponent<Renderer>().material.color = pickupColors[i];
                newObject.GetComponent<Pickup>().Start();
            }
            //Destroy(objects[0]);
            //GameObject originalObject = Instantiate(objects[7], pickupPositions[0], pickupRotations[0]);
            //originalObject.transform.localScale = pickupScales[0];
            //originalObject.GetComponent<Renderer>().material.color = pickupColors[0];
            //originalObject.GetComponent<Pickup>().Start();

            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = transform.position;
            player.GetComponent<CharacterController>().enabled = true;

        }
        if (player.transform.position.y < -40f)
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = transform.position;
            player.GetComponent<CharacterController>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && trigger && !activated)
        {
            activated = true;
            lastActivated = gameObject;
            pickupPositions = new List<Vector3>();
            pickupRotations = new List<Quaternion>();
            pickupScales = new List<Vector3>();
            pickupColors = new List<Color>();
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Pickupable");
            
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].GetComponent<Renderer>())
                {
                    pickupPositions.Add(objects[i].transform.position);
                    pickupRotations.Add(objects[i].transform.rotation);
                    pickupScales.Add(objects[i].transform.localScale);
                    pickupColors.Add(objects[i].GetComponent<Renderer>().material.color);
                }
                
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if((collision.collider.tag == "Pickupable" || collision.collider.tag == "Player"))

       
    //        collision.collider.transform.parent = transform;
            
        
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if ((collision.collider.tag == "Pickupable" || collision.collider.tag == "Player"))
    //    {
    //        collision.collider.transform.parent = null;
            
    //    }
    //}
}
