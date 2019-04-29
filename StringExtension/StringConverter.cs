using System;
using System.Text;

namespace StringExtension
{
    public class StringConverter
    {
        public string Convert(string source, int count)
        {
            string result = source;

            count = count % CalculateMaxConverts(source.Length);

            for (int i = 0; i < count; i++)
            {
                result = Convert(result);
            }

            return result;
        }

        private string Convert(string source)
        {
            StringBuilder first = new StringBuilder(source.Length >> 1);
            StringBuilder second = new StringBuilder(source.Length >> 1);
            for (int i = 0; i < source.Length; i++)
            {
                first.Append(source[i++]);
                second.Append(source[i]);
            }

            return first.Append(second).ToString();
        }

        private int CalculateMaxConverts(int length)
        {
            if ((length & 1) == 0)
            {
                length--;
            }

            int result = 1;
            int mid = (length + 1) / 2;
            int position = length - 1;
            while (position != length)
            {
                position = position > mid ? (position - mid) * 2 : position * 2 - 1;
                result++;
            }

            return result;
        }
    }
}