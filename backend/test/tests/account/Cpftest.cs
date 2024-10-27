using account.src.domain.vo;
using NUnit.Framework;

namespace test.tests.account
{
    public class Cpftest
    {
        [SetUp]
        public void Setup()
        {
        }

        //Deve validar um cpf valido
        [TestCase("97456321558")]
        [TestCase("71428793860")]
        [TestCase("87748248800")]
        public void Deve_Validar_Cpf_Valido(string value)
        {
            //Act
            var cpf = new Cpf(value);

            //Assert
            Assert.That(cpf.getValue(), Is.EqualTo(value));
        }

        //Nao deve validar um cpf invalido
        [TestCase("9745632155")]
        [TestCase("11111111111")]
        [TestCase("97a56321558")]
        public void Nao_Deve_Validar_Cpf_Invalido(string value)
        {
            // Act & Assert
            var ex = Assert.Throws<Exception>(() => new Cpf(value));

            Assert.That(ex.Message, Is.EqualTo("Invalid cpf"));
        }
    }
}