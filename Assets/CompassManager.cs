using UnityEngine;

public class CompassManager : MonoBehaviour
{
    Vector3 exit;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        exit = GameObject.Find("Exit").transform.position;
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetNeedleRotation(player.position)));
    }

    float GetNeedleRotation(Vector3 playerPostion) {
        Vector3 delta = exit - playerPostion;
        delta.y = 0;

        // TODO 
        return Vector3.SignedAngle(exit, playerPostion, Vector3.up);
        // return - Vector3.SignedAngle(delta, Vector3.forward, Vector3.up);
    }
}
