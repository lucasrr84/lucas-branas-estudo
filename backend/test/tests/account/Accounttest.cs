using account.src.domain.entity;
using NUnit.Framework;

namespace test.tests.account
{
    public class Accounttest
    {
        [SetUp]
        public void Setup()
        {
        }

        //Deve criar uma conta
        [Test]
        public void Deve_Criar_Uma_Conta()
        {
            // Act
            var account = Account.create("Joh Doe", "john.doe@gmail.com", "97456321558", "", "123456", true, false);
            
            // Assert
            Assert.IsNotNull(account);
        }

        //Nao deve criar uma conta com nome invalido
        [Test]
        public void Nao_Deve_Criar_Uma_Conta_Com_Nome_Invalido()
        {
            // Act & Assert
            var ex = Assert.Throws<Exception>(() =>
                Account.create("John", "john.doe@gmail.com", "97456321558", "", "123456", true, false));

            Assert.That(ex.Message, Is.EqualTo("Invalid name"));
        }

        //Nao deve criar uma conta com email invalido
        [Test]
        public void Nao_Deve_Criar_Uma_Conta_Com_Email_Invalido()
        {
            // Act & Assert
            var ex = Assert.Throws<Exception>(() =>
                Account.create("John Doe", "john.doe", "97456321558", "", "123456", true, false));

            Assert.That(ex.Message, Is.EqualTo("Invalid email"));
        }

        //Nao deve criar uma conta com cpf invalido
        [Test]
        public void Nao_Deve_Criar_Uma_Conta_Com_Cpf_Invalido()
        {
            // Act & Assert
            var ex = Assert.Throws<Exception>(() =>
                Account.create("John Doe", "john.doe@gmail.com", "9745632155", "", "123456", true, false));

            Assert.That(ex.Message, Is.EqualTo("Invalid cpf"));
        }

        //Nao deve criar uma conta com placa do carro invalida
        [Test]
        public void Nao_Deve_Criar_Uma_Conta_Com_Placa_Invalida()
        {
            // Act & Assert
            var ex = Assert.Throws<Exception>(() =>
                Account.create("John Doe", "john.doe@gmail.com", "97456321558", "", "123456", true, true));

            Assert.That(ex.Message, Is.EqualTo("Invalid car plate"));
        }
    }
}