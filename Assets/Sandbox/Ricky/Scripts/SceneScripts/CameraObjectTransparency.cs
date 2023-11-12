using UnityEngine;

public class CameraObjectTransparency : MonoBehaviour
{
    private Transform player;
    private Transform cameraTransform;

    [SerializeField] private float transparencyThreshold = 5.0f;
    [SerializeField] private LayerMask objectLayerMask = ~0; // 透過するオブジェクトのレイヤーマスク

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Box").transform;
        cameraTransform = transform;
    }

    private void Update()
    {
        Vector3 playerPosition = player.position;
        Vector3 cameraPosition = cameraTransform.position;

        // カメラとプレイヤー間の方向ベクトル
        Vector3 direction = playerPosition - cameraPosition;

        // カメラからプレイヤーへのレイを発射して、透過対象のオブジェクトを検出
        if (Physics.Raycast(cameraPosition, direction, out RaycastHit hit, direction.magnitude, objectLayerMask))
        {
            Renderer objectRenderer = hit.collider.GetComponent<Renderer>();

            if (objectRenderer != null)
            {
                Material objectMaterial = objectRenderer.material;

                // 透明度を制御
                float distanceToPlayer = hit.distance;
                float newTransparency = Mathf.Lerp(1.0f, 0.0f, Mathf.InverseLerp(0.0f, transparencyThreshold, distanceToPlayer));
                objectMaterial.SetFloat("_Transparency", newTransparency);
            }
        }
    }
}