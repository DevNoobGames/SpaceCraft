using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Markus : MonoBehaviour
{
    private World world;
    public bool isGrounded;

    public Texture2D cubeSprite;
    public GameObject inHandObject;
    public bool isFollowing;
    public bool isAttacking;

    public float walkSpeed = 3f;
    public float jumpForce = 5f;
    public float gravity = -9.8f;

    public float playerWidth = 0.3f;
    public float boundsTolerance = 0.1f;
    private Vector3 velocity;
    private float verticalMomentum = 0;
    //private bool jumpRequest;

    public Vector3 spawnPos;
    public GameObject player;

    public Sprite blockImage;
    public Animation anim;

    public float health = 100;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim.Stop();
        world = GameObject.Find("World").GetComponent<World>();
        spawnPos = transform.position;
        isFollowing = false;
        isAttacking = false;
        inHandObject = GameObject.FindGameObjectWithTag("InHandObjects").transform.GetChild(2).gameObject;
    }

    void Update()
    {
        if (!world.inUI)
        {
            CalculateVelocity();
            transform.Translate(velocity, Space.World);
        }

        if (health <= 0)
        {
            string name = "Markus";
            int ID = 21; //Get ID
            Sprite image = blockImage;
            bool isweapon = false;
            bool isblock = true;
            int Amount = 1;
            Texture2D CubeSprite = cubeSprite;
            GameObject iinHandObject = inHandObject;
            GameObject.FindGameObjectWithTag("Toolbar").GetComponent<Toolbar>().AddItemToToolbar(name, ID, image, isweapon, isblock, Amount, CubeSprite, iinHandObject);

            Destroy(gameObject);
        }

        Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        if (Vector3.Distance(transform.position, targetPos) < 20 && !isFollowing && !isAttacking)
        {
            isFollowing = true;
            anim.Play("Scene");
        }
        if (Vector3.Distance(transform.position, targetPos) < 1)
        {
            StartCoroutine(attackCheck());
            /*anim.Play("Hit");
            if (player.GetComponent<Player>().canBeAttacked)
            {
                player.GetComponent<Player>().canBeAttacked = false;
                player.GetComponent<Player>().GotHurt();
            }
            isFollowing = false;
            isAttacking = true;*/
        }
        if (Vector3.Distance(transform.position, targetPos) > 1.5f && isAttacking)
        {
            isAttacking = false;
            anim.Play("Scene");
            isFollowing = true;
        }

        if (isFollowing)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, walkSpeed * Time.deltaTime);
            transform.LookAt(targetPos);
        }
        if (front || back || left || right)
        {
            if (isGrounded)
            {
                Jump();
            }
        }

        //when markus falls through ground for whatevs reason, respawn the dude
        if (transform.position.y <= -100)
        {
            transform.position = spawnPos;
        }
    }

    IEnumerator attackCheck()
    {
        yield return new WaitForSeconds(1);
        Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        if (Vector3.Distance(transform.position, targetPos) < 1)
        {
            anim.Play("Hit");
            if (player.GetComponent<Player>().canBeAttacked)
            {
                player.GetComponent<Player>().canBeAttacked = false;
                player.GetComponent<Player>().GotHurt();
            }
            isFollowing = false;
            isAttacking = true;
        }
    }

    void Jump()
    {
        verticalMomentum = jumpForce;
        isGrounded = false;
        //jumpRequest = false;
    }

    private void CalculateVelocity()
    {
        // Affect vertical momentum with gravity.
        if (verticalMomentum > gravity)
            verticalMomentum += Time.fixedDeltaTime * gravity;

        // if we're sprinting, use the sprint multiplier.
        //velocity = ((transform.forward) + (transform.right)) * Time.fixedDeltaTime * walkSpeed;

        // Apply vertical momentum (falling/jumping).
        velocity += Vector3.up * verticalMomentum * Time.fixedDeltaTime;

        if ((velocity.z > 0 && front) || (velocity.z < 0 && back))
            velocity.z = 0;
        if ((velocity.x > 0 && right) || (velocity.x < 0 && left))
            velocity.x = 0;

        if (velocity.y < 0)
            velocity.y = checkDownSpeed(velocity.y);
        else if (velocity.y > 0)
            velocity.y = checkUpSpeed(velocity.y);
    }


    private float checkDownSpeed(float downSpeed)
    {

        if (
            world.CheckForVoxel(new Vector3(transform.position.x - playerWidth, transform.position.y + downSpeed, transform.position.z - playerWidth)) && (!left && !back) ||
            world.CheckForVoxel(new Vector3(transform.position.x + playerWidth, transform.position.y + downSpeed, transform.position.z - playerWidth)) && (!right && !back) ||
            world.CheckForVoxel(new Vector3(transform.position.x + playerWidth, transform.position.y + downSpeed, transform.position.z + playerWidth)) && (!right && !front) ||
            world.CheckForVoxel(new Vector3(transform.position.x - playerWidth, transform.position.y + downSpeed, transform.position.z + playerWidth)) && (!left && !front)

           )
        {
            isGrounded = true;
            return 0;
        }
        else
        {
            isGrounded = false;
            return downSpeed;
        }

    }

    private float checkUpSpeed(float upSpeed)
    {
        if (
            world.CheckForVoxel(new Vector3(transform.position.x - playerWidth, transform.position.y + 1.8f + upSpeed, transform.position.z - playerWidth)) && (!left && !back) ||
            world.CheckForVoxel(new Vector3(transform.position.x + playerWidth, transform.position.y + 1.8f + upSpeed, transform.position.z - playerWidth)) && (!right && !back) ||
            world.CheckForVoxel(new Vector3(transform.position.x + playerWidth, transform.position.y + 1.8f + upSpeed, transform.position.z + playerWidth)) && (!right && !front) ||
            world.CheckForVoxel(new Vector3(transform.position.x - playerWidth, transform.position.y + 1.8f + upSpeed, transform.position.z + playerWidth)) && (!left && !front)
           )
        {
            verticalMomentum = 0;
            // set to 0 so the player falls when their head hits a block while jumping
            return 0;

        }
        else
        {

            return upSpeed;

        }

    }

    public bool front
    {

        get
        {
            if (
                world.CheckForVoxel(new Vector3(transform.position.x, transform.position.y, transform.position.z + playerWidth)) || //ADDED 0.1F PER YOUTUBE COMMENTS AT VIDEO 17
                world.CheckForVoxel(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z + playerWidth))
                )
                return true;
            else
                return false;
        }

    }
    public bool back
    {

        get
        {
            if (
                world.CheckForVoxel(new Vector3(transform.position.x, transform.position.y, transform.position.z - playerWidth)) ||
                world.CheckForVoxel(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z - playerWidth))
                )
                return true;
            else
                return false;
        }

    }
    public bool left
    {

        get
        {
            if (
                world.CheckForVoxel(new Vector3(transform.position.x - playerWidth, transform.position.y, transform.position.z)) ||
                world.CheckForVoxel(new Vector3(transform.position.x - playerWidth, transform.position.y + 1f, transform.position.z))
                )
                return true;
            else
                return false;
        }

    }
    public bool right
    {

        get
        {
            if (
                world.CheckForVoxel(new Vector3(transform.position.x + playerWidth, transform.position.y, transform.position.z)) ||
                world.CheckForVoxel(new Vector3(transform.position.x + playerWidth, transform.position.y + 1f, transform.position.z))
                )
                return true;
            else
                return false;
        }

    }
}
