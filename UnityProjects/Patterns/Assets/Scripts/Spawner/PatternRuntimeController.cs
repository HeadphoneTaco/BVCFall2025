using UnityEngine;

namespace Spawner
{
    public class PatternRuntimeController : MonoBehaviour {
        [Header("References")]
        public PatternSpawner spawner;   // drag your spawner here in the inspector
        public bool autoAnimateWeights = true;
        public float weightSpeed = 1f;
        public float weightAmplitude = 2f;
        public bool continuousRefresh = true;
        public float refreshInterval = 0.5f;

        private float _refreshTimer;

        void Update() {
            if (spawner == null || spawner.patternBucket == null) return;

            // 1️⃣ Animate weights over time
            if (autoAnimateWeights) {
                var pattern = spawner.patternBucket.Items[spawner.currentPatternIndex];
                if (pattern != null && pattern.variationSet != null) {
                    foreach (var v in pattern.variationSet.variations) {
                        if (v == null) continue;
                        v.weight = Mathf.Abs(Mathf.Sin(Time.time * weightSpeed)) * weightAmplitude;
                    }
                }
            }

            // 2️⃣ Periodically refresh spawner
            if (continuousRefresh) {
                _refreshTimer += Time.deltaTime;
                if (_refreshTimer >= refreshInterval) {
                    _refreshTimer = 0f;
                    spawner.Clear();
                    spawner.Spawn();
                }
            }

            // 3️⃣ Optional: manual refresh on key press
            if (Input.GetKeyDown(KeyCode.R)) {
                spawner.Clear();
                spawner.Spawn();
            }
        }
    }
}