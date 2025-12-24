using UnityEngine;
using System.Linq;

public class HandTrackingController : MonoBehaviour
{
    public RectTransform containerPanel;

    [Header("Hassas Ayarlar")]
    public float pinchThreshold = 25.0f;
    public float smoothSpeed = 5.0f;

    [Header("El Görselleri")]
    public GameObject acikElVisual;
    public GameObject kapaliElVisual;

    private Transform indexTip;
    private Transform thumbTip;
    private GameObject grabbedObject;
    private bool isTouching = false;
    private GameObject potentialTarget;

    void Start()
    {
        if (containerPanel != null)
            containerPanel.localScale = new Vector3(-1, 1, 1);

        UpdateHandVisuals(true);
    }

    void Update()
    {
        var allPoints = GameObject.FindObjectsOfType<GameObject>()
            .Where(obj => obj.name.Contains("Point Annotation"))
            .OrderBy(obj => obj.transform.GetSiblingIndex())
            .ToArray();

        if (allPoints.Length >= 21)
        {
            indexTip = allPoints[12].transform;
            thumbTip = allPoints[4].transform;
        }

        if (indexTip != null && thumbTip != null)
        {
            Vector3 targetPosition = new Vector3(indexTip.position.x, indexTip.position.y, 0f);

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);

            float distance = Vector3.Distance(indexTip.position, thumbTip.position);

            if (distance < pinchThreshold)
            {
                UpdateHandVisuals(false);

                if (isTouching && grabbedObject == null && potentialTarget != null)
                {
                    grabbedObject = potentialTarget;
                    AudioSource s = grabbedObject.GetComponentInChildren<AudioSource>();
                    if (s != null && !s.isPlaying) s.Play();
                    foreach (var r in grabbedObject.GetComponentsInChildren<Renderer>()) r.material.color = Color.green;
                }
            }
            else
            {
                UpdateHandVisuals(true);

                if (grabbedObject != null)
                {
                    foreach (var r in grabbedObject.GetComponentsInChildren<Renderer>()) r.material.color = Color.white;
                    grabbedObject = null;
                }
            }
        }

        if (grabbedObject != null)
        {
            grabbedObject.transform.position = transform.position;
        }
    }

    void UpdateHandVisuals(bool isOpen)
    {
        if (acikElVisual != null) acikElVisual.SetActive(isOpen);
        if (kapaliElVisual != null) kapaliElVisual.SetActive(!isOpen);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            isTouching = true;
            potentialTarget = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            isTouching = false;
            if (grabbedObject == null) potentialTarget = null;
        }
    }
}