using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlotView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countView;
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
            _itemVisual.gameObject.SetActive(false);
            return;
        }

        if (_itemSlotComp.Count > 1)
        {
            _itemVisual.gameObject.SetActive(true);
            _countView.text = _itemSlotComp.Count.ToString();
        }

        _itemImage.color = new Color(1f, 1f, 1f, 1f);
        _itemImage.sprite = _itemSlotComp.Item.Sprite;
        
        
        
    }
    
    
    private void OnDestroy()
    {
        _itemSlotComp.updateSlot -= ModelUpdate;
    }
}
