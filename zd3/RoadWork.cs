using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd3
{
    public class RoadWork
    {
        public double Width { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public string WorkName { get; set; }
        public DateTime StartDate { get; set; }

        public RoadWork(double width, double length, double weight, string workName, DateTime startDate)
        {
            Width = width;
            Length = length;
            Weight = weight;
            WorkName = workName;
            StartDate = startDate;
        }

        public virtual double CalculateQuality()
        {
            return Width * Length * Weight / 1000;
        }

        public override string ToString()
        {
            return $"{WorkName} (Ш: {Width}м, Д: {Length}м, Вес: {Weight}кг/м², Дата: {StartDate:d}, Q: {CalculateQuality():F2})";
        }

    }
}
