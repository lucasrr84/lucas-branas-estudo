using account.src.domain.dto;
using ride.src.application.usecase;
using ride.src.domain.dto;
using ride.src.infra.gateway;
using ride.src.infra.repository;
using NUnit.Framework;

namespace test.tests.ride
{
    public class AcceptRidetest
    {
        private AccountGateway _accountGateway;
        private RequestRide _requestRide;
        private AcceptRide _acceptRide;
        private GetRide _getRide;        
        private IRideRepository _rideRepository;
        private IPositionRepository _positionRepository;

        [SetUp]
        public void Setup()
        {
            _rideRepository = new RideRepositoryMemory();
            _positionRepository = new PositionRepositoryMemory();
            _accountGateway = new AccountGateway(new HttpClient());
            _requestRide = new RequestRide(_accountGateway, _rideRepository);
            _acceptRide = new AcceptRide(_accountGateway, _rideRepository);
            _getRide = new GetRide(_rideRepository, _positionRepository);
        }

        //Deve aceitar uma corrida
        [Test]
        public async Task Deve_Aceitar_Uma_Corrida()
        {
            var inputSignupPassenger = new SignupInputDto
            {
                name = "John Passenger",
                email = $"john.doe{new Random().Next()}@gmail.com",
                cpf = "97456321558",
                password = "123456",
                isPassenger = true
            };
            
            var outputSignupPassenger = await _accountGateway.signup(inputSignupPassenger);

            var inputSignupDriver = new SignupInputDto
            {
                name = "John Driver",
                email = $"john.doe{new Random().Next()}@gmail.com",
                cpf = "97456321558",
                carPlate = "ABC1234",
                password = "123456",
                isDriver = true
            };

            var outputSignupDriver = await _accountGateway.signup(inputSignupDriver);

            var inputRequestRide = new RequestRideInputDto
            {
		        passengerId = outputSignupPassenger.accountId,
		        fromLat = -27.584905257808835,
		        fromLong = -48.545022195325124,
		        toLat = -27.496887588317275,
		        toLong = -48.522234807851476
            };

            var outputRequestRide = await _requestRide.execute(inputRequestRide);

            var acceptRideInput = new AcceptRideInputDto
            {
                rideId = outputRequestRide.rideId,
                driverId = outputSignupDriver.accountId
            };

            await _acceptRide.execute(acceptRideInput);

            var outputGetRide = await _getRide.execute(outputRequestRide.rideId);

            //Assert
            Assert.That(outputGetRide.status, Is.EqualTo("accepted"));
            Assert.That(outputGetRide.driverId, Is.EqualTo(outputSignupDriver.accountId));
        }

        //Nao deve aceitar uma corrida que ja foi aceita
        [Test]
        public async Task Nao_Deve_Aceitar_Uma_Corrida_Que_Ja_Foi_Aceita()
        {
            var inputSignupPassenger = new SignupInputDto
            {
                name = "John Passenger",
                email = $"john.doe{new Random().Next()}@gmail.com",
                cpf = "97456321558",
                password = "123456",
                isPassenger = true
            };
            
            var outputSignupPassenger = await _accountGateway.signup(inputSignupPassenger);

            var inputSignupDriver = new SignupInputDto
            {
                name = "John Driver",
                email = $"john.doe{new Random().Next()}@gmail.com",
                cpf = "97456321558",
                carPlate = "ABC1234",
                password = "123456",
                isDriver = true
            };

            var outputSignupDriver = await _accountGateway.signup(inputSignupDriver);

            var inputRequestRide = new RequestRideInputDto
            {
		        passengerId = outputSignupPassenger.accountId,
		        fromLat = -27.584905257808835,
		        fromLong = -48.545022195325124,
		        toLat = -27.496887588317275,
		        toLong = -48.522234807851476
            };

            var outputRequestRide = await _requestRide.execute(inputRequestRide);

            var acceptRideInput = new AcceptRideInputDto
            {
                rideId = outputRequestRide.rideId,
                driverId = outputSignupDriver.accountId
            };

            await _acceptRide.execute(acceptRideInput);
            //_acceptRide.execute(acceptRideInput);
            
            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _acceptRide.execute(acceptRideInput));

            Assert.That(ex.Message, Is.EqualTo("Invalid status"));
        }
    }
}