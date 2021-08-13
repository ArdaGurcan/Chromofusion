using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    GameObject player;
    Transform camera;
    Rigidbody rb;
    Renderer rend;
    BoxCollider collider;
    MeshCollider meshCollider;
    public bool floating = false;
    Vector3 lastValidPosition = Vector3.zero;
    float initialDistance = 0f;
    float tempDistance;
    

    // Start is called before the first frame update
    public void Start()
    {
        //gameObject.name = transform.position.x + " " + transform.position.y + " " + transform.position.z;
        rb = GetComponent<Rigidbody>();
        Transform parent = transform.parent;
        transform.parent = null;
        transform.localScale *= Random.Range(1f, 1.01f);
        transform.parent = parent;
        rend = GetComponent<Renderer>();
        if(GetComponent<BoxCollider>())
        {
            collider = GetComponent<BoxCollider>();
        }
        if(GetComponent<MeshCollider>())
        {
            meshCollider = GetComponent<MeshCollider>();

        }
        //lastValidPosition = transform.position + Vector3.up * 0.1f;
        rb.mass = transform.localScale.x;
        player = GameObject.FindGameObjectWithTag("Player");
        camera = player.transform.GetChild(0);
    }

    private void Update()
    {
        if(transform.position.y < -20)
        {
            Destroy(gameObject);
        }
        //Debug.Log(player.GetComponent<CharacterController>().velocity.magnitude);
    }

    void LateUpdate()
    {
        
        float k = 0.1f;
        if (floating)
        {
            transform.parent = null;
            //Debug.Log(Physics.OverlapBox(transform.position + transform.localScale.x * transform.up, transform.localScale, transform.rotation, 1).Length);
            //Debug.Log(Physics.Raycast(camera.position, camera.position + camera.forward * Mathf.Min(Mathf.Min(initialDistance, tempDistance), transform.localScale.x * 8 + 1.5f), Vector3.Distance(camera.position, camera.position + camera.forward * Mathf.Min(Mathf.Min(initialDistance, tempDistance), transform.localScale.x * 8 + 1.5f)), 1));
            //Vector3 cameraRotation = camera.rotation.eulerAngles;
            transform.LookAt(camera);
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
            //transform.RotateAround(transform.position + Vector3.Normalize(camera.position - transform.position) * transform.localScale.x / 2, transform.right, (-transform.rotation.eulerAngles.x + cameraRotation.x)/2);
            //(-transform.rotation.eulerAngles + new Vector3(0, cameraRotation.y, cameraRotation.z));
            for (int i = 0; i < 20; i++)
            {
                if (transform.position == lastValidPosition || Physics.OverlapBox(transform.position + transform.localScale.x * transform.up, transform.localScale, transform.rotation, 1).Length == 0)
                {
                    lastValidPosition = transform.position;
                    Vector3 target = camera.position + camera.forward * Mathf.Min(Mathf.Min(initialDistance, tempDistance), transform.localScale.x * 8 + 1.5f);
                    if (Physics.OverlapBox(target, transform.localScale, transform.rotation, 1).Length == 0 && !Physics.Raycast(camera.position, camera.forward, Mathf.Min(Mathf.Min(initialDistance, tempDistance), transform.localScale.x * 8 + 1.5f), 1))
                    {
                        transform.position = target - transform.localScale.x * transform.up;
                        //tempDistance = Vector3.Distance(transform.position, camera.position);
                        if (Physics.OverlapBox(camera.position + camera.forward * Mathf.Min(tempDistance + k, transform.localScale.x * 8 + 1.5f), transform.localScale, transform.rotation, 1).Length == 0 && tempDistance < initialDistance)
                        {
                            tempDistance += k;
                        }
                        Debug.Log(1);
                    }
                    else
                    {
                        Debug.Log(2);
                        tempDistance -= k;
                    }


                }
                else
                {
                    Debug.Log(3);

                    transform.position = lastValidPosition;
                    //tempDistance -= k;
                }
                if(tempDistance < 1.5f)
                {
                    tempDistance = 1.5f;
                }
                
            }
            if(Physics.Raycast(camera.position, camera.forward, Mathf.Min(Mathf.Min(initialDistance, tempDistance), transform.localScale.x * 8 + 1.5f), 1))
            {
                OnMouseUp();
            }

        }

    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawCube(camera.position + camera.forward * Mathf.Min(Mathf.Min(initialDistance, tempDistance), transform.localScale.x * 8 + 1.5f), transform.localScale*2);
        //Gizmos.DrawRay(camera.position, camera.forward * Mathf.Min(initialDistance, transform.localScale.x * 8 + 1.5f));
    }

    private void OnMouseDown()
    {
        lastValidPosition = transform.position + Vector3.up * 0.1f;
        //rb = GetComponent<Rigidbody>();
        //player = GameObject.FindGameObjectWithTag("Player");
        //camera = player.transform.GetChild(0);
        //Debug.Log(Vector3.Distance(transform.position, player.transform.position));
        Debug.Log(gameObject);
        if (Vector3.Distance(transform.position, player.transform.position) < 15f)
        {
            if(gameObject.tag != "BigChungus")
            {
                if (collider)
                    collider.enabled = false;
                if (meshCollider)
                    meshCollider.enabled = false;

            }

            floating = true;
            //rb.useGravity = false;
            rb.isKinematic = true;
            
            //transform.SetParent(player.transform.GetChild(0));
            initialDistance = Vector3.Distance(transform.position, camera.position);
            tempDistance = initialDistance;
            //transform.position = camera.position + camera.forward * Mathf.Min(Vector3.Distance(transform.position, camera.position), transform.localScale.x * 8 + 1.5f);// - camera.up * transform.localScale.x;

            //camera.GetChild(0).GetComponent<FixedJoint>().connectedBody = rb;

        }
        //Vector3 cameraRotation = camera.rotation.eulerAngles;
        //transform.Rotate(-cameraRotation);

    }


    private void OnMouseUp()
    {
        //rb = GetComponent<Rigidbody>();
        //player = GameObject.FindGameObjectWithTag("Player");
        //camera = player.transform.GetChild(0);
        floating = false;
        if (gameObject.tag == "BigChungus")
        {
            player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().chungus = false;
        }
        //camera.GetChild(0).GetComponent<FixedJoint>().connectedBody = null;
        if (collider)
            collider.enabled = true;
        if (meshCollider)
            meshCollider.enabled = true;
        //transform.SetParent(null);
        if(Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = player.GetComponent<CharacterController>().velocity;

        }

        rb.isKinematic = false;
    }

    private void OnMouseOver()
    {
        Debug.Log(gameObject);
        if (!floating && Input.GetKeyDown(KeyCode.Mouse1))
        {
            gameObject.transform.localScale *= Mathf.Pow(0.5f, 1f / 3f);
            GameObject other = Instantiate(gameObject, transform.position - Vector3.right * gameObject.transform.localScale.x * 2f, transform.rotation);
            transform.position += Vector3.right * gameObject.transform.localScale.x * 2f;
            other.GetComponent<Rigidbody>().mass /= 2;
            other.GetComponent<Pickup>().Start();
            rb.mass /= 2;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if ((collision.collider.tag == "Pickupable" || collision.collider.tag == "BigChungus") && !collision.collider.GetComponent<Pickup>().floating && !GetComponent<Pickup>().floating)
        {
            Transform other = collision.collider.transform;
            if (other.localScale.x < transform.localScale.x || (other.localScale.x == transform.localScale.x && other.GetComponent<Rigidbody>().velocity.magnitude > rb.velocity.magnitude))
            {
                float m1 = Mathf.Pow(other.localScale.x,3f);
                float m2 = Mathf.Pow(transform.localScale.x,3f);
                float mf = m1 + m2;

                rb.mass += other.GetComponent<Rigidbody>().mass;

                if(other.tag != "BigChungus")
                {
                    Color c1 = other.GetComponent<Renderer>().material.color;
                    Color c2 = rend.material.color;
                    Color cf = (c1 + c2) / 2;
                    float h, s, v;
                    Color.RGBToHSV(cf, out h, out s, out v);

                    //Color.RGBToHSV(cf)

                    rend.material.color = Color.HSVToRGB(h, s, 0.9f);
                }
                
                //float sum = m1 + m2;
                //Color cf = c2 * m2 / sum / (c2.r + c2.g + c2.b) + c1 * m1 / sum / (c1.r + c1.g + c1.b);
                
                //Debug.Log(m1 + ", " + m2 + ", " + mf);
                Destroy(other.gameObject);
                float x = Mathf.Pow(mf,1f/3f);
                transform.localScale = new Vector3(x, x, x);
                rb.velocity = Vector3.zero;
                
                
            }

        }
    }
}
