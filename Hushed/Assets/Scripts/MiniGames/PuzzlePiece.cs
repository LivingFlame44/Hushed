using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler
{
    public JigsawPuzzleManager puzzleManager;
    public SpriteRenderer spriteRenderer;
    private RectTransform rectTransform;

    private Vector3 offset;
    private Camera mainCamera;

    public bool selected;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                transform.Rotate(0, 0, 90f);
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                transform.Rotate(0,0,-90f);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                spriteRenderer.sortingOrder = Mathf.Clamp(spriteRenderer.sortingOrder - 1, 0, 4);
                puzzleManager.DecreaseLayer(this);
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                spriteRenderer.sortingOrder = Mathf.Clamp(spriteRenderer.sortingOrder + 1, 0, 4);
                puzzleManager.IncreaseLayer(this);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.timeScale != 0)
        {
            puzzleManager.UnselectPuzzles();
            selected = true;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Calculate offset between pointer and object position
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(
            new Vector3(eventData.position.x, eventData.position.y, mainCamera.WorldToScreenPoint(transform.position).z));
        offset = transform.position - worldPoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Convert screen position to world position
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(
            new Vector3(eventData.position.x, eventData.position.y, mainCamera.WorldToScreenPoint(transform.position).z));

        // Apply position with offset
        transform.position = worldPoint + offset;
    }
}
