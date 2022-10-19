using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Unity.Template.VR
{
    public class SpiderWeb
    {
        [SerializeField] private Transform shooterTip;
        [SerializeField] private Rigidbody player;
        [SerializeField] private GameObject webEnd;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private ActionBasedController controller;

        
        [SerializeField] private float webStrenght = 8.5f;
        [SerializeField] private float webDamper = 7f;
        [SerializeField] private float webMassScale = 4.5f;
        [SerializeField] private float webZipStrenght = 5f;
        [SerializeField] private float maxDistance;
        [SerializeField] private LayerMask webLayers;

        private SpringJoint joint;
        private FixedJoint endJoint;
        private Vector3 webPoint;
        private float distanceFromPoint;
        private bool webShot;

        private void Awake()
        {
            //lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.GetComponent<LineRenderer>();
            webEnd.transform.parent = null;
        }

        private void Update()
        {
            HandleInput();
            if (webEnd && joint)
            {
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0,shooterTip.position);
                lineRenderer.SetPosition(1,webEnd.transform.position);
            }
        }

        private void HandleInput()
        {
            float isPressed = controller.activateAction.action.ReadValue<float>();
            Debug.Log("HERE SHOULD BE PRESSED OR NOT PRESSED TRIGGER BUTTON :_))_");

            if (isPressed > 0 && !webShot)
            {
                webShot = true;
                ShootWeb();
            }
            else if (isPressed == 0 & webShot)
            {
                webShot = false;
                StopWeb();
            }
            
        }

        private void ShootWeb()
        {
            RaycastHit hit;
            if (Physics.Raycast(shooterTip.position, shooterTip.forward, out hit, maxDistance, webLayers))
            {
                webPoint = hit.point;
                webEnd.transform.position = webPoint;
                joint = player.gameObject.AddComponent<SpringJoint>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = webPoint;

                if (hit.transform.gameObject.TryGetComponent<Rigidbody>(out Rigidbody ridgidBody))
                {
                    if (ridgidBody)
                    {
                        joint.connectedAnchor = new Vector3(0, 0, 0);
                        webEnd.GetComponent<Rigidbody>().isKinematic = false;
                        endJoint = webEnd.AddComponent<FixedJoint>();
                        endJoint.connectedBody = ridgidBody;

                        joint.connectedBody = webEnd.GetComponent<Rigidbody>();
                    }
                }

                if (!ridgidBody)
                {
                    webEnd.GetComponent<Rigidbody>().isKinematic = true;
                }

                distanceFromPoint = Vector3.Distance(player.transform.position, webPoint) * .75f;
                joint.minDistance = 0;
                joint.maxDistance = distanceFromPoint;

                joint.spring = webStrenght;
                joint.damper = webDamper;
                joint.massScale = webMassScale;
            }
        }

        private void StopWeb()
        {
            //Destroy(joint);
            //if (endJoint) Destroy(endJoint);

            lineRenderer.positionCount = 0;
        }
    }
}