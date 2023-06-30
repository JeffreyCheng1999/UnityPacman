using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;
    public float miniutesAfterGostWillGetFaster = 1;
    public float timer = 0;
    //public Movement[] ghosts;
    public bool isPlayer;

    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }

    public GhostFrightened ghostFrightened;

    public Color OldRed;
    public Color NewRed;
    
    public Color OldCyan;
    public Color NewCyan;

    public Color OldPink;
    public Color NewPink;

    public Color OldOrange;
    public Color NewOrange;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.startingPosition = this.transform.position;
    }
    private void Start()
    {
        ResetState();
        OldRed = new Color(1, 0, 0, 1); 
        //NewRed = new Color(139 / 255, 0, 0, 1);

        OldCyan = new Color(0, 1, 1, 1);
        //NewCyan = new Color(0, 0.3867925f, 0.3867925f, 1);

        OldPink = new Color(0.9882352941176471f, 0.7098039215686275f, 1, 1);
        //NewPink = new Color(0.4509803921568627f, 0.1490196078431373f, 0.4627450980392157f, 1);

        OldOrange = new Color(248 / 255, 187 / 255, 85 / 255, 1);
        //NewOrange = new Color(102 / 255f, 64 / 255, 0, 1);
    }
    public void ResetState()
    {
        this.speedMultiplier = 1.0f;
        this.direction = this.initialDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startingPosition;
        this.rigidbody.isKinematic = false;
        this.enabled = true;
    }

    private void Update()
    {
        if (GameManager.instance.IsGameStarted) { 
            
            if (this.nextDirection != Vector2.zero)
            {
                SetDirection(this.nextDirection);
            }
            if (UIManager.instance.hasTimer)
            {
                timer += Time.deltaTime;
                if (timer > miniutesAfterGostWillGetFaster * 60)
                {
                    timer = 0;
                    if (!isPlayer)
                    {
                        ChangeColors();
                        speed *= 1.15f;
                    }
                }
            }
            else {
                if (!isPlayer)
                {
                    RevertColors();
                }
            }
        }
    }

    public void RevertColors() {

        if (ghostFrightened.body.color == NewRed)
        {
            ghostFrightened.body.color = OldRed;
        }
        //Cyan
        else if (ghostFrightened.body.color == NewCyan)
        {
            ghostFrightened.body.color = OldCyan;
        }
        //Pink
        else if (ghostFrightened.body.color == NewPink)
        {
            ghostFrightened.body.color = OldPink;
        }
        //Oranage
        else if (ghostFrightened.body.color == NewOrange)
        {
            ghostFrightened.body.color = OldOrange;
        }
    }
    public void ChangeColors()
    {
        if (ghostFrightened.body.color == OldRed)
        {
            ghostFrightened.body.color = NewRed;
        }
        //Cyan
        else if (ghostFrightened.body.color == OldCyan)
        {
            ghostFrightened.body.color = NewCyan;
        }
        //Pink
        else if (ghostFrightened.body.color == OldPink)
        {
            ghostFrightened.body.color = NewPink;
        }
        //Oranage
        else if (ghostFrightened.body.color == OldOrange)
        {
            ghostFrightened.body.color = NewOrange;
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.instance.IsGameStarted)
        {
            Vector2 position = this.rigidbody.position;
            Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
            this.rigidbody.MovePosition(position + translation);
        }
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            this.nextDirection = Vector2.zero;
        }
        else
        {
            this.nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, this.obstacleLayer);
        return hit.collider != null;
    }
}
