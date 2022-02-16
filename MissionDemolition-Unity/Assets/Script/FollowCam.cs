/**
 * 
 * 
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    

    /****VARIABLE****/
    static public GameObject POI; //The static point of interest
    
    [Header("Set Inespector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ; //desired Z pos of the camera

    private void Awake()
    {
        camZ = this.transform.position.z; //set z 
    } // end awake

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if (POI == null) return; //do nothing foe POI

        // Vector3 destination = POI.transform.position;


        Vector3 destination;
        if (POI == null)
        {
            destination = Vector3.zero;
        } else
        {
            destination = POI.transform.position;
            if(POI.tag == "Projectile")
            {
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;
                    return; //in the next 
                } //end if POI.GetComponent<Rigidbody>().IsSleeping()
            } // end if POI.tag == "Projectile"
        } // end if else


        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);


        //interpolate from current position to destination

        destination = Vector3.Lerp(transform.position, destination, easing);

        destination.z = camZ;

        transform.position = destination;



        Camera.main.orthographicSize = destination.y + 10;

    } //end FixedUpdate
}
