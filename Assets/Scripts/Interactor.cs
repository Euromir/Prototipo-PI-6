using UnityEngine;

public class Interactor : MonoBehaviour
{
    public float Radius = 1f;

    void Update()
    {
        Interaction();
    }

    public void SetRadius(float newRadius)
    {
        Radius = newRadius;
    }

    public void Interaction()
    {
        Shader.SetGlobalVector("_Position", transform.position);
        Shader.SetGlobalFloat("_Radius", Radius);
    }
}
