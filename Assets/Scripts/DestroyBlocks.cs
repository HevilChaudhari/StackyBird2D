using UnityEngine;

public class DestroyBlocks : MonoBehaviour
{

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    [SerializeField] LayerMask layer;


    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void OnEnable()
    {
        fireBullet.OnBonusLevel += DestroyBlockInstantly;
        GameManager.OnGameStateChanged += DestroyWhenRespawn;
        GameManager.OnGameStateChanged += PausedState;
        GameManager.OnGameStateChanged += PlyingState;
    }

    private void PlyingState(GameManager.GameState state)
    {
        if (state == GameManager.GameState.playing)
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        }
    }

    private void PausedState(GameManager.GameState state)
    {
        if (state == GameManager.GameState.gamepaused)
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        }
    }

    private void DestroyWhenRespawn(GameManager.GameState state)
    {
        if(state == GameManager.GameState.respawn)
        {
        Destroy(gameObject);

        }
    }

    private void DestroyBlockInstantly()
    {
        Invoke("DestroyBlock", 0.4f);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.right,0.5f,layer);
        

        if(hit.collider != null)
        {
            
            transform.SetParent(null);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            Invoke("DestroyBlock", 2f);
            
        }

    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void DestroyBlock()
    {
        
        Destroy(gameObject);
    }

    

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void OnDisable()
    {
        fireBullet.OnBonusLevel -= DestroyBlockInstantly;
        GameManager.OnGameStateChanged -= DestroyWhenRespawn;
        GameManager.OnGameStateChanged -= PausedState;
        GameManager.OnGameStateChanged -= PlyingState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.restart);
        }
    }
}
