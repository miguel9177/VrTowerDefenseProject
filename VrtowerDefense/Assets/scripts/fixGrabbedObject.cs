using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixGrabbedObject : MonoBehaviour
{

    public GameObject VrPlayer;
    //this will save the position that the object grabbed needs to be
    public GameObject HandGrabbedPos;
    Vector3 oldpos;
    bool iscolliding = false;
    //this will control if the object is grabbed or not
    public bool is_grabbed = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    //this is changed by the object that is being grabbed probably the object with this script
    public void changeBool()
    {
        //if this object is not grabbed make him grabbed
        if (is_grabbed == false)
        {
            is_grabbed = true;
            oldpos = this.transform.position;
        }
        //if this object is grabbed make him not grabbed
        else
        {
            is_grabbed = false;
            // i do this for the tower to be on the ground all the time
            this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
            if (iscolliding == true)
            {
                this.transform.position = oldpos;
            }
        }
    }



    void OnCollisionEnter(Collision collision)
    {
        iscolliding = true;
        Debug.Log(collision);
    }

    void OnCollisionExit(Collision collision)
    {
        iscolliding = false;
    }

    // Update is called once per frame
    void Update()
    {

        //if this object is grabbed
        if (is_grabbed == true)
        {

            //make this object go to the handgrabbedposition
            this.transform.position = HandGrabbedPos.transform.position;
        }
    }
}