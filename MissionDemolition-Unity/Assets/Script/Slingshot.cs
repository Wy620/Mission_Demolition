using System.Collections;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    static private Slingshot s;

    [Header("Set in Inspector")] // fields set in the Unity Inspector pane
    public GameObject prefabProjectile;
    public float velocityMult = 8f;

    [Header("Set Dynamically")] // fields set dynamically
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;
    private Rigidbody projectileRigidbody;

    static public Vector3 LAUNCH_POS
    {
        get
        {
            if (s == null) return Vector3.zero;
            return s.launchPos;
        }
    }

    void Awake()
    {
        s = this;
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }
    void OnMouseEnter()
    {
        //print("Slingshot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        //print("Slingshot:OnMouseExit()");
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        aimingMode = true; // The player has pressed the mouse button while over Slingshot
        projectile = Instantiate(prefabProjectile) as GameObject; // Instantiate a Projectile
        projectile.transform.position = launchPos; // Start it at the launchPoint
        projectile.GetComponent<Rigidbody>().isKinematic = true; // set it to isKinematic for now
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!aimingMode) return; // If slingshot is not in aimingMode, don't run this code

        Vector3 mousePos2D = Input.mousePosition; // Get the current mouse
                                                  // position in 2D screen coordinates
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPos; // Find the delta from the
                                                     // launchPos to the mousePos3D
        float maxMagnitude = this.GetComponent<SphereCollider>().radius; // Limit mouseDelta to the radius
                                                                         // of the Slingshot SphereCollider
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        Vector3 projPos = launchPos + mouseDelta; // Move the projectile to this new position
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0)) // The mouse has been released
        {
            aimingMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;
            MissionDemolition.ShotFired();
            ProjectileLine.S.poi = projectile;
        }



    }
}

