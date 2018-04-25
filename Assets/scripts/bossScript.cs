using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossScript : MonoBehaviour {

    public float rightSpeed;
    public float leftSpeed;
    public float attackSpeed;
    public float rotationSpeed;
    public float damage;
    public float xOffset;
    public float health;

    private GameObject greenFeathers;
    private GameObject player;
    private Vector3 myPos;
    private SpriteRenderer spriteRender;
    private Vector3 camPos;
    private bool isDead = false;

    private Vector3 playerPos;
    private Vector3 attackPos;
    private Vector3 retractPos;
    private bool isAttacking = false;
    private bool doneAttacking = true;
    private bool doneRetracting = true;
    private bool flyLeft = true;


    private GameObject upgradeManager;

    // Use this for initialization
    void Start ()
    {
        upgradeManager = GameObject.FindGameObjectWithTag("UpgradeManager");
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRender = this.GetComponent<SpriteRenderer>();
        greenFeathers = Resources.Load("greenFeathers", typeof(GameObject)) as GameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (!isDead)
        {
            playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            Fly();
            

            if (flyLeft)
            {
                
                // The boss is flying left, melee attack when close to player
                MeleeAttack();
            }

        }
	}

    private void Fly()
    {
        if (!isAttacking)
        {
            if (flyLeft)
            {
                // The bird spawned to the left
                transform.Translate(Vector3.left * leftSpeed * Time.deltaTime);
            }
            else
            {
                // The bird spawned to the right
                transform.Translate(Vector3.right * rightSpeed * Time.deltaTime);
            }
            
        }

        // This will calculate if the boss hits the border of screen and will have to change direction
        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        myPos = transform.position;
        if (leftBorder > myPos.x)
        {
            //Debug.Log("LeftBorder is bigger than my pos");
            flyLeft = false;
            spriteRender.flipX = false;
            isAttacking = false;
        }
        else if (rightBorder < myPos.x)
        {
            //Debug.Log("RightBorder is lower than my pos");
            flyLeft = true;
            spriteRender.flipX = true;
            isAttacking = false;
        }
    }

    private void MeleeAttack()
    {
        float randomFloat = Random.Range(3.5f, 9.5f);
        if (transform.position.x > player.transform.position.x &&
            transform.position.x - randomFloat < player.transform.position.x)
        {
            // Check that it is not already attacking
            if (!isAttacking)
            {
                //Debug.Log("BOSS IS ATTACKING NOW!");
                isAttacking = true;
                doneAttacking = false;
                doneRetracting = false;
                attackPos = new Vector3(playerPos.x + 2, -1, playerPos.z);
            }
        }

        if (isAttacking)
        {
            // Check if it is done attacking, if not, fly fast in attack
            if (!doneAttacking)
            {
                transform.position = Vector3.MoveTowards(transform.position, attackPos, attackSpeed * Time.deltaTime);

                Vector3 direction = (transform.position - attackPos);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            }
            else // If done attacking, fly slower back in the air
            {
                transform.position = Vector3.MoveTowards(transform.position, retractPos, (attackSpeed - 5) * Time.deltaTime);

                Vector3 direction = (transform.position - retractPos);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            }

            // Check if it reached attacking point, and change it to doneAttacking so it can fly back in the air
            if (Vector3.Distance(transform.position, attackPos) <= 0)
            {
                if (!doneAttacking)
                {
                    doneAttacking = true;
                    //Debug.Log("BOSS HAS REACHED ATTACK POINT!");
                    retractPos = new Vector3(playerPos.x, 8, playerPos.z);
                }
            }

            // Check if it is done retracting from attack
            if (Vector3.Distance(transform.position, retractPos) <= 0)
            {
                if (!doneRetracting)
                {
                    isAttacking = false;
                    //Debug.Log("BOSS HAS REACHED RETRACT POINT!");
                    transform.rotation = Quaternion.identity;
                    doneRetracting = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enters trigger");
        //Debug.Log(collision.tag);
        if (collision.tag == "Player")
        {
            if (!isDead) // If not dead
            {
                player.SendMessage("TakeMeleeDamage", damage);
            }
        }
        else if (collision.tag == "Weapon")
        {
            Debug.Log("enters weapon");
            GameObject.FindGameObjectWithTag("AudioManager").SendMessage("PlayBossTakeDamageSound");
            health -= collision.gameObject.GetComponent<weaponScript>().damage;
            if (health <= 0)
            {
                Debug.Log("enters health 0");
                isDead = true;
                Destroy(gameObject);
                GameObject feathers = (GameObject)Instantiate(greenFeathers, transform.position, transform.rotation);
                Destroy(feathers, 3f);
                GameObject.FindGameObjectWithTag("AudioManager").SendMessage("PlayBossSpawnSound");
                GameObject.Find("birdSpawner").SendMessage("StartStopBossFight", false);

                // call spawn weapon from upgrade manager
                Debug.Log("spawn weapon");
                upgradeManager.GetComponent<upgradeManager>().SendMessage("spawnWeapon");


                // update score
                player.SendMessage("updateScoreP",5);
            }
        }
    }
}
