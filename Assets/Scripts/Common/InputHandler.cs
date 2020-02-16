using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    private InputField field;

    [SerializeField]
    private UnityEvent OnReturnHanlder;

    // Start is called before the first frame update
    void Start()
    {
        field = GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!field.isFocused) return;

        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (OnReturnHanlder != null) OnReturnHanlder.Invoke();
        }
    }
}
