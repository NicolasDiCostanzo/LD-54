using UnityEngine;
using UnityEngine.UI;

public class CompassManager : MonoBehaviour
{
    WeatherManager weatherManager;
    ItemHolder itemHolder;

    [SerializeField]
    Image compassBg;
    [SerializeField]
    Image needle;

    Vector3 exit;
    Transform player;

    public void Init()
    {
        exit = GameObject.Find("Exit").transform.position;
        player = GameObject.Find("Player").transform;
    }

    void Awake()
    {
        weatherManager = FindObjectOfType<WeatherManager>();
        itemHolder = FindObjectOfType<ItemHolder>();
    }

    void Update()
    {
        bool hasCompass = itemHolder.CurrentItem == ItemType.Compass;
        compassBg.enabled = hasCompass;
        needle.enabled = hasCompass;

        if (!hasCompass) return;

        if (weatherManager.CurrentWeatherState != WeatherManager.WeatherState.MagneticStorm)
        {

            Vector3 playerPos = new Vector3(player.position.x, 0, player.position.z);
            transform.rotation = GetNeedleRotation(playerPos);
        }
        else
        {
            // Random rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, Random.Range(0, 360)), .01f);
        }
    }

    Quaternion GetNeedleRotation(Vector3 playerPos)
    {
        float angle = Vector3.SignedAngle(Vector3.forward, exit - playerPos, Vector3.up);
        return Quaternion.Euler(new Vector3(0, 0, -(angle - 45)));
    }
}
