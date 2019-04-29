using System;
using System.Text;

namespace StringExtension
{
    public class StringConverter
    {
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

            count = count % CalculateMaxConvertsCount(source.Length);
            StringBuilder sb = new StringBuilder(source);

            for (int i = 1; i < source.Length; i++)
            {
                sb[GetNewPosition(i, source.Length, count)] = source[i];
            }

            return sb.ToString();
        }

        private int CalculateMaxConvertsCount(int length)
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
		    int mid = ((length - 1) >> 1) + 1;
		    int position = oldPosition;

            for (int i = 0; i < count; i++)
		    {
			    position = (position & 1) != 0 ? mid + (position >> 1) : (position) >> 1;
			
		    }

		    return position;
	    }
    }
}