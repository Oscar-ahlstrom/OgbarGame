using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{

    [SerializeField] public Sprite defaultSprite;
    [SerializeField] public Sprite holdSprite;

    private bool cursorVisible;
    private Image imageRenderer;
    InputAction mousePos;
    InputAction mouseButton;

    private void Start()
    {
        Cursor.visible = cursorVisible;
        mousePos = InputSystem.actions.FindAction("Point");
        mouseButton = InputSystem.actions.FindAction("Attack");
        imageRenderer = GetComponent<Image>();

        imageRenderer.sprite = defaultSprite;
        transform.localScale = new Vector3(defaultSprite.texture.width / defaultSprite.pixelsPerUnit, defaultSprite.texture.height / defaultSprite.pixelsPerUnit, 1);
    }
    void Update()
    {
        transform.position = mousePos.ReadValue<Vector2>();

        if (mouseButton.WasPressedThisFrame())
        {
            imageRenderer.sprite = holdSprite;
            transform.localScale = new Vector3(holdSprite.texture.width/holdSprite.pixelsPerUnit, holdSprite.texture.height / holdSprite.pixelsPerUnit, 1);
        }

        if (mouseButton.WasReleasedThisFrame())
        {
            imageRenderer.sprite = defaultSprite;
            transform.localScale = new Vector3(defaultSprite.texture.width / defaultSprite.pixelsPerUnit, defaultSprite.texture.height / defaultSprite.pixelsPerUnit, 1);
        }
    }


}
