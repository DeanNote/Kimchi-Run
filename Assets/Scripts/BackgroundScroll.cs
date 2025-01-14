using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("How fast should the texture scroll?")]
    public float ScrollSpeed;

    [Header("References")]
    public MeshRenderer meshranderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        meshranderer.material.mainTextureOffset += new Vector2(ScrollSpeed * 
        GameManager.Instatnce.CalculateGameSpeed() / 20 * Time.deltaTime, 0);
    }
}
