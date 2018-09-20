using System;

namespace MarkSFrancis.Console.Progress
{
    /// <summary>
    /// Provides a way of writing the progress of an operation to the console
    /// </summary>
    public class ProgressBarRenderer
    {
        /// <summary>
        /// The maximum value of progress possible
        /// </summary>
        public decimal MaxValue { get; }

        private const int BlockCount = 10;

        private static readonly char[] Frames = {

            '|',
            '/',
            '-',
            '\\'
        };

        /// <summary>
        /// Create a new <see cref="ProgressBarRenderer"/>
        /// </summary>
        /// <param name="maxValue">The maximum value, representing the progress bar at its completed state</param>
        /// <exception cref="ArgumentLessThanZeroException"><paramref name="maxValue"/> is less than zero</exception>
        public ProgressBarRenderer(decimal maxValue)
        {
            if (maxValue < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(maxValue));
            }

            MaxValue = maxValue;
            _currentAnimationFrame = 0;
        }

        private int _currentAnimationFrame;

        private decimal _progress;

        /// <summary>
        /// Get or set the current progress
        /// </summary>
        /// <exception cref="ArgumentLessThanZeroException">The value was set to a value less than zero</exception>
        /// <exception cref="ArgumentOutOfRangeException">The value was set to a value greater than <see cref="MaxValue"/></exception>
        public decimal Progress
        {
            get => _progress;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentLessThanZeroException(nameof(value));
                }

                if (value > MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _progress = value;
            }
        }

        /// <summary>
        /// Get the completed state
        /// </summary>
        public bool IsComplete => Progress == MaxValue;

        /// <summary>
        /// Get whether the progress is faulted
        /// </summary>
        public bool IsFaulted { get; set; }

        private void UpdateAnimationChar()
        {
            if (++_currentAnimationFrame >= Frames.Length)
            {
                _currentAnimationFrame = 0;
            }
        }

        /// <summary>
        /// Return this <see cref="ProgressBarRenderer"/> formatted as a progress bar, with the spinner
        /// </summary>
        /// <returns>This <see cref="ProgressBarRenderer"/> formatted as a progress bar</returns>
        public override string ToString()
        {
            return ToString(false, false);
        }

        /// <summary>
        /// Return this <see cref="ProgressBarRenderer"/> formatted as a progress bar
        /// </summary>
        /// <param name="moveSpinnerToNextPosition">Whether to rotate the spinner by 1 position</param>
        /// <returns>This <see cref="ProgressBarRenderer"/> formatted as a progress bar</returns>
        public string ToStringWithSpinner(bool moveSpinnerToNextPosition)
        {
            return ToString(true, moveSpinnerToNextPosition);
        }

        private string ToString(bool showSpinner, bool moveSpinnerToNextPosition)
        {
            if (moveSpinnerToNextPosition)
            {
                UpdateAnimationChar();
            }

            var percProgress = (Progress / MaxValue) * 100;
            var blocksToPrint = (int)Math.Truncate(percProgress / BlockCount);

            var progressDoneIndicator = new string('#', blocksToPrint);
            var progressToGoIndicator = new string('-', BlockCount - blocksToPrint);

            string renderedBar = $"[{progressDoneIndicator}{progressToGoIndicator}] {percProgress.ToString("0.##")}%";

            if (showSpinner)
            {
                var frame = Frames[_currentAnimationFrame];
                renderedBar += $" {frame}";
            }

            return renderedBar;
        }
    }
}
