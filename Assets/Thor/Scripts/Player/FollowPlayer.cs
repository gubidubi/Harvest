using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    private float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -10f;

        Vector3 finalPos = Vector3.Lerp(playerPos, mousePos, 0.2f);

        gameObject.GetComponent<Rigidbody>().velocity = (finalPos - gameObject.transform.position) * followSpeed;
    }
}
