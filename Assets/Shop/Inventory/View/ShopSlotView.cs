using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countView;
    [SerializeField] private TextMeshProUGUI _nameItem;
    [SerializeField] private TextMeshProUGUI _priceItem;
    [SerializeField] private Image _itemImage;
    [SerializeField] private Transform _itemVisual;
    [SerializeField] private Transform _itemModel;
    private IModelObtainableItem _itemSlotComp;
    private void Awake()
    {
        _itemSlotComp = _itemModel.gameObject.GetComponent<IModelObtainableItem>();
        _itemSlotComp.updateSlot += ModelUpdate;
    }
    
    private void ModelUpdate()
    {
        if(_itemSlotComp.Item == null)
        {
            _itemImage.sprite = null;
            _itemImage.color = new Color(0f, 0f, 0f, 0f);
            _nameItem.text = "";
            _priceItem.text = "";
            _itemVisual.gameObject.SetActive(false);
            return;
        }
        _itemVisual.gameObject.SetActive(true);
        _itemImage.color = new Color(1f, 1f, 1f, 1f);
        _itemImage.sprite = _itemSlotComp.Item.Sprite;
        _nameItem.text = _itemSlotComp.Item.Name;
        _priceItem.text = (_itemSlotComp.Item.Price * _itemSlotComp.Count).ToString();
        _countView.text = _itemSlotComp.Count > 1 ? _itemSlotComp.Count.ToString() : _itemSlotComp.Count.ToString();

    }
    
    
    private void OnDestroy()
    {
        _itemSlotComp.updateSlot -= ModelUpdate;
    }
}
