using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd3
{
    public class EnhancedRoadWork : RoadWork
    {
        public int StrengthCoefficient { get; set; }
        public string Contractor { get; set; }
        public bool IsCompleted { get; set; }

        public EnhancedRoadWork(double width, double length, double weight, string workName,
                              DateTime startDate, int strengthCoefficient, string contractor,
                              bool isCompleted)
            : base(width, length, weight, workName, startDate)
        {
            StrengthCoefficient = strengthCoefficient;
            Contractor = contractor;
            IsCompleted = isCompleted;
        }

        public override double CalculateQuality()
        {
            double baseQ = base.CalculateQuality();

            if (StrengthCoefficient >= 5 && StrengthCoefficient <= 8)
            {
                return baseQ * 1.1;
            }
            else if (new[] { 3, 4, 9, 10 }.Contains(StrengthCoefficient))
            {
                return baseQ * 1.6;
            }
            else
            {
                return baseQ * 1.9;
            }
        }

        public override string ToString()
        {
            return base.ToString() + $" [Улучшенная: P={StrengthCoefficient}, Qp={CalculateQuality():F2}]";
        }
    }
}
