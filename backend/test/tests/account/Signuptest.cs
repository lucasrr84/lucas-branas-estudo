using account.src.application.usecase;
using account.src.domain.dto;
using account.src.infra.gateway;
using account.src.infra.repository;
using NUnit.Framework;

namespace test.tests.account
{
    public class Signuptest
    {
        private Signup _signup;
        private IAccountRepository _accountRepository;
        private IMailerGateway _mailerGateway;
        private GetAccount _getAccount;


        [SetUp]
        public void Setup()
        {
            _accountRepository = new AccountRepositoryMemory();
            _mailerGateway = new MailerGatewayMemory();
            _signup = new Signup(_accountRepository, _mailerGateway);
            _getAccount = new GetAccount(_accountRepository);
        }

        //Deve criar a conta de um passageiro
        [Test]
        public async Task Deve_Criar_A_Conta_De_Um_Passageiro()
        {
            // Arrange
            var input = new AccountDto
            {
                name = "John Doe",
                email = $"john.doe{new Random().Next()}@gmail.com",
                cpf = "97456321558",
                password = "123456",
                isPassenger = true
            };
            
            // Act - Criar a conta
            var outputSignup = await _signup.execute(input);

            Assert.IsNotNull(outputSignup.accountId);
            
            var outputGetAccount = await _getAccount.execute(outputSignup.accountId);

            Assert.That(outputGetAccount.name, Is.EqualTo(input.name));
            Assert.That(outputGetAccount.email, Is.EqualTo(input.email));
            Assert.That(outputGetAccount.cpf, Is.EqualTo(input.cpf));
            Assert.That(outputGetAccount.password, Is.EqualTo(input.password));
        }

        //Deve criar a conta de um passageiro em md5
        [Test]
        public async Task Deve_Criar_A_Conta_De_Um_Passageiro_Em_Md5()
        {
            // Arrange
            var input = new AccountDto
            {
                name = "John Doe",
                email = $"john.doe{new Random().Next()}@gmail.com",
                cpf = "97456321558",
                password = "123456",
                isPassenger = true,
                passwordType = "md5"
            };
            
            // Act - Criar a conta
            var outputSignup = await _signup.execute(input);

            Assert.IsNotNull(outputSignup.accountId);
            
            var outputGetAccount = await _getAccount.execute(outputSignup.accountId);

            Assert.That(outputGetAccount.name, Is.EqualTo(input.name));
            Assert.That(outputGetAccount.email, Is.EqualTo(input.email));
            Assert.That(outputGetAccount.cpf, Is.EqualTo(input.cpf));
            Assert.That(outputGetAccount.password, Is.EqualTo("e10adc3949ba59abbe56e057f20f883e"));
        }

        //Deve criar a conta de um passageiro em sha1
        [Test]
        public async Task Deve_Criar_A_Conta_De_Um_Passageiro_Em_Sha1()
        {
            // Arrange
            var input = new AccountDto
            {
                name = "John Doe",
                email = $"john.doe{new Random().Next()}@gmail.com",
                cpf = "97456321558",
                password = "123456",
                isPassenger = true,
                passwordType = "sha1"
            };
            
            // Act - Criar a conta
            var outputSignup = await _signup.execute(input);

            Assert.IsNotNull(outputSignup.accountId);
            
            var outputGetAccount = await _getAccount.execute(outputSignup.accountId);

            Assert.That(outputGetAccount.name, Is.EqualTo(input.name));
            Assert.That(outputGetAccount.email, Is.EqualTo(input.email));
            Assert.That(outputGetAccount.cpf, Is.EqualTo(input.cpf));
            Assert.That(outputGetAccount.password, Is.EqualTo("7c4a8d09ca3762af61e59520943dc26494f8941b"));
        }

        //Nao deve criar a conta de um passageiro com nome invalido
        [Test]
        public async Task Nao_Deve_Criar_A_Conta_De_Um_Passageiro_Com_Nome_Invalido()
        {
            // Arrange
            var input = new AccountDto
            {
                name = "John",
                email = $"john.doe{new Random().Next()}@gmail.com",
                cpf = "97456321558",
                password = "123456",
                isPassenger = true
            };
            
            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _signup.execute(input));
            Assert.That(ex.Message, Is.EqualTo("Invalid name"));
        }

        //Nao deve criar a conta de um passageiro duplicado
        [Test]
        public async Task Nao_Deve_Criar_A_Conta_De_Um_Passageiro_Duplicado()
        {
            // Arrange
            var input = new AccountDto
            {
                name = "John Doe",
                email = $"john.doe{new Random().Next()}@gmail.com",
                cpf = "97456321558",
                password = "123456",
                isPassenger = true
            };
            
            // Act & Assert
            await _signup.execute(input);
            var ex = Assert.ThrowsAsync<Exception>(async () => await _signup.execute(input));
            Assert.That(ex.Message, Is.EqualTo("Duplicated account"));
        }
    }
}