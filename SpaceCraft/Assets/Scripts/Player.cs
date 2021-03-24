using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public bool isGrounded;
    public bool isSprinting;
    public GameObject hurtPanel;
    public GameObject gameOverPanel;

    public AudioSource jumpAudio;
    public AudioSource mineAudio;
    public AudioSource hurtAudio;

    public bool hasSuperGun;

    public float Health = 5;
    public GameObject[] Hearts;
    public bool canBeAttacked;

    private Transform cam;
    private World world;

    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;
    public float jumpForce = 5f;
    public float gravity = -9.8f;

    public float playerWidth = 0.3f;
    public float boundsTolerance = 0.1f;

    private float horizontal;
    private float vertical;
    private float mouseHorizontal;
    private float mouseVertical;
    private Vector3 velocity;
    private float verticalMomentum = 0;
    private bool jumpRequest;

    public Transform highlightBlock;
    public Transform placeBlock;
    public float checkIncrement = 0.1f;
    public float reach = 8f;

    public Toolbar toolbar;

    //DEVNOOB EDIT
    public bool hasPlacedBlock = false;
    public bool isMining;
    public Vector3 startMiningPos;
    Coroutine mineCouro = null;
    float rotationX = 0;
    public float lookXLimit = 45.0f;
    public GameObject shotParticle;

    private void Start()
    {
        hasSuperGun = false;
        cam = GameObject.Find("Main Camera").transform;
        world = GameObject.Find("World").GetComponent<World>();
        canBeAttacked = true;
        world.inUI = false;
    }

    private void FixedUpdate()
    {
        if (!world.inUI)
        {
            CalculateVelocity();
            if (jumpRequest)
                Jump();

            transform.Translate(velocity, Space.World);
        }
    }

    public void GotHurt()
    {
        StartCoroutine(GotHurtt());
    }

    IEnumerator GotHurtt()
    {
        foreach (GameObject heart in Hearts)
        {
            heart.SetActive(false);
        }

        hurtPanel.SetActive(true);
        hurtPanel.GetComponent<Animation>().Play();
        Health -= 1;
        hurtAudio.Play();
        if (Health <= 0)
        {
            Debug.Log("Died");
            gameOverPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }

        for (int i = 0; i < Health; i++)
        {
            Hearts[i].SetActive(true);
        }

        yield return new WaitForSeconds(1);
        hurtPanel.SetActive(false);
        canBeAttacked = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            world.inUI = !world.inUI;
        }
        

        if (!world.inUI)
        {
            GetPlayerInputs();
            /*transform.Rotate(Vector3.up * mouseHorizontal * world.settings.mouseSensitivity);
            cam.Rotate(Vector3.right * -mouseVertical * world.settings.mouseSensitivity);*/
            rotationX += -Input.GetAxis("Mouse Y") * world.settings.mouseSensitivity;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * world.settings.mouseSensitivity, 0);
            placeCursorBlocks();
        }

        if (transform.position.y < -10)
        {
            transform.position = new Vector3(transform.position.x, 200, transform.position.z);
        }
    }

    void Jump()
    {

        verticalMomentum = jumpForce;
        isGrounded = false;
        jumpRequest = false;
        jumpAudio.Play();
    }

    private void CalculateVelocity()
    {
        // Affect vertical momentum with gravity.
        if (verticalMomentum > gravity)
            verticalMomentum += Time.fixedDeltaTime * gravity;

        // if we're sprinting, use the sprint multiplier.
        if (isSprinting)
            velocity = ((transform.forward * vertical) + (transform.right * horizontal)) * Time.fixedDeltaTime * sprintSpeed;
        else
            velocity = ((transform.forward * vertical) + (transform.right * horizontal)) * Time.fixedDeltaTime * walkSpeed;

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

    private void GetPlayerInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseHorizontal = Input.GetAxis("Mouse X");
        mouseVertical = Input.GetAxis("Mouse Y");

        if (Input.GetButtonDown("Sprint"))
            isSprinting = true;
        if (Input.GetButtonUp("Sprint"))
            isSprinting = false;

        if (isGrounded && Input.GetButtonDown("Jump"))
            jumpRequest = true;


        //Kill markuys
        if (Input.GetMouseButton(0) && toolbar.slots[toolbar.ActiveItem].isWeapon)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Markus") && hit.distance < reach) 
                {
                    Debug.Log("Hit markus");
                    //hurtAudio.Play();
                    //remove health from markus
                    if (!hasSuperGun)
                    {
                        hit.transform.GetComponent<Markus>().health -= 0.25f;
                    }
                    else
                    {
                        hit.transform.GetComponent<Markus>().health -= 2;
                    }
                    //Play anim if has
                    if (toolbar.slots[toolbar.ActiveItem].inHandObject != null)
                    {
                        if (toolbar.slots[toolbar.ActiveItem].inHandObject.GetComponent<Animation>())
                        {
                            toolbar.slots[toolbar.ActiveItem].inHandObject.GetComponent<Animation>().Play();
                            GameObject shotpart = Instantiate(shotParticle, toolbar.slots[toolbar.ActiveItem].inHandObject.transform.position, transform.rotation) as GameObject;
                            shotpart.transform.LookAt(hit.point);
                            shotpart.GetComponent<Rigidbody>().AddForce((hit.point - toolbar.slots[toolbar.ActiveItem].inHandObject.transform.position).normalized * 900);
                        }
                    }
                }
            }
        }

        //Destroy block
        if (Input.GetMouseButton(0))
        {
            if (highlightBlock.gameObject.activeSelf)
            {
                //Check what its hitting
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Hit smth in front of a block");
                }
                else
                {
                    if (toolbar.slots[toolbar.ActiveItem].amount > 0)
                    {
                        if (toolbar.slots[toolbar.ActiveItem].isWeapon && !isMining)
                        {
                            //Start the mining
                            isMining = true;
                            startMiningPos = highlightBlock.position;
                            mineCouro = StartCoroutine(Miner());
                        }
                    }

                    //Play anim if has
                    if (toolbar.slots[toolbar.ActiveItem].inHandObject != null)
                    {
                        if (toolbar.slots[toolbar.ActiveItem].inHandObject.GetComponent<Animation>())
                        {
                            toolbar.slots[toolbar.ActiveItem].inHandObject.GetComponent<Animation>().Play();
                            GameObject shotpart = Instantiate(shotParticle, toolbar.slots[toolbar.ActiveItem].inHandObject.transform.position, transform.rotation) as GameObject;
                            Vector3 shotPos = new Vector3(highlightBlock.position.x + 0.5f, highlightBlock.position.y + 0.5f, highlightBlock.position.z + 0.5f);
                            shotpart.transform.LookAt(shotPos);
                            shotpart.GetComponent<Rigidbody>().AddForce((shotPos - toolbar.slots[toolbar.ActiveItem].inHandObject.transform.position).normalized * 900);
                        }
                    }
                }
                
            }
        }

        if (isMining)
        {
            if (startMiningPos != highlightBlock.position)
            {
                //stop mining
                isMining = false;
                StopCoroutine(mineCouro);
            }
            if (Input.GetMouseButtonUp(0))
            {
                isMining = false;
                StopCoroutine(mineCouro);
            }
        }

        //Place block
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("hitting smth");
            }
            else
            {
                if (placeBlock.gameObject.activeSelf)
                {
                    if (toolbar.slots[toolbar.ActiveItem].amount > 0)
                    {
                        Debug.Log("got this far");
                        if (toolbar.slots[toolbar.ActiveItem].isBlock && !hasPlacedBlock && Vector3.Distance(new Vector3(placeBlock.position.x + 0.5f, placeBlock.position.y + 0.5f, placeBlock.position.z + 0.5f), cam.transform.position) > 1f)
                        {
                            Debug.Log(Vector3.Distance(new Vector3(placeBlock.position.x + 0.5f, placeBlock.position.y + 0.5f, placeBlock.position.z + 0.5f), cam.transform.position));
                            world.GetChunkFromVector3(placeBlock.position).EditVoxel(placeBlock.position, (byte)toolbar.slots[toolbar.ActiveItem].itemInIdList);
                            toolbar.slots[toolbar.ActiveItem].amount -= 1;
                            toolbar.UpdateStack();
                        }
                    }
                }
            }
        }

    }

    IEnumerator Miner()
    {
        if (!hasSuperGun)
        {
            yield return new WaitForSeconds(1);
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
        }
        if (isMining)
        {
            mineAudio.Play();
            //Add block to inventory
            ChunkCoord thisChunk = new ChunkCoord(highlightBlock.position);
            VoxelState chunk = world.chunks[thisChunk.x, thisChunk.z].GetVoxelFromGlobalVector3(highlightBlock.position);
            BlockType block = world.blocktypes[world.chunks[thisChunk.x, thisChunk.z].GetVoxelFromGlobalVector3(highlightBlock.position).id];

            string name = block.blockName;
            int ID = chunk.id; //Get ID
            Sprite image = block.icon;
            bool isweapon = block.isWeapon;
            bool isblock = block.isBlock;
            int Amount = 1;
            Texture2D cubeSprite = null;
            if (block.cubeSprite != null)
            {
                cubeSprite = block.cubeSprite;
            }
            else
            {
                cubeSprite = null;
            }
            //GameObject inHandObj = ;
            toolbar.AddItemToToolbar(name, ID, image, isweapon, isblock, Amount, cubeSprite, block.inHandObject);

            //Destroy block
            isMining = false;
            world.GetChunkFromVector3(highlightBlock.position).EditVoxel(highlightBlock.position, 0);
        }
        isMining = false;
    }

    private void placeCursorBlocks()
    {

        float step = checkIncrement;
        Vector3 lastPos = new Vector3();



        while (step < reach)
        {

            Vector3 pos = cam.position + (cam.forward * step);

            if (world.CheckForVoxel(pos))
            {

                highlightBlock.position = new Vector3(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y), Mathf.FloorToInt(pos.z));
                placeBlock.position = lastPos;

                if (toolbar.slots[toolbar.ActiveItem].isWeapon)
                {
                    highlightBlock.gameObject.SetActive(true);
                    placeBlock.gameObject.SetActive(false);
                }

                /*if (toolbar.slots[toolbar.ActiveItem].isBlock && cam.transform.localRotation.x < 0.4f)
                {
                    //Debug.Log("Can place");
                    placeBlock.gameObject.SetActive(true);
                    highlightBlock.gameObject.SetActive(false);
                }
                if (toolbar.slots[toolbar.ActiveItem].isBlock && cam.transform.localRotation.x >= 0.4f)
                {
                    //Debug.Log("Cannot place");
                    highlightBlock.gameObject.SetActive(false);
                    placeBlock.gameObject.SetActive(false);
                }*/

                if (toolbar.slots[toolbar.ActiveItem].isBlock)
                {
                    if (cam.transform.localRotation.x < 0.4f)
                    {
                        //can play regardless
                        placeBlock.gameObject.SetActive(true);
                        highlightBlock.gameObject.SetActive(false);
                    }
                    if (cam.transform.localRotation.x >= 0.4f)
                    {
                        // if looking down, but distance allows (jumping or ledge)
                        Debug.Log(Vector3.Distance(pos, cam.transform.position));
                        //if (Vector3.Distance((new Vector3(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y), Mathf.FloorToInt(pos.z))), cam.transform.position) > 1.3)
                        if (Vector3.Distance(pos, cam.transform.position) > 2.5)
                        {
                            placeBlock.gameObject.SetActive(true);
                            highlightBlock.gameObject.SetActive(false);
                        }
                        else
                        {
                            //Debug.Log("Cannot place");
                            highlightBlock.gameObject.SetActive(false);
                            placeBlock.gameObject.SetActive(false);
                        }
                    }
                }
                return;

            }

            lastPos = new Vector3(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y), Mathf.FloorToInt(pos.z));

            step += checkIncrement;

        }

        highlightBlock.gameObject.SetActive(false);
        placeBlock.gameObject.SetActive(false);
            

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