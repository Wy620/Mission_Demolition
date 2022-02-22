using System.Collections;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int numClouds = 40; // the number of clouds to make
    public GameObject cloudPrefab; // The prefab for the clouds
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1; // Min scale of each cloud
    public float cloudScaleMax = 3; // Max scale of each cloud
    public float cloudSpeedMult = 0.5f; // Adjust speed of clouds

    private GameObject[] cloudInstances;

    void Awake()
    {
        cloudInstances = new GameObject[numClouds]; // Make an array large enough to
                                                    // hold all the Cloud_instances
        GameObject anchor = GameObject.Find("CloudAnchor"); // Find the CloudAnchor parent GameObject
        GameObject cloud; // Iterate through and make Cloud_s
        for (int i = 0; i < numClouds; i++)
        {
            cloud = Instantiate<GameObject>(cloudPrefab); // Make an instance of cloudPrefab
            Vector3 cPos = Vector3.zero; // Position cloud
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);

            float scaleU = Random.value; // scale cloud
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU); // smaller cloud
            cPos.z = 100 - 90 * scaleU; // smaller clouds should be further away
            cloud.transform.position = cPos; // apply these transforms to the cloud
            cloud.transform.localScale = Vector3.one * scaleVal;
            cloud.transform.SetParent(anchor.transform); // make cloud a child of the anchor
            cloudInstances[i] = cloud; // Add the cloud to cloudInstances

        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject cloud in cloudInstances)
        {
            float scaleVal = cloud.transform.localScale.x; // get the cloud scale and position
            Vector3 cPos = cloud.transform.position;
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult; // move larger clouds faster
            if (cPos.x <= cloudPosMin.x)
            {
                cPos.x = cloudPosMax.x; // move it to the far right
            } // end if
            cloud.transform.position = cPos; // Apply the new position to cloud

        } // end foreach
    }
}