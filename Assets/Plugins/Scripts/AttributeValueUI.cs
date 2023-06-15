using System;
using GameCreator.Characters;
using GameCreator.Core;
using GameCreator.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Scripts
{
    public class AttributeValueUI : MonoBehaviour
    {
        public TargetCharacter character = new TargetCharacter();

        [AttributeSelector] public AttrAsset Attribute;

        public Image imageFill;

        private Stats stats = new();

        private void Start()
        {
            Character _character = this.character.GetCharacter(gameObject);
            _character.TryGetComponent(out stats);
            if (!stats)
                Debug.LogError($"No have stats in {character.character.gameObject}");
            stats.AddOnChangeAttr(_ =>UpdateSliderValue() );
        }

        private void Update()
        {
        }

        private void SliderInit()
        {
           imageFill.fillAmount = stats.GetAttrValuePercent(Attribute.attribute.uniqueName);
        }

        private void UpdateSliderValue()
        {
          imageFill.fillAmount = stats.GetAttrValuePercent(Attribute.attribute.uniqueName);
        }
    }
}