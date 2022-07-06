using LibForMindbox;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibForMindBoxTest
{
    internal class TriangleSuite
    {
        const double tolerance = 0.00000000000001;
        const double regularTriangleArea = 0.96824583655185426;
        const double rightTriangleArea = 24.0;

        [Test]
        [TestCase(double.NaN)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.MinValue)]
        [TestCase(0)]
        public void AllSideSizeInvalidValues(double sideSize)
        {
            var exc = Assert.Catch<ArgumentOutOfRangeException>(
                () => new Triangle(sideSize, sideSize, sideSize)
            );
            Assert.Multiple(() =>
            {
                Assert.That(
                    exc.ActualValue,
                    Is.EqualTo(sideSize)
                );

                StringAssert.AreEqualIgnoringCase(
                    "side1mm",
                    exc.ParamName
                );

                StringAssert.AreEqualIgnoringCase(
                    $"Must be greater then 0 (Parameter 'side1mm')\r\nActual value was {sideSize}.",
                    exc.Message
                );
            });
        }

        [Test]
        public void SumSidesOverflow()
        {
            var exc = Assert.Catch<OverflowException>(
                () => new Triangle(
                    double.MaxValue,
                    double.MaxValue,
                    double.MaxValue
                )
            );
            StringAssert.AreEqualIgnoringCase(
                $"Sum of sides [{double.MaxValue}, {double.MaxValue}, {double.MaxValue}] is Infinity",
                exc.Message
            );
        }

        [Test]
        [TestCase(double.NaN)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.MinValue)]
        [TestCase(0)]
        public void FirstSideSizeInvalidValues(double sideSize)
        {
            var exc = Assert.Catch<ArgumentOutOfRangeException>(
                () => new Triangle(sideSize, 1, 2)
            );
            Assert.Multiple(() =>
            {
                Assert.That(
                    exc.ActualValue,
                    Is.EqualTo(sideSize)
                );

                StringAssert.AreEqualIgnoringCase(
                    "side1mm",
                    exc.ParamName
                );

                StringAssert.AreEqualIgnoringCase(
                    $"Must be greater then 0 (Parameter 'side1mm')\r\nActual value was {sideSize}.",
                    exc.Message
                );
            });
        }

        [Test]
        [TestCase(double.NaN)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.MinValue)]
        [TestCase(0)]
        public void SecondSideSizeInvalidValues(double sideSize)
        {
            var exc = Assert.Catch<ArgumentOutOfRangeException>(
                () => new Triangle(1, sideSize, 2)
            );
            Assert.Multiple(() =>
            {
                Assert.That(
                    exc.ActualValue,
                    Is.EqualTo(sideSize)
                );

                StringAssert.AreEqualIgnoringCase(
                    "side2mm",
                    exc.ParamName
                );

                StringAssert.AreEqualIgnoringCase(
                    $"Must be greater then 0 (Parameter 'side2mm')\r\nActual value was {sideSize}.",
                    exc.Message
                );
            });
        }

        [Test]
        [TestCase(double.NaN)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.MinValue)]
        [TestCase(0)]
        public void ThirdSideSizeInvalidValues(double sideSize)
        {
            var exc = Assert.Catch<ArgumentOutOfRangeException>(
                () => new Triangle(1, 2, sideSize)
            );
            Assert.Multiple(() =>
            {
                Assert.That(
                    exc.ActualValue,
                    Is.EqualTo(sideSize)
                );

                StringAssert.AreEqualIgnoringCase(
                    "side3mm",
                    exc.ParamName
                );

                StringAssert.AreEqualIgnoringCase(
                    $"Must be greater then 0 (Parameter 'side3mm')\r\nActual value was {sideSize}.",
                    exc.Message
                );
            });
        }

        [Test]
        [TestCase(1, 2, 3)]
        [TestCase(1, 2, 4)]
        [TestCase(1, 3, 2)]
        [TestCase(1, 4, 2)]
        [TestCase(3, 1, 2)]
        [TestCase(4, 1, 2)]
        [TestCase(double.MaxValue, tolerance, tolerance)]
        [TestCase(tolerance, double.MaxValue, tolerance)]
        [TestCase(tolerance, tolerance, double.MaxValue)]
        [TestCase(double.MaxValue, double.MaxValue, tolerance)]
        [TestCase(tolerance, double.MaxValue, double.MaxValue)]
        [TestCase(double.MaxValue, tolerance, double.MaxValue)]
        public void ImpossibleTriangle(
            double side1,
            double side2,
            double side3
        )
        {
            var exc = Assert.Catch<ArgumentException>(
                () => new Triangle(side1, side2, side3)
            );

            Assert.Multiple(() =>
            {
                Assert.That(
                    exc.ParamName,
                    Is.Null
                );

                StringAssert.AreEqualIgnoringCase(
                    $"Impossible triangle. Invalid length of sides [{side1}, {side2}, {side3}]",
                    exc.Message
                );
            });
        }

        [Test]
        [TestCase(1, 2, 2, false, regularTriangleArea)]
        [TestCase(2, 1, 2, false, regularTriangleArea)]
        [TestCase(2, 2, 1, false, regularTriangleArea)]
        [TestCase(6, 8, 10, true, rightTriangleArea)]
        [TestCase(10, 6, 8, true, rightTriangleArea)]
        [TestCase(8, 10, 6, true, rightTriangleArea)]
        public void ValidTriangle(
            double side1,
            double side2,
            double side3,
            bool isRight,
            double etalonArea
        )
        {
            var triangle = new Triangle(side1, side2, side3);
            Assert.Multiple(() =>
            {
                Assert.That(
                    triangle.AreaMm2,
                    Is.EqualTo(etalonArea).Within(tolerance)
                );

                Assert.That(
                    triangle.AreaSm2,
                    Is.EqualTo(etalonArea / 100)
                );

                Assert.That(
                    triangle.AreaM2,
                    Is.EqualTo(etalonArea / 100 / 10_000)
                );

                Assert.That(
                    triangle.IsRightTriangle,
                    Is.EqualTo(isRight)
                );
            });
        }


        [Test]
        public void MultipleCalcArea()
        {
            var triangle = new Triangle(1, 2, 2);
            var constraint = Is.EqualTo(regularTriangleArea);

            Assert.That(
                triangle.AreaMm2,
                constraint,
                nameof(Triangle.AreaMm2)
            );

            Assert.That(
                triangle.AreaMm2,
                constraint,
                nameof(Triangle.AreaMm2)
            );

            Assert.That(
                triangle.AreaMm2,
                constraint,
                nameof(Triangle.AreaMm2)
            );
        }

        [Test]
        public void MultipleCalcAreaSm2()
        {
            var triangle = new Triangle(1, 2, 2);
            var constraint = Is.EqualTo(regularTriangleArea / 100);

            Assert.That(
                triangle.AreaSm2,
                constraint,
                nameof(Triangle.AreaSm2)
            );

            Assert.That(
                triangle.AreaSm2,
                constraint,
                nameof(Triangle.AreaSm2)
            );

            Assert.That(
                triangle.AreaSm2,
                constraint,
                nameof(Triangle.AreaSm2)
            );
        }

        [Test]
        public void MultipleCalcAreaM2()
        {
            var triangle = new Triangle(1, 2, 2);
            var constraint = Is.EqualTo(regularTriangleArea / 100 / 10_000);

            Assert.That(
                triangle.AreaM2,
                constraint,
                nameof(Triangle.AreaM2)
            );

            Assert.That(
                triangle.AreaM2,
                constraint,
                nameof(Triangle.AreaM2)
            );

            Assert.That(
                triangle.AreaM2,
                constraint,
                nameof(Triangle.AreaM2)
            );
        }

        [Test]
        public void MultipleCalcIsRightFalse()
        {
            var triangle = new Triangle(1, 2, 2);

            Assert.That(
                triangle.IsRightTriangle,
                Is.False,
                nameof(Triangle.IsRightTriangle)
            );

            Assert.That(
                triangle.IsRightTriangle,
                Is.False,
                nameof(Triangle.IsRightTriangle)
            );

            Assert.That(
                triangle.IsRightTriangle,
                Is.False,
                nameof(Triangle.IsRightTriangle)
            );
        }

        [Test]
        public void MultipleCalcIsRightTrue()
        {
            var triangle = new Triangle(6, 8, 10);

            Assert.That(
                triangle.IsRightTriangle,
                Is.True,
                nameof(Triangle.IsRightTriangle)
            );

            Assert.That(
                triangle.IsRightTriangle,
                Is.True,
                nameof(Triangle.IsRightTriangle)
            );

            Assert.That(
                triangle.IsRightTriangle,
                Is.True,
                nameof(Triangle.IsRightTriangle)
            );
        }
    }
}
