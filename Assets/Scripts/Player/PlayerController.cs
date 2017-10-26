using RedWoods.Service;
using System;
using UnityEngine;
using Zenject;

using static RedWoods.Util.Range;

//namespace RedWoods.Player
//{
public class PlayerController : MonoBehaviour
{
    private IUserInputService userInputService;
    private ITimeService timeService;
    private int health;
    private int maxHealth = 3;
    private float invincibilityTime = 3f;
    private float lastDamageTime = 0;
    public int playerid = -1;
    public float speedModifier = 1;
    private bool facingRight = false;

    public float InvincibilityTime
    {
        get { return invincibilityTime; }
        set { invincibilityTime = Limit(value, 0, 5); }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = Limit(value, 2, 5); }
    }

    public int Health
    {
        get { return health; }
        set { health = Limit(value, 0, maxHealth); }
    }

    public event EventHandler OnDeath;

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

    public void Flip()
    {
        facingRight = !facingRight;
        var newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    public void Damage()
    {
        float time = timeService.GetTime();
        if (Health > 0 && time > lastDamageTime + invincibilityTime)
        {
            lastDamageTime = time;
            Health--;
            if (Health == 0)
                Kill();
        }
    }

    public void Kill()
    {
        OnDeath?.Invoke(this, null);
    }
}

//}