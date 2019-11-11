using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that moves the camera to the players location with every call to update.
 * Camera movement is done in Late Update to allow the player to move first.
 */
public class CameraMoveMouse : MonoBehaviour
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

    public Vector2 input;

    public static CameraMoveMouse Instance
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
        input = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        input.x = Mathf.Clamp(input.x, 0, Screen.width);
        input.y = Mathf.Clamp(input.y, 0, Screen.height);
        input -= new Vector2(Screen.width / 2, Screen.height / 2);

        input.x /= (Screen.width / 2);
        input.y /= (Screen.height / 2);

        // calculate camera offset
        Vector2 desiredOffset = new Vector2(maxOffset.x * input.x, maxOffset.y * input.y);
        currentOffset = Vector2.Lerp(currentOffset, desiredOffset, baseSmooth);

        // calculate camera position
        float x = target.position.x + currentOffset.x + setOffset.x;
        float y = target.position.y + currentOffset.y + setOffset.y;
        float z = setOffset.z;
        Vector3 newPos = new Vector3(x, y, z);

        // set camera position
        transform.position = newPos; 
    }
}
