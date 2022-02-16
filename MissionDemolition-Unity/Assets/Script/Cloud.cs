/***
 * Create randomly cloud
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [Header("Set in inespector")]
    
    public GameObject CloudSphere;
    public int numberSpheresMin = 6;
    public int numberSpheresMax = 10;
    public Vector2 sphereScaleRangeX = new Vector2(4, 8);
    public Vector2 sphereScaleRangeY = new Vector2(3, 4);
    public Vector2 sphereScaleRangeZ = new Vector2(2, 4);
    public Vector3 sphereOffsetScale = new Vector3(5, 2, 1);
    public float scaleYMin = 2f;

    private List<GameObject> spheres;

    // Start is called before the first frame update
    void Start()
    {
        spheres = new List<GameObject>();

        int num = Random.Range(numberSpheresMin, numberSpheresMax);

        for (int i = 0; i < num; i++)
        {
            GameObject sp = Instantiate<GameObject>(CloudSphere);
            spheres.Add(sp);

            Transform spTrans = sp.transform;
            spTrans.SetParent(this.transform);

            //Randomly assign a position
            Vector3 offset = Random.insideUnitSphere;

            offset.x *= sphereOffsetScale.x;
            offset.y *= sphereOffsetScale.y;
            offset.z *= sphereOffsetScale.z;

        } //end for
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
