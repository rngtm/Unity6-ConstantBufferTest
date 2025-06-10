using UnityEngine;

public class GraphicsControllerBehaviour : MonoBehaviour
{
    private GraphicsController _graphicsController;

    private void Start()
    {
        _graphicsController = new GraphicsController();
    }

    private void Update()
    {
        _graphicsController.Execute();
    }
}

public class ShaderProperties
{
    public const int N = 500;
    public readonly int[] PropertyIds; 
    public ShaderProperties()
    {
        PropertyIds = new int[N];
        for (int i = 0; i < N; i++)
        {
            PropertyIds[i] = Shader.PropertyToID($"_Float{i}");
        }
    }
}
