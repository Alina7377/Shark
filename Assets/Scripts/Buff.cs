using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField] private EBuffType _buffType;
    [SerializeField] private int _val;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IGettingBuff>(out IGettingBuff damageObject))
        {
            damageObject.GetBuff(_buffType, _val);
            Destroy(this.gameObject);
        }
    }
}
