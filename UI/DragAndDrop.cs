
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    public Canvas canvas;

    public Vector2 baslangicPosition;

    public bool dogruYereBirakildimi = false;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = canvas.GetComponent<Canvas>();

    }
    void Start()
    {
        baslangicPosition = rectTransform.anchoredPosition;
    }

    //sürükelenye başladı
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(dogruYereBirakildimi) return;

        canvasGroup.alpha = 0.8f; // nesneyi görünürlüğünü azaltıyoruz.
        canvasGroup.blocksRaycasts = false; // nesnenin tıklanabilriliğini kapat.
    }

    //Sürükleniyor
    public void OnDrag(PointerEventData eventData)
    {
        if(dogruYereBirakildimi) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    //Sürükleme bitti
    public void OnEndDrag(PointerEventData eventData)
    {
     
       canvasGroup.alpha=1f;
       canvasGroup.blocksRaycasts = true;

        if (!dogruYereBirakildimi)
        {
            rectTransform.DOAnchorPos(baslangicPosition,0.3f).SetEase(Ease.OutBack);
        }
    }
}
