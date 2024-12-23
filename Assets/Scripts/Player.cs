using System.Collections;
using UnityEngine;



public class Player : MonoBehaviour
{

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    [SerializeField] private Transform blockSpawnpoint;
    [SerializeField] private GameObject blockPrefab, birdBody;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Transform clickableArea;

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private bool canSpawnBlocks;
    private bool maxHieght;
    private bool isDead;
    private Vector3 lastCheckPoint;

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += StartState;
        GameManager.OnGameStateChanged += PlayingState;
        GameManager.OnGameStateChanged += RestartState;
        GameManager.OnGameStateChanged += FinishState;
        GameManager.OnGameStateChanged += RespawnPlayer;
        GameManager.OnGameStateChanged += GamePausedState;
    }

    private void GamePausedState(GameManager.GameState state)
    {
        if (state == GameManager.GameState.gamepaused)
        {
            canSpawnBlocks = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        }
    }

    private void RespawnPlayer(GameManager.GameState state)
    {
        if (state == GameManager.GameState.respawn)
        {
            player.transform.position = new Vector3(lastCheckPoint.x, 0f, 0f);
            this.transform.position = new Vector3(player.transform.position.x - 3.5f, 0.5f, transform.position.z);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }


    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void StartState(GameManager.GameState state)
    {
        if (state == GameManager.GameState.start)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        }
    }



    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void PlayingState(GameManager.GameState state)
    {
        if (state == GameManager.GameState.playing)
        {
            isDead = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine("CanSpawnBlock");
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------

    private IEnumerator CanSpawnBlock()
    {
        yield return new WaitForSeconds(.2f);
        canSpawnBlocks = true;
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void RestartState(GameManager.GameState state)
    {
        if (state == GameManager.GameState.restart)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            AudioManager.Instance.playSound("PlayerDied");
            canSpawnBlocks = false;

        }
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void FinishState(GameManager.GameState state)
    {
        if (state == GameManager.GameState.finish)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            AudioManager.Instance.playSound("LevelFinish");
            canSpawnBlocks = false;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= StartState;
        GameManager.OnGameStateChanged -= PlayingState;
        GameManager.OnGameStateChanged -= RestartState;
        GameManager.OnGameStateChanged -= FinishState;
        GameManager.OnGameStateChanged -= RespawnPlayer;
        GameManager.OnGameStateChanged -= GamePausedState;

    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------
    // Update is called once per frame

    void Update()
    {

        //Set limit of BlockSpawn with max height
       if (!maxHieght)
        {
            SpawnBlocks();
        }

        //playerdead
        if (!isDead)
        {
            playerDead();
        }

    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------
    //Spawn Blocks
    public void SpawnBlocks()
    {
        if (Input.GetMouseButtonDown(0) && canSpawnBlocks)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null && hit.collider.CompareTag("ClickableArea"))
            {
                Debug.Log("Clicked on the BoxCollider!");
                transform.position += new Vector3(0f, 1f, 0f);
                GameObject block = Instantiate(blockPrefab, blockSpawnpoint);
                block.transform.localPosition = Vector3.zero;
                block.GetComponent<SpriteRenderer>().color = birdBody.GetComponent<SpriteRenderer>().color;
                block.transform.SetParent(player);
                AudioManager.Instance.playSound("StackUp");

                
            }
        }



    }




    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------
    //player dead

    private void playerDead()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, layer);
        if (hit.collider != null)
        {
            Handheld.Vibrate();
            Debug.Log("RestartEventTrigger");
            GameManager.Instance.UpdateGameState(GameManager.GameState.restart);
            isDead = true;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------
    //detect collision

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //maxheight(safeArea)
        if (collision.gameObject.CompareTag("Boundry"))
        {
            maxHieght = true;
        }

        //CompleteLevel
        if (collision.gameObject.layer == 8)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.finish);
            canSpawnBlocks = false;
        }

        //CollectCoin
        if (collision.gameObject.layer == 7)
        {
            Destroy(collision.gameObject);
            UIManager.instance.addCoin();

        }

        if (collision.gameObject.layer == 9)
        {
            lastCheckPoint = collision.transform.position;
        }

        if (collision.gameObject.layer == 11)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.restart);
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------
    //exit collision

    private void OnTriggerExit2D(Collider2D collision)
    {
        maxHieght = false;
    }

}
