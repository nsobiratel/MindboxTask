namespace LibForMindbox
{
    public abstract class Figure
    {
        protected double? areamm2;
        public abstract double AreaMm2 { get; }

        public double AreaSm2 => this.AreaMm2 / 100;

        public double AreaM2 => this.AreaSm2 / 10_000;
    }
}