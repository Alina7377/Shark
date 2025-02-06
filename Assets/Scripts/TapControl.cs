using UnityEngine;

public class TapControl : MonoBehaviour
{
    [SerializeField] private Character _character; 

    private void Update()
    {
        if (Input.touchCount > 0)
            if (Input.touches[0].phase == TouchPhase.Began)
                _character.NewTarget(Input.GetTouch(0).position);
    }

}
