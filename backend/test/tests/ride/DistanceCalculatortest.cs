using ride.src.domain.service.distance;
using ride.src.domain.vo;
using NUnit.Framework;

namespace test.tests.ride
{
    public class DistanceCalculatortest
    {
        [SetUp]
        public void Setup()
        {
        }

        //Deve calcular a distancia entre duas coordenadas
        [Test]
        public void Deve_Calcular_A_Distancia_Entre_Duas_Coordenadas()
        {
            var from = new Coord(-27.584905257808835, -48.545022195325124);
	        var to = new Coord(-27.496887588317275, -48.522234807851476);
	        
            Assert.That(DistanceCalculator.calculate(from, to), Is.EqualTo(10));
        }
    }
}