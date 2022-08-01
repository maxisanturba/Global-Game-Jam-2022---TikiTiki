using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_FSM : MonoBehaviour
{
    #region Variables

    public int playerColor;
    public float speedMov;
    public GameObject bulletObject;
    public GameObject anchorPoint;
    public GameObject pointerBar;
    public GameObject powerBar;
    public Slider sliderPower;

    public float angle = 0;
    public float t;

    public GameController gameController;


    public AudioSource audioSource;
    public AudioClip shootClip;

    #endregion


    #region State Machine Declarations

    private PlayerBaseState currentState;

    public readonly PlayerIdleState idleState = new PlayerIdleState();
    public readonly PlayerMovAndDirState movAndDirState = new PlayerMovAndDirState();
    public readonly PlayerPowerState powerState = new PlayerPowerState();
    public readonly PlayerShootState shootState = new PlayerShootState();

    #endregion
    private void Start()
    {
        if (!gameController.gameOver)
        {
            TransitionToState(idleState);
        }
    }
    private void Update()
    {
        if (!gameController.gameOver)
        {
            currentState.Update(this);
        }
    }
    public void TransitionToState(PlayerBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void ReleaseTheBullet()
    {
        audioSource.PlayOneShot(shootClip);
        Instantiate(bulletObject, anchorPoint.transform.position, Quaternion.identity);
    }

    public void Aiming()
    {
        var angleToAim = anchorPoint.transform.localEulerAngles;
        angleToAim = new Vector3(angleToAim.x, angleToAim.y, AngleOscillation());
        anchorPoint.transform.localEulerAngles = angleToAim;
    }
    private float AngleOscillation()
    {
        angle = Mathf.PingPong(t, 150);
        t += 100 * Time.deltaTime;
        return angle;
    }
    public static bool Shoot(Player_FSM player)
    {
        switch (player.playerColor)
        {
            case 1:
            {
                var shootButton = Input.GetButtonDown("Fire_01");
                return shootButton;
            }
            case 2:
            {
                var shootButton = Input.GetButtonDown("Fire_02");
                return shootButton;
            }
            default:
                return false;
        }
    }
    public static void HorizontalMovement(Player_FSM player)
    {
        switch (player.playerColor)
        {
            case 1:
            {
                var horizontal = Input.GetAxis("Horizontal_01") * player.speedMov;
                player.transform.Translate(Vector3.right * horizontal * Time.deltaTime);
                break;
            }
            case 2:
            {
                var horizontal = Input.GetAxis("Horizontal_02") * player.speedMov;
                player.transform.Translate(Vector3.right * -horizontal * Time.deltaTime);
                break;
            }
            default:
                Debug.LogWarning("No Player Assigned");
                break;
        }
    }
}
