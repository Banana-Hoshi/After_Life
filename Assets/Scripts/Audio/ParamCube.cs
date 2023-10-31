using UnityEngine;

public class ParamCube : MonoBehaviour
{
    [HideInInspector] public int _band;
    [SerializeField] private float _startScale = 1f;
    [SerializeField] private float _scaleMultipler = 10f;

    private float startPos;
    private float previousPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.y;
        previousPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float resizeAmount = FMODAudioVisualizer.bandBuffer[_band] * _scaleMultipler;

        transform.position = new Vector3(960f, startPos, 0f);
        transform.localScale = new Vector3(transform.localScale.x, resizeAmount + _startScale, transform.localScale.z);
        transform.position = new Vector3(960f, transform.position.y + resizeAmount / 2 - 150f, 0f);
    }
}