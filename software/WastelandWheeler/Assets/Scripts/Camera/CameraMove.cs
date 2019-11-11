using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that moves the camera to the players location with every call to update.
 * Camera movement is done in Late Update to allow the player to move first.
 */
public class CameraMove : MonoBehaviour
{
    public Vector3 setOffset = new Vector3(0,0,-5);

    private Transform target;

    private Camera cam;
    public float height;
    public float width;

    private Vector2 currentOffset;
    private Vector2 maxOffset;
    public float xoff = 0.30f;
    public float yoff = 0.30f;

    public float baseSmooth = 0.01f;
    public float correction = 0.10f;

    public static CameraMove Instance
    {
        get;
        set;
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // When game starts, find the player object
    void Start()
    {
        cam = GetComponent<Camera>();

        target = GameObject.FindWithTag("Player").transform;

        currentOffset = new Vector2(0, 0);
    }

    // After update is called, move the position of the camera to the position of the player
    void LateUpdate()
    {
        // do nothing if player is dead
        if (target == null) return;

        // get camera dimensions
        if (height != 2f * cam.orthographicSize)
        {
            height = 2f * cam.orthographicSize;
            width = height * cam.aspect;
        }
        maxOffset = new Vector2((width / 2) * xoff, (height / 2) * yoff);

        // get input
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // calculate camera offset
        Vector2 desiredOffset = new Vector2(maxOffset.x * input.x, maxOffset.y * input.y);
        float smooth = baseSmooth * (1 + correction - correction * Vector2.Dot(currentOffset.normalized, input.normalized));
        currentOffset = Vector2.Lerp(currentOffset, desiredOffset, smooth);

        // calculate camera position
        float x = target.position.x + currentOffset.x + setOffset.x;
        float y = target.position.y + currentOffset.y + setOffset.y;
        float z = setOffset.z;
        Vector3 newPos = new Vector3(x, y, z);

        // set camera position
        transform.position = newPos; 
    }
}
