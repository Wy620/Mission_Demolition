using System.Collections;
using UnityEngine;

public class Goal : MonoBehaviour
{
    static public bool goalMet = false; // A static field accessible by code anywhere

    void OnTriggerEnter(Collider other)
    {
        // when the trigger is hit by something
        // Check to see if it's a projectile
        if (other.gameObject.tag == "Projectile")
        {
            Goal.goalMet = true; // If so, set goalMet to true
            Material mat = GetComponent<Renderer>().material; // also set the alpha of the color
                                                              // to higher opacity
            Color c = mat.color;
            c.a = 1;
            mat.color = c;
        } // end if
    } // end OnTriggerEnter

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}