using account.src.domain.dto;
using ride.src.application.usecase;
using ride.src.domain.dto;
using ride.src.infra.gateway;
using ride.src.infra.repository;
using NUnit.Framework;

namespace test.tests.ride
{
    public class UpdatePositiontest
    {
        private AccountGateway _accountGateway;
        private IRideRepository _rideReposiroty;
        private IPositionRepository _positionRepository;
        private RequestRide _requestRide;
        private AcceptRide _acceptRide;
        private StartRide _startRide;
        private UpdatePosition _updatePosition;
        private GetRide _getRide;

        [SetUp]
        public void Setup()
        {
            _rideReposiroty = new RideRepositoryMemory();
            _positionRepository = new PositionRepositoryMemory();
            _accountGateway = new AccountGateway(new HttpClient());
            _requestRide = new RequestRide(_accountGateway, _rideReposiroty);
            _acceptRide = new AcceptRide(_accountGateway, _rideReposiroty);
            _startRide = new StartRide(_rideReposiroty);
            _updatePosition = new UpdatePosition(_rideReposiroty, _positionRepository);
            _getRide = new GetRide(_rideReposiroty, _positionRepository);
        }

        //Deve atualiazar a posicao de uma corrida
        [Test]
        public async Task Deve_Atualizar_A_Posicao_De_Uma_Corrida()
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

            var startRideInput = new StartRideInputDto
            {
                rideId = outputRequestRide.rideId,
            };

            await _startRide.execute(startRideInput);

            var inputUpdatePosition1 = new UpdatePositionInputDto
            {
                rideId = outputRequestRide.rideId,
                lat = -27.584905257808835,
		        longi = -48.545022195325124,
		        date = DateTime.Now
            };

            await _updatePosition.execute(inputUpdatePosition1);

            var inputUpdatePosition2 = new UpdatePositionInputDto
            {
                rideId = outputRequestRide.rideId,
                lat = -27.496887588317275,
		        longi = -48.522234807851476,
		        date = DateTime.Now
            };

            await _updatePosition.execute(inputUpdatePosition2);

            var inputUpdatePosition3 = new UpdatePositionInputDto
            {
                rideId = outputRequestRide.rideId,
                lat = -27.584905257808835,
		        longi = -48.545022195325124,
		        date = DateTime.Now
            };

            await _updatePosition.execute(inputUpdatePosition3);

            var inputUpdatePosition4 = new UpdatePositionInputDto
            {
                rideId = outputRequestRide.rideId,
                lat = -27.496887588317275,
		        longi = -48.522234807851476,
		        date = DateTime.Now
            };

            await _updatePosition.execute(inputUpdatePosition4);

            var outputGetRide = await _getRide.execute(outputRequestRide.rideId);

            Assert.That(outputGetRide.distance, Is.EqualTo(30));
        }
    }
}