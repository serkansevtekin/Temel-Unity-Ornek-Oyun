using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;


public class itemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {

        //eventData.pointerDrag -> bırakılan objenin biligisi
        GameObject birakilanNesne = eventData.pointerDrag;
        if (birakilanNesne != null)
        {
            DragAndDrop dragAndDrop = birakilanNesne.GetComponent<DragAndDrop>();

            if (dragAndDrop.name == this.gameObject.transform.GetChild(0).name)
            {
                //doğru nesne bırakıldı
                dragAndDrop.GetComponent<RectTransform>().DOAnchorPos(GetComponent<RectTransform>().anchoredPosition, 0.1f);

                dragAndDrop.dogruYereBirakildimi = true;
                dragAndDrop.GetComponent<CanvasGroup>().blocksRaycasts = false;
                UIManager.UIinstance.OnCorrectMath();
            }
            else
            {
                //doğru nesne bırakılmadı
                dragAndDrop.dogruYereBirakildimi = false;
                dragAndDrop.GetComponent<RectTransform>().DOAnchorPos(dragAndDrop.baslangicPosition, 0.3f).SetEase(Ease.OutBack);
            }


        }

    }
}
