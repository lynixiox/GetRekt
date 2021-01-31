using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemsOnMesh : MonoBehaviour
{

    public int NumberOfObjects;
    public int currentObjects;

    public GameObject Egg1;
    public GameObject Egg2;
    public GameObject Egg3;
    public GameObject GoldenEgg; 

    private float randomX;
    private float randomZ;
    public Renderer r;

    // Use this for initialization
    void Awake()
    {
      //  r = GetComponent<Renderer>();
        randomX = Random.Range(r.bounds.min.x, r.bounds.max.x);
        randomZ = Random.Range(r.bounds.min.z, r.bounds.max.z);
    }

    // Update is called once per frame
    void Update()
    {
        randomX = Random.Range(r.bounds.min.x, r.bounds.max.x);
        randomZ = Random.Range(r.bounds.min.z, r.bounds.max.z);
        RaycastHit hit;
        if (currentObjects <= NumberOfObjects)
        {
            Debug.Log("1");
            if (Physics.Raycast(new Vector3(randomX, r.bounds.max.y + 5f, randomZ), -Vector3.up, out hit) && hit.collider.tag == "Ground") 
            {
                GameObject u;
                Debug.Log("3");
                int i = Random.Range(0, 101);
                if(i > 14)
                {
                    u =Instantiate(GoldenEgg, hit.point + new Vector3(0, 0.4f, 0), transform.rotation);

                }else if((i > 8) && (i < 15))
                {
                    u=Instantiate(Egg3, hit.point + new Vector3(0, 0.4f, 0), transform.rotation);

                }
                else if((i > 1) && (i < 9))
                {
                    u=Instantiate(Egg2, hit.point + new Vector3(0, 0.4f, 0), transform.rotation);

                }
                else if(i < 2)
                {
                    u=Instantiate(Egg1, hit.point + new Vector3(0,0.4f,0), transform.rotation);

                }
              //  u.transform.rotation = new Quaternion(Mathf.Deg2Rad*-90,0,0,0);
           
                currentObjects += 1;

            }
        }
    }
}
