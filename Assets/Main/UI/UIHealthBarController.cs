using UnityEngine;

public class UIHealthBarController : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private RectTransform barTransform;

    [SerializeField] private float lerpSpeed = 10f;

    private float fullHealthScaleX;

    private void Start()
    {
        fullHealthScaleX = barTransform.localScale.x;
    }

    private void Update()
    {
        float healthPercent = (float)playerHealth.GetCurrentHealth() / playerHealth.GetMaxHealth();
        float targetScaleX = fullHealthScaleX * healthPercent;

        Vector3 currentScale = barTransform.localScale;
        currentScale.x = Mathf.Lerp(currentScale.x, targetScaleX, Time.deltaTime * lerpSpeed);
        barTransform.localScale = currentScale;
    }
}
