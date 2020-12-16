using UnityEngine;

public class CheatForTest : MonoBehaviour
{
    public Resource.ResourceObject resource;

    public void Cheat()
    {
        resource.CurrentAmount += 10000000000ul;
    }
}
