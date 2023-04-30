using GameCreator.Core;
using GameCreator.Stats;
using MoreMountains.Tools;
using UnityEngine;

namespace Plugins.Scripts
{
    public class AttrProgressBar : MonoBehaviour
    {
        [SerializeField]
        private TargetGameObject _target = new(TargetGameObject.Target.Player);


        [SerializeField]
        [AttributeSelector]
        private AttrAsset _attribute;


        [SerializeField]
        private MMProgressBar _progressBar;


        private Stats _stats;


        private void Start()
        {
            if (_attribute == null)
            {
                Debug.LogError($"[{nameof(AttrProgressBar)}] AttrAsset is null");

                return;
            }

            _stats = _target.GetComponent<Stats>(gameObject);

            if (_stats)
            {
                _stats.AddOnChangeAttr(UpdateProgressBar);
                _progressBar.SetBar01(GetAttrPercent());
            }
            else
            {
                Debug.LogError($"[{nameof(AttrProgressBar)}] Stats is null");
            }
        }


        private void OnDestroy()
        {
            if (_stats)
                _stats.RemoveOnChangeAttr(UpdateProgressBar);
        }


        private void UpdateProgressBar(Stats.EventArgs args)
        {
            _progressBar.UpdateBar01(GetAttrPercent());
        }


        private float GetAttrPercent() => _stats.GetAttrValuePercent(_attribute.attribute.uniqueName);
    }
}