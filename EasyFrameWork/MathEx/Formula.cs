/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.MathEx
{
    public class Formula
    {
        /// <summary>
        /// 输入算式，直接得到结果
        /// </summary>
        /// <param name="MatchStr">输入的算式</param>
        /// <returns></returns>
        public static decimal GetResult(string MatchStr)
        {
            int befor = 0; int count = 0;
            for (int i = 0; i < MatchStr.Length; i++)
            {
                if (MatchStr[i] == '(')
                    befor++;
                else if (MatchStr[i] == ')')
                    befor--;
                if (char.IsDigit(MatchStr[i]) || MatchStr[i] == '(' || MatchStr[i] == ')' || MatchStr[i] == '.' || MatchStr[i] == '+' || MatchStr[i] == '-' || MatchStr[i] == '*' || MatchStr[i] == '/')
                {
                    count++;
                }
            }
            if (befor != 0)
            {
                throw new Exception("算式的括号不匹配！");
            }
            if (count != MatchStr.Length)
            {
                throw new Exception("算式中含有非法字符！");
            }
            MatchStr = NoClub(MatchStr);
            return ResultSue(MatchStr);
        }
        private static string NoClub(string MatchStr)
        {
            int startIndex = 0;
            int endIndex = 0;
            for (int i = 0; i < MatchStr.Length; i++)
            {
                if (MatchStr[i] == '(')
                {
                    startIndex = i;
                }
                else if (MatchStr[i] == ')')
                {
                    endIndex = i;
                    break;
                }
            }
            if (startIndex != endIndex)
            {
                string tempStr = MatchStr.Substring(startIndex + 1, endIndex - startIndex - 1);
                decimal result = ResultSue(tempStr);
                MatchStr = MatchStr.Substring(0, startIndex) + result + MatchStr.Substring(endIndex + 1, MatchStr.Length - endIndex - 1);
            }
            if (MatchStr.IndexOf('(') != -1)
            {
                MatchStr = NoClub(MatchStr);
            }
            return MatchStr;
        }
        private static decimal ResultSue(string MatchStr)
        {
            decimal returnResult = 0;
            int indexM = MatchStr.IndexOf('*');
            int indexW = MatchStr.IndexOf('/');

            if ((indexM < indexW && indexM != -1) || (indexM != -1 && indexW == -1))
            {
                string partPre = MatchStr.Substring(0, indexM);
                string partNext = MatchStr.Substring(indexM + 1, MatchStr.Length - indexM - 1);
                string pre, Next;
                decimal prenum = PreNumber(partPre, out pre);
                decimal nextnum = NextNumber(partNext, out Next);
                returnResult = prenum * nextnum;
                MatchStr = pre + returnResult + Next;
            }
            else if ((indexW < indexM && indexW != -1) || (indexW != -1 && indexM == -1))
            {
                string partPre = MatchStr.Substring(0, indexW);
                string partNext = MatchStr.Substring(indexW + 1, MatchStr.Length - indexW - 1);
                string pre, Next;
                decimal prenum = PreNumber(partPre, out pre);
                decimal nextnum = NextNumber(partNext, out Next);
                returnResult = prenum / nextnum;
                MatchStr = pre + returnResult + Next;
            }
            if (indexM == -1 && indexW == -1)
            {
                int indexP = MatchStr.IndexOf('+');
                int indexQ = MatchStr.IndexOf('-');
                if ((indexP < indexQ && indexP != -1) || (indexP != -1 && indexQ == -1))
                {
                    string partPre = MatchStr.Substring(0, indexP);
                    string partNext = MatchStr.Substring(indexP + 1, MatchStr.Length - indexP - 1);
                    string pre, Next;
                    decimal prenum = PreNumber(partPre, out pre);
                    decimal nextnum = NextNumber(partNext, out Next);
                    returnResult = prenum + nextnum;
                    MatchStr = pre + returnResult + Next;
                }
                else if ((indexQ < indexP && indexQ != -1) || (indexQ != -1 && indexP == -1))
                {
                    string partPre = MatchStr.Substring(0, indexQ);
                    string partNext = MatchStr.Substring(indexQ + 1, MatchStr.Length - indexQ - 1);
                    string pre, Next;
                    decimal prenum = PreNumber(partPre, out pre);
                    decimal nextnum = NextNumber(partNext, out Next);
                    returnResult = prenum - nextnum;
                    MatchStr = pre + returnResult + Next;
                }
            }
            if (MatchStr.IndexOf('*') != -1 || MatchStr.IndexOf('/') != -1 || MatchStr.IndexOf('+') != -1 || MatchStr.IndexOf('-') != -1)
            {
                returnResult = ResultSue(MatchStr);
            }
            return returnResult;
        }
        private static decimal PreNumber(string str, out string PreStr)
        {
            Stack<Char> nums = new Stack<char>();
            PreStr = string.Empty;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (Char.IsDigit(str[i]) || str[i] == '.')
                {
                    nums.Push(str[i]);
                }
                else
                {
                    PreStr = str.Substring(0, i + 1);
                    break;
                }
            }
            string lastNum = string.Empty;
            while (nums.Count != 0)
            {
                lastNum += nums.Pop();
            }
            return decimal.Parse(lastNum);
        }
        private static decimal NextNumber(string str, out string NextStr)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (Char.IsDigit(str[i]) || str[i] == '.')
                {
                    continue;
                }
                else
                {
                    NextStr = str.Substring(i, str.Length - i);
                    return decimal.Parse(str.Substring(0, i));
                }
            }
            NextStr = string.Empty;
            return decimal.Parse(str);
        }
    }
}
