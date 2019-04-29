using System;
using System.Text;

namespace StringExtension
{
    /// <summary>
    /// Provides the functionality for converting strings.
    /// </summary>
    public class StringConverter
    {
        /// <summary>
        /// Collects odd characters of the specified string and puts it in the begininig
        /// of the string, even characters stays a the ending. Repeats <paramref name="count"/>
        /// times.
        /// </summary>
        /// <param name="source">An source <see cref="string"/> to convert.</param>
        /// <param name="count">Number of repetitions.</param>
        /// <returns>Converted <see cref="string"/>.</returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentException">Throws if <paramref name="source"/> is <see cref="string.Empty"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">Throws if <paramref name="count"/> is not positive.</exception>
        public string Convert(string source, int count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source.Trim().Length < 1)
            {
                throw new ArgumentException("Argument is empty string.", nameof(source));
            }

            if (count < 1)
            {
                throw new ArgumentOutOfRangeException("Argument less than 1.", nameof(count));
            }

            if (source.Length < 3)
            {
                return source;
            }

            StringBuilder sb = new StringBuilder(source);

            count = count % CalculateMaxPossibleConvertationsCount(source.Length);

            for (int i = 1; i < source.Length; i++)
            {
                sb[GetNewPosition(i, source.Length, count)] = source[i];
            }

            return sb.ToString();
        }

        private int CalculateMaxPossibleConvertationsCount(int length)
        {
            if ((length & 1) == 0)
            {
                length--;
            }

            int result = 1;
            int mid = (length + 1) >> 1;
            int position = length - 1;

            while (position != length)
            {
                position = position > mid ? (position - mid) << 1 : (position << 1) - 1;
                result++;
            }

            return result;
        }

        private int GetNewPosition(int oldPosition, int length, int count)
        {
            int mid = (length + 1) >> 1;
            int newPosition = oldPosition;
            
            for (int i = 0; i < count; i++)
            {
                newPosition = (newPosition & 1) != 0 ? mid + (newPosition >> 1) : newPosition >> 1;
            
            }
            
            return newPosition;
        }
    }
}