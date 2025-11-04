using JetBrains.Annotations;

namespace PatternGame
{
    [UsedImplicitly]
    public sealed class PatternModel : IPatternProgress, IPatternValidation
    {
        private int[] _pattern;
        private int _currentStep;

        public void GeneratePattern(int tiles, int length)
        {
            _pattern = new int[length];
            for (var i = 0; i < length; i++)
            {
                var next = UnityEngine.Random.Range(0, tiles);
                _pattern[i] = next;
            }
        }

        public int[] GetSequence() => _pattern;

        void IPatternProgress.Increase() => _currentStep++;
        bool IPatternProgress.IsLastStep() => _currentStep == _pattern.Length - 1;

        public void Reset()
        {
            _pattern = null;
            _currentStep = 0;
        }

        public bool ValidateStep(int id)
        {
            if (_pattern.Length == 0)
            {
                throw new System.Exception("Sequence is empty");
            }

            var expected = _pattern[_currentStep];
            var correct = expected == id;

            return correct;
        }
    }
}