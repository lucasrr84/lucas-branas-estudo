using account.src.domain.vo;
using NUnit.Framework;

namespace test.tests.account
{
    public class Passwordtest
    {
        [SetUp]
        public void Setup()
        {
        }

        //Deve criar senha valida
        [Test]
        public void Deve_Cria_Senha_Valida()
        {
            //Act
            var password = PasswordFactory.create("textplain", "123456");

            //Assert
            Assert.That(password.getValue(), Is.EqualTo("123456"));
        }

        //Nao deve criar senha invalida
        [Test]
        public void Nao_Deve_Cria_Senha_Invalida()
        {
            // Act & Assert
            var ex = Assert.Throws<Exception>(() => PasswordFactory.create("textplain", "1234"));

            Assert.That(ex.Message, Is.EqualTo("Invalid password"));
        }
    }
}