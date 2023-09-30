using UnityEngine;

public class CompassManager : MonoBehaviour
{
    Vector3 exit;
    // Start is called before the first frame update
    void Start()
    {
        exit = GameObject.Find("Exit").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 player = GameObject.Find("Player").transform.position;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetNeedleRotation(player)));
    }

    float GetNeedleRotation(Vector3 playerPostion) {
        Vector3 delta = exit - playerPostion;
        delta.y = 0;

        return - Vector3.SignedAngle(delta, Vector3.forward, Vector3.up);
    }
}
