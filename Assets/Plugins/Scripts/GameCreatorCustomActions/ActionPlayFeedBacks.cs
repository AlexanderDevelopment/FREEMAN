using GameCreator.Core;
using MoreMountains.Feedbacks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace Plugins.Scripts.GameCreatorCustomActions
{
    [AddComponentMenu("")]
    public class ActionPlayFeedBacks : IAction
    {
        [SerializeField] 
        private MMF_Player m_MMF_Player;

        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
            m_MMF_Player.PlayFeedbacks();
            return true;
        }

        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR

        public static new string NAME = "Feedbacks/Play feedback";
        private const string NODE_TITLE = "Play feedback {0}";

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
        {
            return string.Format(
                NODE_TITLE,
                this.m_MMF_Player == null ? "(none)" : this.m_MMF_Player.name
            );
        }

#endif
    }
}