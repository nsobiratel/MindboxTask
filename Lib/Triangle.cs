using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibForMindbox
{
    public class Triangle : Figure
    {
        private readonly double side1;
        private readonly double side2;
        private readonly double side3;
        private readonly double sidesSum;

        private bool? isRigthTriangle;

        public Triangle(double side1mm, double side2mm, double side3mm)
        {
            if (side1mm <= 0 || !double.IsFinite(side1mm))
            {
                throw new ArgumentOutOfRangeException(nameof(side1mm), side1mm, "Must be greater then 0");
            }

            if (side2mm <= 0 || !double.IsFinite(side2mm))
            {
                throw new ArgumentOutOfRangeException(nameof(side2mm), side2mm, "Must be greater then 0");
            }

            if (side3mm <= 0 || !double.IsFinite(side3mm))
            {
                throw new ArgumentOutOfRangeException(nameof(side3mm), side3mm, "Must be greater then 0");
            }

            if (
                side1mm + side2mm <= side3mm
                || side1mm + side3mm <= side2mm
                || side2mm + side3mm <= side1mm
            )
            {
                throw new ArgumentException(
                    $"Impossible triangle. Invalid length of sides [{side1mm}, {side2mm}, {side3mm}]"
                );
            }

            this.sidesSum = side1mm + side2mm + side3mm;
            if (double.IsInfinity(this.sidesSum))
            {
                throw new OverflowException(
                    $"Sum of sides [{side1mm}, {side2mm}, {side3mm}] is Infinity"
                );
            }

            this.side1 = side1mm;
            this.side2 = side2mm;
            this.side3 = side3mm;
        }

        public override double AreaMm2
        {
            get
            {
                if (!this.areamm2.HasValue)
                {
                    double p = (this.side1 + this.side2 + this.side3) / 2;
                    this.areamm2 = Math.Sqrt(p * (p - this.side1) * (p - this.side2) * (p - this.side3));
                }
                return this.areamm2.Value;
            }
        }

        public bool IsRightTriangle 
        { 
            get
            {
                if (!this.isRigthTriangle.HasValue)
                {
                    double powMaxSide;
                    double powSidesSum;
                    if (this.side1 > this.side2 && this.side1 > this.side3)
                    {
                        powMaxSide = Math.Pow(this.side1, 2);
                        powSidesSum = Math.Pow(this.side2, 2) + Math.Pow(this.side3, 2);
                    }
                    else if (this.side2 > this.side1 && this.side2 > this.side3)
                    {
                        powMaxSide = Math.Pow(this.side2, 2);
                        powSidesSum = Math.Pow(this.side1, 2) + Math.Pow(this.side3, 2);
                    }
                    else
                    {
                        powMaxSide = Math.Pow(this.side3, 2);
                        powSidesSum = Math.Pow(this.side1, 2) + Math.Pow(this.side2, 2);
                    }
                    this.isRigthTriangle = powMaxSide == powSidesSum;
                }
                return this.isRigthTriangle.Value;
            }
        }
    }
}
