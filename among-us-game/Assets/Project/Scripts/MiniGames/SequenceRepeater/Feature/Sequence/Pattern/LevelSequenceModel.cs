namespace MiniGames.SequenceRepeater
{
    public sealed class LevelSequenceModel
    {
        private int[] _sequence;
        private int _currentStep;

        public int[] GenerateSequence(int tiles, int length)
        {
            _sequence = new int[length];
            for (var i = 0; i < length; i++)
            {
                var next = UnityEngine.Random.Range(0, tiles);
                _sequence[i] = next;
            }
            return _sequence;
        }

        public int[] GetSequence() => _sequence;

        public void Increase() => _currentStep++;

        public bool IsLastStep() => _currentStep == _sequence.Length - 1;

        public bool ValidateStep(int id)
        {
            if (_sequence.Length == 0)
            {
                throw new System.Exception("Sequence is empty");
            }

            var expected = _sequence[_currentStep];
            var correct = expected == id;

            return correct;
        }

        public void Reset()
        {
            _sequence = null;
            _currentStep = 0;
        }
    }
}