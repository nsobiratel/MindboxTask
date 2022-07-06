using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibForMindbox
{
    public class Circle : Figure
    {
        private readonly double radius;

        public Circle(double radiusMm)
        {
            if (radiusMm <= 0 || !double.IsFinite(radiusMm))
            {
                throw new ArgumentOutOfRangeException(nameof(radiusMm), radiusMm, "Must be greater then 0");
            }
            this.radius = radiusMm;
        }

        public override double AreaMm2
        {
            get
            {
                if (!this.areamm2.HasValue)
                {
                    this.areamm2 = Math.PI * Math.Pow(this.radius, 2);
                    if (double.IsPositiveInfinity(this.areamm2.Value))
                    {
                        this.areamm2 = double.MaxValue;
                    }
                }
                return this.areamm2.Value;
            }
        }
    }
}
