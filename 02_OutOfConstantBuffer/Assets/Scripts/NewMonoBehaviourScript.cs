using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private Color _color = new Color(1, 1, 1, 1);

    private Material _duplicatedMaterial;

    private void Start()
    {
        _duplicatedMaterial = GetComponent<Renderer>().material; // マテリアルが複製される
    }

    private void Update()
    {
        _duplicatedMaterial.SetColor("_Color", _color);
    }
}
