using LibForMindbox;

namespace LibForMindBoxTest
{
    public class CircleSuite
    {
        const double tolerance = 0.00000000000001;

        [Test]
        [TestCase(double.NaN)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.MinValue)]
        [TestCase(0)]
        public void RadiusInvalidValues(double radius)
        {
            var exc = Assert.Catch<ArgumentOutOfRangeException>(
                () => new Circle(radius)
            );
            Assert.Multiple(() =>
            {
                Assert.That(
                    exc.ActualValue,
                    Is.EqualTo(radius)
                );

                Assert.That(
                    exc.ParamName,
                    Is.EqualTo("radiusMm")
                );

                Assert.That(
                    exc.Message,
                    Is.EqualTo($"Must be greater then 0 (Parameter 'radiusMm')\r\nActual value was {radius}.")
                );
            });
        }

        [Test]
        [TestCase(double.Epsilon, 0)]
        [TestCase(double.MaxValue, double.MaxValue)]
        public void RadiusValidBounds(double boundedRadiusMm, double expectedMm)
        {
            var circle = new Circle(boundedRadiusMm);
            Assert.Multiple(() =>
            {
                Assert.That(
                    circle.AreaMm2,
                    Is.EqualTo(expectedMm).Within(tolerance),
                    nameof(circle.AreaMm2)
                );

                Assert.That(
                    circle.AreaSm2,
                    Is.EqualTo(expectedMm / 100).Within(tolerance),
                    nameof(circle.AreaSm2)
                );

                Assert.That(
                    circle.AreaM2,
                    Is.EqualTo(expectedMm / 100 / 10_000).Within(tolerance),
                    nameof(circle.AreaM2)
                );
            });
        }

        [Test]
        public void OneMmRadius()
        {
            var circle = new Circle(1);
            Assert.That(
                circle.AreaMm2,
                Is.EqualTo(Math.PI),
                nameof(circle.AreaMm2)
            );
        }

        [Test]
        public void MultipleCalcArea()
        {
            var circle = new Circle(10);
            var constraint = Is.EqualTo(Math.PI * 100);

            Assert.That(
                circle.AreaMm2,
                constraint,
                nameof(circle.AreaMm2)
            );

            Assert.That(
                circle.AreaMm2,
                constraint,
                nameof(circle.AreaMm2)
            );

            Assert.That(
                circle.AreaMm2,
                constraint,
                nameof(circle.AreaMm2)
            );
        }

        [Test]
        public void MultipleCalcAreaSm2()
        {
            var circle = new Circle(10);
            var constraint = Is.EqualTo(Math.PI);

            Assert.That(
                circle.AreaSm2,
                constraint,
                nameof(circle.AreaSm2)
            );

            Assert.That(
                circle.AreaSm2,
                constraint,
                nameof(circle.AreaSm2)
            );

            Assert.That(
                circle.AreaSm2,
                constraint,
                nameof(circle.AreaSm2)
            );
        }

        [Test]
        public void MultipleCalcAreaM2()
        {
            var circle = new Circle(10);
            var constraint = Is.EqualTo(Math.PI / 10_000);

            Assert.That(
                circle.AreaM2,
                constraint,
                nameof(circle.AreaM2)
            );

            Assert.That(
                circle.AreaM2,
                constraint,
                nameof(circle.AreaM2)
            );

            Assert.That(
                circle.AreaM2,
                constraint,
                nameof(circle.AreaM2)
            );
        }

        [Test]
        public void Sequential_Mm2_Sm2_M2()
        {
            var circle = new Circle(100.1);
            var areaMm = Math.PI * 100.1 * 100.1;
            const double tolerance = 0.0000000001;

            Assert.That(
                circle.AreaMm2,
                Is.EqualTo(areaMm).Within(tolerance),
                nameof(circle.AreaMm2)
            );

            Assert.That(
                circle.AreaSm2,
                Is.EqualTo(areaMm / 100).Within(tolerance),
                nameof(circle.AreaSm2)
            );

            Assert.That(
                circle.AreaM2,
                Is.EqualTo(areaMm / 100 / 10_000).Within(tolerance),
                nameof(circle.AreaM2)
            );
        }
    }
}