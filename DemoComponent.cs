/*
 * Author: 
 * Date: 
 * Description: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; 

public class DemoComponent : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
  

    /// <summary>
    /// The OnFire Event
    /// </summary>
    int score = 0;

    Vector3 moveData = Vector3.zero;
    Vector3 rotationInput = Vector3.zero;
    Vector3 headRotationInput = Vector3.zero;
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI LowerText;
    public GameObject playerCamera;
  


    void OnLook(InputValue value)
    {
        rotationInput.y = value.Get<Vector2>().x;
        headRotationInput.x = value.Get<Vector2>().y * -1;
    }

    float moveSpeed = 0.06f;
    float rotationSpeed = 0.3f;
    void OnMove(InputValue value)
    {
        moveData = value.Get<Vector2>();
        Debug.Log(moveData);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Collectible")
        {
            Destroy(collision.gameObject);
            score += 1;
            displayText.text = "Score : " + score + "/8";
            if (score == 8)
            {
                LowerText.text = "Congratulations!!";
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Collectible")
        {
            Debug.Log("Enter Trigger : " + collider.gameObject.name);
            LowerText.text = "you hit a box";
        }
        
    }

    

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log(gameObject.name + "has stop colliding into" + collision.gameObject.name);
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(gameObject.name + "is collided into" + collision.gameObject.name);
    }

    void OnEKeyboard()
    {
        GetComponent<Rigidbody>().
            AddForce(new Vector3(0,10,0), ForceMode.Impulse);
    }
    void Update()
    {
        Vector3 forwardDir = transform.forward;
        Vector3 rightDir = transform.right;
    



        transform.position += (forwardDir * moveData.y + rightDir * moveData.x) * moveSpeed;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotationInput * rotationSpeed);
        playerCamera.transform.rotation = Quaternion.Euler(playerCamera.transform.rotation.eulerAngles + headRotationInput * rotationSpeed);


    }
}
