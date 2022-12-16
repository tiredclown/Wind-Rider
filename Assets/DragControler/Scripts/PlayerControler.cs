using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControler : MonoBehaviour
{




    Rigidbody m_Rigidbody;
    public Transform Child;
    public float ChildHSpeed, forwardSpeed;
    public float maxClampX, maxClampY;
    public bool ISCrossPlateFormInput;
    void Start()
    {

        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ |
        RigidbodyConstraints.FreezePositionX;

    }
    public void ChildMovement_InputCrossPlateForm()
    {


        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        //	float h = Inp;

        Child.transform.localPosition = new Vector3(Child.transform.localPosition.x + h * 100 * Time.deltaTime,
                                                 Child.transform.localPosition.y + v * 100 * Time.deltaTime, Child.transform.localPosition.z);

        Vector3 pos = Child.transform.localPosition;
        pos.y = Mathf.Clamp(pos.y, -maxClampX, maxClampX);
        pos.x = Mathf.Clamp(pos.x, -maxClampY, maxClampY);
        Child.transform.localPosition = pos;


        if (pos.x >= maxClampX)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + 1 * 25 * Time.deltaTime,
                                             transform.localPosition.y, transform.localPosition.z);
        }
        else if (pos.x <= -maxClampX)
        {
            transform.localPosition = new Vector3(transform.localPosition.x - 1 * 25 * Time.deltaTime,
                                   transform.localPosition.y, transform.localPosition.z);
        }

        if (pos.y >= maxClampY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x,
                                             transform.localPosition.y + 1 * 25 * Time.deltaTime, transform.localPosition.z);
        }
        else if (pos.y <= -maxClampY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x,
                                             transform.localPosition.y - 1 * 25 * Time.deltaTime, transform.localPosition.z);
        }


        Vector3 IniRot = Child.transform.localEulerAngles;
        Quaternion NextRot = Quaternion.Euler(IniRot);
        if (h < 0)
        {

            Quaternion Rot_R = Quaternion.Euler(new Vector3(0, 0, 30f));
            NextRot = Quaternion.Lerp(NextRot, Rot_R, Time.deltaTime * Mathf.Abs(h * ChildHSpeed));
        }
        else if (h > 0)
        {


            Quaternion Rot_L = Quaternion.Euler(new Vector3(0, 0, -30f));
            NextRot = Quaternion.Lerp(NextRot, Rot_L, Time.deltaTime * Mathf.Abs(h * ChildHSpeed));
        }
        /*else
        {


            Quaternion Rot_Idle = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            NextRot = Quaternion.Lerp(NextRot, Rot_Idle, Time.deltaTime * Mathf.Abs(ChildHSpeed / 10));
        }*/

        Child.transform.localRotation = NextRot;


    }
    public void ChildMovement_InputMouse()
    {
        if (Input.GetMouseButton(0))
        {
            //Vector3 currentMousePos = Input.mousePosition;
            //float h = InputController2.Dir.x;
            //float h = Input.mousePosition.x;

            float h = CrossPlatformInputManager.GetAxis("Horizontal");

            //          Vector3 pos = Child.transform.localPosition - transform.right * ChildHSpeed * h * Time.deltaTime;
            //pos.x = Mathf.Clamp(pos.x, -1.5f, 1.5f);
            //	Child.transform.localPosition = pos;




            Child.transform.localPosition = new Vector3(Child.transform.localPosition.x + h * 100 * Time.deltaTime,
                                                 Child.transform.localPosition.y, Child.transform.localPosition.z);

            Vector3 pos = Child.transform.localPosition;
            pos.x = Mathf.Clamp(pos.x, -1.4f, 1.4f);
            Child.transform.localPosition = pos;
            Vector3 IniRot = Child.transform.localEulerAngles;
            Quaternion NextRot = Quaternion.Euler(IniRot);
            if (h < 0)
            {

                Quaternion Rot_R = Quaternion.Euler(new Vector3(0, 0, -30f));
                NextRot = Quaternion.Lerp(NextRot, Rot_R, Time.deltaTime * Mathf.Abs(h * ChildHSpeed));
            }
            else if (h > 0)
            {


                Quaternion Rot_L = Quaternion.Euler(new Vector3(0, 0, 30f));
                NextRot = Quaternion.Lerp(NextRot, Rot_L, Time.deltaTime * Mathf.Abs(h * ChildHSpeed));
            }
            else
            {


                Quaternion Rot_Idle = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                NextRot = Quaternion.Lerp(NextRot, Rot_Idle, Time.deltaTime * Mathf.Abs(ChildHSpeed / 10));
            }

            Child.transform.localRotation = NextRot;
        }

    }

    private void FixedUpdate()
    {
        if (ISCrossPlateFormInput)
        {
            ChildMovement_InputCrossPlateForm();
        }
        if (ISCrossPlateFormInput == false)
        {
            ChildMovement_InputMouse();
        }
    }

    public void Update()
    {
        if (Manager.gameStarted)
        {
            transform.Translate(transform.forward*Time.deltaTime*forwardSpeed);
        }
    }





}

