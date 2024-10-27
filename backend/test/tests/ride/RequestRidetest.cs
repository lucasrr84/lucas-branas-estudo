using account.src.domain.dto;
using ride.src.application.usecase;
using ride.src.domain.dto;
using ride.src.infra.gateway;
using ride.src.infra.repository;
using NUnit.Framework;

namespace test.tests.ride
{
    public class RequestRidetest
    {
        private AccountGateway _accountGateway;
        private IRideRepository _rideRepository;
        private IPositionRepository _positionRepository;
        private RequestRide _requestRide;
        private GetRide _getRide;

        [SetUp]
        public void Setup()
        {
            _accountGateway = new AccountGateway(new HttpClient());
            _rideRepository = new RideRepositoryMemory();
            _positionRepository = new PositionRepositoryMemory();
            _requestRide = new RequestRide(_accountGateway, _rideRepository);
            _getRide = new GetRide(_rideRepository, _positionRepository);
        }

        //Deve solicitar uma corrida
        [Test]
        public async Task Deve_Solicitar_Uma_Corrida()
        {
            // Arrange
            var inputSignup = new SignupInputDto
            {
                name = "John Doe",
                email = $"john.doe{new Random().Next()}@gmail.com",
                cpf = "97456321558",
                password = "123456",
                isPassenger = true
            };

            var outputSignup = await _accountGateway.signup(inputSignup);

            var inputRequestRide = new RequestRideInputDto
            {
		        passengerId = outputSignup.accountId,
		        fromLat = -27.584905257808835,
		        fromLong = -48.545022195325124,
		        toLat = -27.496887588317275,
		        toLong = -48.522234807851476
            };

            var outputRequestRide = await _requestRide.execute(inputRequestRide);
            Assert.IsNotNull(outputRequestRide.rideId);

            var outputGetRide = await _getRide.execute(outputRequestRide.rideId);

            Assert.That(outputGetRide.rideId, Is.EqualTo(outputRequestRide.rideId));
            Assert.That(outputGetRide.passengerId, Is.EqualTo(inputRequestRide.passengerId));
            Assert.That(outputGetRide.fromLat, Is.EqualTo(inputRequestRide.fromLat));
            Assert.That(outputGetRide.fromLong, Is.EqualTo(inputRequestRide.fromLong));
            Assert.That(outputGetRide.toLat, Is.EqualTo(inputRequestRide.toLat));
            Assert.That(outputGetRide.toLong, Is.EqualTo(inputRequestRide.toLong));
            Assert.That(outputGetRide.status, Is.EqualTo("requested"));
        }

        //Não deve solicitar uma corrida se já houver outra em andamento
        [Test]
        public async Task Nao_Deve_Solicitar_Uma_Corrida_Se_Houver_Outra_Em_Andamento()
        {
            // Arrange
            var inputSignup = new SignupInputDto
            {
                name = "John Doe",
                email = $"john.doe{new Random().Next()}@gmail.com",
                cpf = "97456321558",
                password = "123456",
                isPassenger = true
            };

            var outputSignup = await _accountGateway.signup(inputSignup);

            var inputRequestRide = new RequestRideInputDto
            {
		        passengerId = outputSignup.accountId,
		        fromLat = -27.584905257808835,
		        fromLong = -48.545022195325124,
		        toLat = -27.496887588317275,
		        toLong = -48.522234807851476
            };

            // Act & Assert
            await _requestRide.execute(inputRequestRide);
            var ex = Assert.ThrowsAsync<Exception>(async () => 
                await _requestRide.execute(inputRequestRide));
            
            Assert.That(ex.Message, Is.EqualTo("Passenger already have an active ride"));
        }

        //Não deve solicitar uma corrida se a conta não for de um passageito
        [Test]
        public async Task Nao_Deve_Solicitar_Uma_Corrida_Se_A_Conta_Nao_For_De_Um_Passageiro()
        {
            // Arrange
            var inputSignup = new SignupInputDto
            {
                name = "John Doe",
                email = $"john.doe{new Random().Next()}@gmail.com",
                cpf = "97456321558",
                carPlate = "ABC1234",
                password = "123456",
                isDriver = true
            };

            var outputSignup = await _accountGateway.signup(inputSignup);

            var inputRequestRide = new RequestRideInputDto
            {
		        passengerId = outputSignup.accountId,
		        fromLat = -27.584905257808835,
		        fromLong = -48.545022195325124,
		        toLat = -27.496887588317275,
		        toLong = -48.522234807851476
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => 
                await _requestRide.execute(inputRequestRide));
            
            Assert.That(ex.Message, Is.EqualTo("Account must be from a passenger"));
        }
    }
}