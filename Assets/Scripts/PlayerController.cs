using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject Pantsu;
    GameObject LosePantsu;
    public AudioClip malus;
    public AudioClip bonus;
    private AudioSource source;
    void Awake()
    {

        source = GetComponent<AudioSource>();
    }
    Collider2D myCollider;

    Vector3 m_movementH;
    Vector3 m_movementV;

    public float speed;

    float currentTimeB;
    float currentTimeP;
    float currentTimeC;
    float currentTimeS;

    public float RamenBuffTime;
    public float UmbrellaBuffTime;
    public float confusedTime;

    public int playerScoring;
    public int animState;

    bool keepPlayer;
    public bool isProtected;

    enum States { Default, Stunned, ForcedMovement, Confused, Dead, Boosted, Protected };
    States playerState;

    private IEnumerator coroutine;

    GameObject Kidnapper;

    Animator animator;

    Vector2 deltaTornado;

    // Use this for initialization
    void Start()
    {
        playerState = States.Default;
        keepPlayer = false;

        animState = 0;

        currentTimeB = 0f;
        currentTimeC = 0.0f;
        currentTimeP = 0f;
        animator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        isProtected = false;

        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        AnimationMovements();
        if (playerState == States.Dead)
        {
            myCollider.enabled = false;
            animator.Play("Yamete", 0);
        }
        else
        {
            /*
            if (isProtected)
            {
                gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 0.0f, 0.0f);
            }
            else if (playerState == States.Default)
            {
                gameObject.GetComponent<SpriteRenderer>().material.color = new Color(0.0f, 1.0f, 0.0f);
            }
            else if (playerState == States.Boosted)
            {
                gameObject.GetComponent<SpriteRenderer>().material.color = new Color(0.0f, 0.0f, 1.0f);
            }
            else if (playerState == States.Confused)
            {
                gameObject.GetComponent<SpriteRenderer>().material.color = new Color(0.0f, 1.0f, 1.0f);
            }
            else if (playerState == States.ForcedMovement)
            {
                gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f);
            }
            else if (playerState == States.Stunned)
            {
                gameObject.GetComponent<SpriteRenderer>().material.color = new Color(0.0f, 0.0f, 0.0f);
            }
            */
            PlayerMovement();
            AnimationMovements();
        }
    }

    void FixedUpdate()
    {
        if (playerState != States.ForcedMovement && playerState != States.Stunned && playerState != States.Dead)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            rb.MovePosition(transform.position + new Vector3(m_movementH.x, m_movementV.y, 0) * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
    }

    void PlayerMovement()
    {
        m_movementH.x = 0f;
        m_movementV.y = 0f;

        //Player Movement when the character is alive
        if (playerState == States.Default)
        {
            if ((Input.GetKey("z")) || (Input.GetKey("up")))
            {
                m_movementV.y = speed;
            }
            else if ((Input.GetKey("s")) || (Input.GetKey("down")))
            {
                m_movementV.y = (-speed);
            }
            else
            {
                m_movementV.y = 0f;
            }

            if ((Input.GetKey("q")) || (Input.GetKey("left")))
            {
                m_movementH.x = (-speed);
            }
            else if ((Input.GetKey("d")) || (Input.GetKey("right")))
            {
                m_movementH.x = speed;
            }
            else
            {
                m_movementH.x = 0f;
            }
        }

        //Player Movement when the character is stunned
        else if ((playerState == States.Stunned))
        {
            /*coroutine = Wait(2f);
            StartCoroutine(coroutine);*/
            currentTimeC += Time.deltaTime;
            if (currentTimeC >= 2.0f)
            {
                playerState = States.Default;
                currentTimeC = 0.0f;
                currentTimeS = 0f;
            }
        }

        //Player Movement when the character is confuse
        else if (playerState == States.Confused)
        {
            if ((Input.GetKey("s")) || (Input.GetKey("down")))
            {
                m_movementV.y = speed;
            }
            else if ((Input.GetKey("z")) || (Input.GetKey("up")))
            {
                m_movementV.y = (-speed);
            }
            else
            {
                m_movementV.y = 0f;
            }

            if ((Input.GetKey("d")) || (Input.GetKey("right")))
            {
                m_movementH.x = (-speed);
            }
            else if ((Input.GetKey("q")) || (Input.GetKey("left")))
            {
                m_movementH.x = speed;
            }
            else
            {
                m_movementH.x = 0f;
            }

            currentTimeC += Time.deltaTime;
            if (currentTimeC >= 5.0f)
            {
                playerState = States.Default;
                currentTimeC = 0.0f;
            }
        }
        else if (playerState == States.ForcedMovement)
        {
            Debug.Log(this.transform.position);
            this.transform.position += (Kidnapper.transform.position);
            Debug.Log("AFTER = " + this.transform.position);
            // - new Vector3(deltaTornado.x, deltaTornado.y, 0.0f);
            /*m_movementH.x = (Kidnapper.transform.position.x) - (this.gameObject.transform.position.x);
            m_movementV.y = (Kidnapper.transform.position.y) - (this.gameObject.transform.position.y);*/
        }
        else if (playerState == States.Boosted)
        {
            speed = 7.5f;
            if ((Input.GetKey("z")) || (Input.GetKey("up")))
            {
                m_movementV.y = speed;
            }
            else if ((Input.GetKey("s")) || (Input.GetKey("down")))
            {
                m_movementV.y = (-speed);
            }
            else
            {
                m_movementV.y = 0f;
            }

            if ((Input.GetKey("q")) || (Input.GetKey("left")))
            {
                m_movementH.x = (-speed);
            }
            else if ((Input.GetKey("d")) || (Input.GetKey("right")))
            {
                m_movementH.x = speed;
            }
            else
            {
                m_movementH.x = 0f;
            }
            currentTimeB += Time.deltaTime;
            if (currentTimeB >= 5.0f)
            {
                speed = 5f;
                currentTimeB = 0f;
                playerState = States.Default;
            }
        }
        else if (playerState == States.Protected)
        {
            if ((Input.GetKey("z")) || (Input.GetKey("up")))
            {
                m_movementV.y = speed;
            }
            else if ((Input.GetKey("s")) || (Input.GetKey("down")))
            {
                m_movementV.y = (-speed);
            }
            else
            {
                m_movementV.y = 0f;
            }

            if ((Input.GetKey("q")) || (Input.GetKey("left")))
            {
                m_movementH.x = (-speed);
            }
            else if ((Input.GetKey("d")) || (Input.GetKey("right")))
            {
                m_movementH.x = speed;
            }
            else
            {
                m_movementH.x = 0f;
            }
            currentTimeP += Time.deltaTime;
            if (currentTimeP >= 5.0f)
            {
                isProtected = false;
                currentTimeP = 0f;
                playerState = States.Default;
            }
        }
    }

    //waiting coroutine
    /*private IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        playerState = States.Default;
    }*/

    //Score incrementation
    void AddScore(int m_score)
    {
        source.PlayOneShot(bonus, 1.0f);
        HUDscore.score += m_score;
    }

    //Ramen buf application
    void RamenBuff()
    {
         if (playerState == States.Default)
        {
            source.PlayOneShot(bonus, 1.0f);
            playerState = States.Boosted;
            currentTimeB = 0f;
        }
    }

    //Umbrella protection application
    void UmbrellaProtection()
    {

        if (playerState == States.Default)
        {
            source.PlayOneShot(bonus, 1.0f);
            isProtected = true;
            playerState = States.Protected;
            currentTimeP = 0f;
        }
    }

    //Player collided with a tornado
    void Kidnapped(GameObject tornado)
    {
        if (playerState == States.Default)
        {
            playerState = States.ForcedMovement;
            Kidnapper = tornado;
            keepPlayer = true;
            deltaTornado = Kidnapper.transform.position - transform.position;
        }
    }

    //Player collided with a trash
    void Stunning()
    {

        if (playerState == States.Default || playerState == States.Confused)
        {
            source.PlayOneShot(malus, 1.0f);
            playerState = States.Stunned;
            currentTimeC = 0.0f;
        }
        else if (playerState == States.Protected)
        {
            playerState = States.Default;
            isProtected = false;
        }
    }

    void Confusing()
    {

        if (playerState == States.Default)
        {
            source.PlayOneShot(malus, 1.0f);
            playerState = States.Confused;
            currentTimeC = 0.0f;
        }
        else if (playerState == States.Protected)
        {
            playerState = States.Default;
            isProtected = false;
        }
    }

    //Animation manager
    void AnimationMovements()
    {
        if(playerState == States.Confused)
        {
            if (m_movementV.y < 0)
            {
                //Debug.Log("Je vais en bas");
                animator.Play("Paper_face", 0);
            }
            else if (m_movementV.y > 0)
            {
                //Debug.Log("Je vais en haut");
                animator.Play("Paper_dos", 0);
            }
            else if (m_movementH.x < 0)
            {
                //Debug.Log("Je vais à gauche");
                animator.Play("Paper_gauche", 0);
            }
            else if (m_movementH.x > 0)
            {
                // Debug.Log("Je vais à droite");
                animator.Play("Paper_droite", 0);
            }
            else
            {
                //Debug.Log("Pas bouger ");
                animator.Play("Paper_Idle", 0);
            }
        }
        else if ((playerState == States.Default) || (playerState == States.Boosted))
        {
            if (m_movementV.y < 0)
            {
                //Debug.Log("Je vais en bas");
                animator.Play("Course_face", 0);
            }
            else if (m_movementV.y > 0)
            {
                //Debug.Log("Je vais en haut");
                animator.Play("Course_dos", 0);
            }
            else if (m_movementH.x < 0)
            {
                //Debug.Log("Je vais à gauche");
                animator.Play("Course_gauche", 0);
            }
            else if (m_movementH.x > 0)
            {
                // Debug.Log("Je vais à droite");
                animator.Play("Course_droite", 0);
            }
            else
            {
                //Debug.Log("Pas bouger ");
                animator.Play("NoMove", 0);
            }
        }
        else if (playerState == States.Stunned)
        {
            animator.Play("Stun", 0);
        }
        else if (playerState == States.Protected)
        {
           
            if (m_movementV.y < 0)
            {
                //Debug.Log("Je vais en bas");
                animator.Play("Umbrella_face", 0);
            }
            else if (m_movementV.y > 0)
            {
                //Debug.Log("Je vais en haut");
                animator.Play("Umbrella_dos", 0);
            }
            else if (m_movementH.x < 0)
            {
                //Debug.Log("Je vais à gauche");
                animator.Play("Umbrella_gauche", 0);
            }
            else if (m_movementH.x > 0)
            {
                // Debug.Log("Je vais à droite");
                animator.Play("Umbrella_droite", 0);
            }
            else
            {
                //Debug.Log("Pas bouger ");
                animator.Play("Umbrella_Idle", 0);
                }
        }
    }

    //Player collided with a wind and died
    void DiePlayer()
    {
        playerState = States.Dead;
        Vector3 transition = new Vector3(0f, 0f, 10f);
        LosePantsu = GameObject.Instantiate(Pantsu, transition, Quaternion.identity) as GameObject;
        //SceneManager.LoadScene("DeadScreen", LoadSceneMode.Single);
        GameObject go = GameObject.Find("Game Controller");
        GameControllerScript other = (GameControllerScript)go.GetComponent<GameControllerScript>();
        other.setStateOver();
    }
    void OnCollisionEnter2D(Collision2D collide)
    {
        /*if (keepPlayer == true)
        {
            Debug.Log("Passage 1");
            if (collide.gameObject.tag == "Screen")
            {
                Debug.Log("Passage");
                playerState = States.Default;
                keepPlayer = false;
            }
        }*/
    }
}
