using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [Header("PickupSettings")]
    [SerializeField] Transform _holdArea;
    [SerializeField] Transform _raycastStartingPoint;
    [SerializeField] private float _pickupRange = 1.5f;
    private GameObject _heldObject;
    private Rigidbody _heldObjRB;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_heldObject == null )
            {
                RaycastHit hit;
                if (Physics.Raycast(_raycastStartingPoint.transform.position, transform.TransformDirection(Vector3.forward), out hit, _pickupRange))
                {
                    if(hit.collider.CompareTag("PuzzlePiece"))
                        PickUpObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }

        if (_heldObject != null)
        {
            MoveObject();
        }
    }

    private void PickUpObject(GameObject pickedObject)
    {
        if (pickedObject.GetComponent<Rigidbody>())
        {
            _heldObjRB = pickedObject.GetComponent<Rigidbody>();
            _heldObjRB.useGravity = false;
            _heldObjRB.drag = 0;
            _heldObjRB.constraints = RigidbodyConstraints.FreezeAll;

            _heldObjRB.transform.parent = _holdArea;
            _heldObject = pickedObject;
            _heldObject.transform.position = _holdArea.position;
            _heldObject.GetComponent<Collider>().enabled = false;

            _heldObject.transform.rotation = transform.rotation;

            _heldObject.GetComponent<AudioSource>().Play();
        }
    }

    private void DropObject()
    {
        _heldObjRB.useGravity = true;
        _heldObjRB.drag = 0;
        _heldObjRB.constraints = RigidbodyConstraints.None;

        _heldObject.GetComponent<Collider>().enabled = true;    
        _heldObjRB.transform.parent = null;
        _heldObject = null;
    }

    private void MoveObject()
    {
        if (Vector3.Distance(_heldObject.transform.position, _holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (_holdArea.position - _heldObject.transform.position);
        }   
    }
}
