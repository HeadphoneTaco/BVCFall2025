using CoreUtils.GameVariables;
using UnityEngine;

namespace CoreUtils {
    public class OnGameVariableSetActive : MonoBehaviour {
        [SerializeField, AutoFillAsset] private GameVariableBool m_ToggleVariable;
        [SerializeField, AutoFill] private GameObject m_Target;

        private void Awake() {
            if (m_Target == null) {
                m_Target = gameObject;
            }

            if (m_ToggleVariable != null) {
                m_ToggleVariable.Changed += OnChange;
                OnChange(m_ToggleVariable.Value);
            }
        }

        private void OnDestroy() {
            if (m_ToggleVariable != null) {
                m_ToggleVariable.Changed -= OnChange;
            }
        }

        private void OnChange(bool value) {
            m_Target.SetActive(value);
        }
    }
}