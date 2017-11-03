using RedWoods.Service;
using System;
using UnityEngine;
using Zenject;

using static RedWoods.Util.Range;

//namespace RedWoods.Player
//{
public class PlayerController : MonoBehaviour
{
    public int playerid = -1;
    public float speedModifier = 1;

    private IUserInputService userInputService;
    private ITimeService timeService;

    private int health;
    private int maxHealth = 3;
    private float invincibilityTime = 3f;
    private float lastDamageTime = 0;
    private bool facingRight = false;

    /// <summary>
    /// Time (in seconds) for which a player is invincible post-damage.
    /// Range: [0.0f, 5.0f]
    /// </summary>
    public float InvincibilityTime
    {
        get { return invincibilityTime; }
        set { invincibilityTime = Limit(value, 0, 5); }
    }

    /// <summary>
    /// The maximum health this player can have.
    /// Range: [2, 5]
    /// </summary>
    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = Limit(value, 2, 5); }
    }

    /// <summary>
    /// The current health of the player.  When set to zero, the player is killed.
    /// Range: [0, MaxHealth]
    /// </summary>
    public int Health
    {
        get { return health; }
        set
        {
            bool wasAlive = health > 0;
            health = Limit(value, 0, maxHealth);
            if (wasAlive && health == 0)
                Kill();
        }
    }

    /// <summary>
    /// Notifies on player death
    /// </summary>
    public event EventHandler OnDeath;

    // Used by ZenJect for Dependency Injection
    [Inject]
    public void Construct(IUserInputService _userInputService, ITimeService _timeService)
    {
        userInputService = _userInputService;
        timeService = _timeService;
    }

    // Use this for initialization
    public void Start()
    {
        Health = MaxHealth;
    }

    public void FixedUpdate()
    {
        var horz = userInputService.GetAxis("Horizontal", playerid);
        var vert = userInputService.GetAxis("Vertical", playerid);
        var direction = new Vector2(horz, vert);
        var speed = speedModifier * Limit(direction.magnitude, 0, 1);

        GetComponent<Rigidbody2D>().velocity = speed * direction.normalized;

        if ((horz > 0 && facingRight) || (horz < 0 && !facingRight))
            Flip();
    }

    /// <summary>
    /// Flips the direction the player is facing.
    /// Also inverts the scale of the player to flip sprites
    /// </summary>
    public void Flip()
    {
        facingRight = !facingRight;
        var newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    /// <summary>
    /// Applies a single point of damage to the player
    /// </summary>
    public void Damage()
    {
        float time = timeService.GetTime();
        if (Health > 0 && time > lastDamageTime + invincibilityTime)
        {
            lastDamageTime = time;
            Health--;
        }
    }

    private void LateUpdate()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100);
    }

    public void Kill()
    {
        OnDeath?.Invoke(this, null);
    }
}

//}