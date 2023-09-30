using UnityEngine;

public class CompassManager : MonoBehaviour
{
    Vector3 exit;
    Transform player;

    public void Init()
    {
        exit = GameObject.Find("Exit").transform.position;
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        Vector3 playerPos = new Vector3(player.position.x, 0, player.position.z);
        transform.rotation = GetNeedleRotation(playerPos);
    }

    Quaternion GetNeedleRotation(Vector3 playerPos)
    {
        float angle = Vector3.SignedAngle(Vector3.forward, exit - playerPos, Vector3.up);
        return Quaternion.Euler(new Vector3(0, 0, -(angle - 45)));
    }
}
