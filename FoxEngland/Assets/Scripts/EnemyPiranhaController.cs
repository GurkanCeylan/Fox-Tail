using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPiranhaController : MonoBehaviour
{
    private Animator anim;
    public bool attacking;
    public SpriteRenderer theSR;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            anim.SetBool("canAttack", true);
        }else if (!attacking)
        {
            anim.SetBool("canAttack", false);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            attacking = true;

            if (transform.position.x < PlayerController.instance.transform.position.x)
            {
                theSR.flipX = true;
            }
            else if (transform.position.x > PlayerController.instance.transform.position.x)
            {
                theSR.flipX = false;
            }  
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        attacking = false;
    }

}
