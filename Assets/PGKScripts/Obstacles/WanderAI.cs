using UnityEngine;
using System.Collections;

public class WanderAI : MonoBehaviour
{
    // The target marker.
    public Transform[] target;
    private Animator anim; 
    public float speed;

    private int current;

    void Start()
    {
       
        anim = GetComponent<Animator>();
      anim.SetBool("isWalking", true);
    }


    void Update()
    {
       

        if (transform.position != target[current].position)
        {
            Vector3 direction = target[current].position - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            //GetComponent<Rigidbody>().MovePosition(pos);
            transform.position = pos;
            
        }
        else
        {
            current = (current + 1) % target.Length;
        }
        
    }

 
}