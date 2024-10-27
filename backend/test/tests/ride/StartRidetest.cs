using account.src.domain.dto;
using ride.src.application.usecase;
using ride.src.domain.dto;
using ride.src.infra.gateway;
using ride.src.infra.repository;
using NUnit.Framework;

namespace test.tests.ride
{
    public class StartRidetest
    {
        private AccountGateway _accountGateway;
        private IRideRepository _rideRepository;
        private IPositionRepository _positionRepository;
        private RequestRide _requestRide;
        private StartRide _startRide;
        private GetRide _getRide;
        private AcceptRide _acceptRide;
        
        [SetUp]
        public void Setup()
        {
            _accountGateway = new AccountGateway(new HttpClient());
            _rideRepository = new RideRepositoryMemory();
            _positionRepository = new PositionRepositoryMemory();
            _requestRide = new RequestRide(_accountGateway, _rideRepository);
            _getRide = new GetRide(_rideRepository, _positionRepository);
            _acceptRide = new AcceptRide(_accountGateway, _rideRepository);
            _startRide = new StartRide(_rideRepository);
        }

        //Deve iniciar uma corrida
        [Test]
        public async Task Deve_Iniciar_Uma_Corrida()
        {
            // Arrange
            var inputSignupPassenger = new SignupInputDto
            {
                name = "John Doe",
                email = $"john.doe{new Random().Next()}@gmail.com",
                cpf = "97456321558",
                password = "123456",
                isPassenger = true
            };

            var outputSignupPassenger = await _accountGateway.signup(inputSignupPassenger);

            var inputSignupDriver = new SignupInputDto
            {
                name = "John Doe",
                email = $"john.doe{new Random().Next()}@gmail.com",
                cpf = "97456321558",
                carPlate = "ABC1234",
                password = "123456",
                isDriver = true,
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

            var startRideInput = new StartRideInputDto
            {
                rideId = outputRequestRide.rideId,
            };

            await _startRide.execute(startRideInput);

            var outputGetRide = await _getRide.execute(startRideInput.rideId);

            Assert.That(outputGetRide.status, Is.EqualTo("in_progress"));
        }
    }
}