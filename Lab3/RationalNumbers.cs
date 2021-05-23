using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class RationalNumbers
    {
        private BigInteger numerator;              // Числитель
        private BigInteger denominator;            // Знаменатель
        private BigInteger sign;				   // Знак
        private string periodicView;               // Дробь в периодическом виде 

        public RationalNumbers(BigInteger numerator, BigInteger denominator) //Конструктор
        {
            if (denominator == 0)            
                throw new DivideByZeroException("В знаменателе не может быть нуля");
            
            this.numerator = BigInteger.Abs(numerator);
            this.denominator = BigInteger.Abs(denominator);

            if (numerator * denominator < 0)           
                this.sign = -1;     
            else            
                this.sign = 1;     
        }

        public RationalNumbers(BigInteger numerator) //Конструктор
        {
            this.numerator = BigInteger.Abs(numerator);
            this.denominator = 1;

            if (numerator < 0)
                this.sign = -1;
            else
                this.sign = 1;
        }

        public static BigInteger GetGreatestCommonDivisor(BigInteger a, BigInteger b) //Алгоритм Евклида - нахождение НОД
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        private static BigInteger GetLeastCommonMultiple(BigInteger a, BigInteger b) // Возвращает НОК
        {           
            return a * b / GetGreatestCommonDivisor(a, b);
        }
      
        private RationalNumbers GetReverse()// Возвращает дробь, обратную данной
        {
            return new RationalNumbers(this.denominator * this.sign, this.numerator);
        }
        
        private RationalNumbers GetWithChangedSign()// Возвращает дробь с противоположным знаком
        {
            return new RationalNumbers(-this.numerator * this.sign, this.denominator);
        }


        // Метод для устранения повторяющегося кода в методах сложения и вычитания дробей.
        private static RationalNumbers PerformOperation(RationalNumbers a, RationalNumbers b, Func<BigInteger, BigInteger, BigInteger> operation)
        {
            // Наименьшее общее кратное знаменателей
            var leastCommonMultiple = GetLeastCommonMultiple(a.denominator, b.denominator);
            // Дополнительный множитель к первой дроби
            var additionalMultiplierFirst = leastCommonMultiple / a.denominator;
            // Дополнительный множитель ко второй дроби
            var additionalMultiplierSecond = leastCommonMultiple / b.denominator;
            // Результат операции
            var operationResult = operation(a.numerator * additionalMultiplierFirst * a.sign,
            b.numerator * additionalMultiplierSecond * b.sign);
            return new RationalNumbers(operationResult, a.denominator * additionalMultiplierFirst);
        }
 
        public static RationalNumbers operator +(RationalNumbers a, RationalNumbers b) //Перегрузка оператора "+" для случая сложения двух дробей
        {
            return PerformOperation(a, b, (BigInteger x, BigInteger y) => x + y);
        }
    
        public static RationalNumbers operator +(RationalNumbers a, BigInteger b) //Перегрузка оператора "+" для случая сложения дроби с числом
        {
            return a + new RationalNumbers(b);
        }
      
        public static RationalNumbers operator +(BigInteger a, RationalNumbers b) //Перегрузка оператора "+" для случая сложения числа с дробью
        {
            return b + a;
        }
        
        public static RationalNumbers operator -(RationalNumbers a, RationalNumbers b) // Перегрузка оператора "-" для случая вычитания двух дробей
        {
            return PerformOperation(a, b, (BigInteger x, BigInteger y) => x - y);
        }
        
        public static RationalNumbers operator -(RationalNumbers a, BigInteger b) // Перегрузка оператора "-" для случая вычитания из дроби числа
        {
            return a - new RationalNumbers(b);
        }
        
        public static RationalNumbers operator -(BigInteger a, RationalNumbers b) // Перегрузка оператора "-" для случая вычитания из числа дроби
        {
            return b - a;
        }
        
        public static RationalNumbers operator *(RationalNumbers a, RationalNumbers b) // Перегрузка оператора "*" для случая произведения двух дробей
        {
            return new RationalNumbers(a.numerator * a.sign * b.numerator * b.sign, a.denominator * b.denominator);
        }
        
        public static RationalNumbers operator *(RationalNumbers a, BigInteger b) // Перегрузка оператора "*" для случая произведения дроби и числа
        {
            return a * new RationalNumbers(b);
        }
        
        public static RationalNumbers operator *(BigInteger a, RationalNumbers b) // Перегрузка оператора "*" для случая произведения числа и дроби
        {
            return b * a;
        }
        
        public static RationalNumbers operator /(RationalNumbers a, RationalNumbers b) // Перегрузка оператора "/" для случая деления двух дробей
        {
            return a * b.GetReverse();
        }
        
        public static RationalNumbers operator /(RationalNumbers a, BigInteger b) // Перегрузка оператора "/" для случая деления дроби на число
        {
            return a / new RationalNumbers(b);
        }
        
        public static RationalNumbers operator /(BigInteger a, RationalNumbers b) // Перегрузка оператора "/" для случая деления числа на дробь
        {
            return new RationalNumbers(a) / b;
        }
        
        public static RationalNumbers operator -(RationalNumbers a) // Перегрузка оператора "унарный минус"
        {
            return a.GetWithChangedSign();
        }
        
        public static RationalNumbers operator ++(RationalNumbers a) // Перегрузка оператора "++"
        {
            return a + 1;
        }
        
        public static RationalNumbers operator --(RationalNumbers a) // Перегрузка оператора "--"
        {
            return a - 1;
        }
   
        public override string ToString()// Переопределение метода ToString
        {
            if (this.numerator == 0)
            {
                return "0";
            }
            string result;
            if (this.sign < 0)
            {
                result = "-";
            }
            else
            {
                result = "";
            }
            if (this.numerator == this.denominator)
            {
                return result + "1";
            }
            if (this.denominator == 1)
            {
                return result + this.numerator;
            }
            return result + this.numerator + "/" + this.denominator;
        }

        public string GetPeriod() //Преобразования обыкновенной дроби в периодическую
        {
            StringBuilder result = new StringBuilder();

            var num = numerator;
            var dem = denominator;

            var list = new List<BigInteger>();

            if (sign == -1)
                result.Append("-");
            result.Append(num / dem + ".");
            var ost = num % dem;

            while (true)
            {
                num = ost * 10;
                ost = num % dem;
                result.Append(num / dem);

                if (0 == ost) 
                    break;

                if (list.Contains(ost))
                {
                    if(!(dem % 2 == 0 || dem % 5 == 0)) 
                        result.Remove(result.Length - 1, 1);

                    ost = list.Count() - list.IndexOf(ost);
                    break;
                }

                else
                    list.Add(ost);
            }

            if (ost != 0)
            {
                result.Insert(result.Length - (int)ost, "(");
                result.Append(")");
            }

            periodicView = result.ToString();
            return periodicView;
        }

        public static string GetRational(string str) //Преобразования периодической дроби в обыкновенную
        {
            BigInteger num;
            BigInteger dem;
            
            var a = str.IndexOf("(");
            int countNumbInPeriod = str.IndexOf(")") - a - 1;

            if (str.IndexOf(".") + 1 == str.IndexOf("("))
            {
                num = new BigInteger(Int64.Parse(str.Substring(a + 1, countNumbInPeriod)));
                dem = new BigInteger(Int64.Parse(new String('9', countNumbInPeriod)));
            }

            else 
            {
                var b = str.Substring(str.IndexOf(".") + 1);
                var d = b.Substring(0, b.IndexOf("("));
                var c = b.Remove(b.IndexOf("("), 1).Remove(b.IndexOf(")") - 1);

                num = new BigInteger(Int64.Parse(c)) - new BigInteger(Int64.Parse(d));
                dem = new BigInteger(Int64.Parse(new String('9', countNumbInPeriod) + new String('0', d.Length)));
            }

            var  wholePart = new BigInteger(Int64.Parse(str.Substring(0, str.IndexOf("."))));

            var GCD = GetGreatestCommonDivisor(num, dem);
            var res = (new RationalNumbers(num / GCD, dem / GCD) + wholePart).ToString();
            return res;
        }

        public string GetRational()
        {
            return GetRational(periodicView);
        }
    }
}
